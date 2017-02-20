using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest.Mvc.ViewModels.Admin
{
    public class EntityListViewModel<T>
    {
        public IList<T> Entities { get; set; }
    }
}
