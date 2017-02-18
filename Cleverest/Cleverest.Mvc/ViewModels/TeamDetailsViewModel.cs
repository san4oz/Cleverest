using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleverest.ViewModels;

namespace Cleverest.Mvc.ViewModels
{
    public class TeamDetailsViewModel : BaseViewModel
    {
        public TeamDetailsInfoViewModel Info { get; set; }

        public IList<ProfileViewModel> Participants { get; set; }
    }
}
