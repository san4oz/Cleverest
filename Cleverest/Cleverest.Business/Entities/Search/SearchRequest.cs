using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest.Business.Entities.Search
{
    public class SearchRequest
    {
        public string Keywords { get; set; }

        public string SearchField { get; set; }

        public IList<string> Fields { get; set; }

        public SearchRequest()
        {
            this.Fields = new List<string>();
        }
    }
}
