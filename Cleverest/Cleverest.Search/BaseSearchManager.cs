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

namespace Cleverest.Search
{
    public abstract class BaseSearchManager<T> : IBaseSearchManager<T>
        where T : Entity
    {
        private string _luceneDir = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "../index");
        private FSDirectory DirectoryTemp;

        public abstract List<string> SearchableFields { get; }

        private Lucene.Net.Util.Version Version = Lucene.Net.Util.Version.LUCENE_30;

        private FSDirectory Directory
        {
            get
            {
                if (DirectoryTemp == null) DirectoryTemp = FSDirectory.Open(new DirectoryInfo(_luceneDir));
                if (IndexWriter.IsLocked(DirectoryTemp))
                    IndexWriter.Unlock(DirectoryTemp);
                var lockFilePath = Path.Combine(_luceneDir, "write.lock");
                if (File.Exists(lockFilePath))
                    File.Delete(lockFilePath);
                return DirectoryTemp;
            }
        }

        public void AddToIndex(T entity)
        {
            AddToIndex(new[] { entity });
        }

        public void AddToIndex(IList<T> entities)
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

        public abstract Document CreateDocument(T entity);

        public abstract T GetData(Document doc);

        public virtual List<T> Search(string term)
        {
            using (var searcher = new IndexSearcher(Directory, false))
            {
                var hitsLimit = short.MaxValue;

                using (var analyzer = new StandardAnalyzer(Version))
                {
                    var parser = new QueryParser(Version, SearchableFields.First(), analyzer);
                    var query = parser.Parse(term);
                    var hits = searcher.Search(query, hitsLimit).ScoreDocs;

                    var result = GetList(hits.Select(h => searcher.Doc(h.Doc)).ToList());

                    return result.ToList();              
                }
            }
        }

        public IList<T> GetList(IList<Document> hits)
        {
            return hits.Select(hit => GetData(hit)).ToList();
        }
    }
}
