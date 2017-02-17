using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Analysis;

namespace Cleverest.Search
{
    public class SearchTokenizer : CharTokenizer
    {
        private static readonly List<char> symbolChars = new List<char>(new char[] { '_' });

        protected override bool IsTokenChar(char c)
        {
            return Char.IsLetterOrDigit(c) || symbolChars.Contains(c);
        }

        protected override char Normalize(char c)
        {
            return Char.ToLower(c, CultureInfo.InvariantCulture);
        }

        public SearchTokenizer(TextReader reader)
            :base(reader)
        {

        }
    }
}
