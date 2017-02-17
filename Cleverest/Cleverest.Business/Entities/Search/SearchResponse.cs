using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest.Business.Entities.Search
{
    public class SearchResponse
    {
        public List<SearchResult> Results { get; set; }

        public SearchResponse()
        {
            Results = new List<SearchResult>();
        }
    }
}
