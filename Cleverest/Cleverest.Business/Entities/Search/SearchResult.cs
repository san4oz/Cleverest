using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest.Business.Entities.Search
{
    public class SearchResult
    {
        public string Id { get; set; }

        public float Score { get; set; }

        public Dictionary<string, object> Fields { get; set; }

        public object GetValueOrDefault(string fieldName)
        {
            object value = null;
            Fields.TryGetValue(fieldName, out value);
            return value;
        }

        public SearchResult()
        {
            this.Fields = new Dictionary<string, object>();
        }
    }
}
