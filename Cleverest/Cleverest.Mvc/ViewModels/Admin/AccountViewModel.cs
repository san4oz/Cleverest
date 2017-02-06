using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Cleverest.App_Start.AutoMapper;
using Cleverest.Business.Entities;
using Cleverest.ViewModels;

namespace Cleverest.Mvc.ViewModels.Admin
{
    public class AccountViewModel : BaseViewModel, IMapFrom<Account>
    {
        public string Name { get; set; }

        public string TeamId { get; set; }

        public List<SelectListItem> TeamsIds { get; set; }

        public AccountViewModel()
        {
            TeamsIds = new List<SelectListItem>();
        }
    }
}
