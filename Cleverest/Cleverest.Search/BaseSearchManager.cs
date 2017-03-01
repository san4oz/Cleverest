using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Cleverest.Business.Entities;
using Cleverest.Business.InterfaceDefinitions.Search;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Lucene.Net.Search;
using Lucene.Net.QueryParsers;
using Lucene.Net.Analysis;
using Cleverest.Business.Entities.Search;
using Cleverest.Search.Persisters;

namespace Cleverest.Search
{
    public abstract class BaseSearchManager<T> : IBaseSearchManager<T>
        where T : Entity, new()
    {
        private string _luceneDir = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "../index");
        private FSDirectory DirectoryTemp;

        private Lucene.Net.Util.Version Version = Lucene.Net.Util.Version.LUCENE_30;

        private FSDirectory Directory
        {
            get
            {
                if (DirectoryTemp == null)
                    DirectoryTemp = FSDirectory.Open(new DirectoryInfo(_luceneDir));

                if (IndexWriter.IsLocked(DirectoryTemp))
                    IndexWriter.Unlock(DirectoryTemp);

                var lockFilePath = Path.Combine(_luceneDir, "write.lock");
                if (File.Exists(lockFilePath))
                    File.Delete(lockFilePath);

                return DirectoryTemp;
            }
        }

        public virtual void AddToIndex(T entity)
        {
            AddToIndex(new[] { entity });
        }

        public virtual void AddToIndex(IList<T> entities)
        {
            using (var analyzer = new StandardAnalyzer(Version))
            {
                using (var writer = new IndexWriter(this.Directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
                {
                    foreach (var entity in entities)
                    {
                        var searchQuery = new TermQuery(new Term("Id", entity.Id));
                        writer.DeleteDocuments(searchQuery);

                        var doc = CreateDocument(entity);
                        writer.AddDocument(doc);
                    }
                }
            }
        }

        protected virtual Document CreateDocument(T entity)
        {
            var document = new Document();
            document.Add(new Field(IndexConstants.Id, entity.Id, Field.Store.YES, Field.Index.NO));

            var keywords = ExtractKeywords(entity, GetIndexInfo());
            document.Add(new Field(IndexConstants.Keywords, keywords, Field.Store.YES, Field.Index.ANALYZED));

            return document;
        }

        protected virtual string ExtractKeywords(T entity, IndexInfo info)
        {
            var sb = new StringBuilder();
            var persister = new IndexFieldPersisterBase();

            foreach(var field in info.KeywordFields)
            {
                var value = persister.ExtractKeywords(entity, field);
                sb.Append(value);
                sb.Append(' ');
            }

            return sb.ToString();
        }
       
        public virtual SearchResponse Search(SearchRequest request)
        {
            if (string.IsNullOrEmpty(request.Keywords))
                return new SearchResponse();

            var response = new SearchResponse();

            using (var searcher = new IndexSearcher(Directory, false))
            {
                var hitsLimit = short.MaxValue;

                using (var analyzer = new StandardAnalyzer(Version))
                {
                    var searchField = request.SearchField ?? IndexConstants.Keywords;

                    var query = ParseQuery(request.Keywords, searchField, analyzer, false);                    
                    var hits = searcher.Search(query, hitsLimit).ScoreDocs.ToList();

                    var searchResults = CreateSearchResults(searcher, hits, request);

                    response.Results.AddRange(searchResults);

                    return response;
                }
            }
        }

        protected virtual IList<SearchResult> CreateSearchResults(IndexSearcher searcher, List<ScoreDoc> hits, SearchRequest request)
        {
            var results = new List<SearchResult>();

            var indexInfo = GetIndexInfo();

            var persister = new IndexFieldPersisterBase();

            var fieldsToRead = indexInfo.KeywordFields.Where(f => request.Fields.Contains(f)).ToList();
            fieldsToRead.Add(IndexConstants.Id);

            foreach(var hit in hits)
            {
                var doc = GetDocument(searcher, hit.Doc, fieldsToRead.ToArray());
                var result = new SearchResult() { Id = doc.Get(IndexConstants.Id), Score = hit.Score };
                result.Fields = fieldsToRead.ToDictionary(key => key, val => persister.ParseRawValue(doc.Get(val)));
                results.Add(result);
            }

            return results;
        }

        protected Document GetDocument(IndexSearcher searcher, int n)
        {
            return GetDocument(searcher, n, new string[] { IndexConstants.Id });
        }

        protected Document GetDocument(IndexSearcher searcher, int n, string[] fields)
        {
            return searcher.Doc(n, new MapFieldSelector(fields));
        }

        protected virtual Query ParseQuery(string query, string searchField, Analyzer analyzer, bool matchAllKeywords = false)
        {
            if (string.IsNullOrEmpty(query))
                return null;

            var parser = new QueryParser(Version, searchField, analyzer);
            InitQueryParserOperator(parser, matchAllKeywords);
            query = AddWildCards(parser, query, matchAllKeywords);

            return parser.Parse(query);
        }

        protected virtual string AddWildCards(QueryParser parser, string query, bool matchAllKeywords)
        {
            query = matchAllKeywords ? SearchUtils.MakeFullWildcardQuery(query) : SearchUtils.MakeTrailingWildCardQuery(query);
            parser.AllowLeadingWildcard = true;
            parser.DefaultOperator = QueryParser.Operator.OR;
            return query;
        }

        protected virtual void InitQueryParserOperator(QueryParser queryParser, bool matchAllKeywords)
        {
            var op = matchAllKeywords ? QueryParser.Operator.OR : QueryParser.Operator.AND;
            queryParser.DefaultOperator = op;
        }     

        protected abstract IndexInfo GetIndexInfo();
    }
}
