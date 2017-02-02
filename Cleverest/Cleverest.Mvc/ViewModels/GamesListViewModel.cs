using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest.Mvc.ViewModels
{
    public class GamesListViewModel
    {
        public IList<GameViewModel> Games { get; set; }
        public int Page;
        public int PageSize;
    }
}
