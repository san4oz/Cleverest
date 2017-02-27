using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleverest.App_Start.AutoMapper;
using Cleverest.Business.Entities;
using Cleverest.ViewModels;

namespace Cleverest.Mvc.ViewModels
{
    public class AccountTeamRequestViewModel : BaseViewModel
    {
        public ProfileViewModel FromAccount { get; set; }

        public TeamViewModel Team { get; set; }

        public AccountTeamRequestType Type { get; set; }
    }
}
