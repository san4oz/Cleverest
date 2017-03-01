using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Analysis.Tokenattributes;

namespace Cleverest.Search
{
    public static class SearchUtils
    {
        public const string WildcardTokenSymbol = "*";

        public static string MakeFullWildcardQuery(string query)
        {
            return MakeSpecificQuery(query, WildcardTokenSymbol, WildcardTokenSymbol);
        }

        public static string MakeTrailingWildCardQuery(string query)
        {
            return MakeSpecificQuery(query, string.Empty, WildcardTokenSymbol);
        }

        private static string MakeSpecificQuery(string query, string tokenBeginning, string tokenEndinng)
        {
            if (string.IsNullOrEmpty(query))
                return null;

            query = query.Trim().ToLowerInvariant();

            using (var stringReader = new StringReader(query))
            {
                var tokenizer = new SearchTokenizer(stringReader);


                var termAttribute = tokenizer.AddAttribute<ITermAttribute>();

                string result = string.Empty;

                while (tokenizer.IncrementToken())
                {
                    string word = termAttribute.Term;
                    result = string.Format("{0} {1}{2}{3}", result, tokenBeginning, word, tokenEndinng);
                }

                return result.TrimStart(' ');
            }
        }
    }
}
