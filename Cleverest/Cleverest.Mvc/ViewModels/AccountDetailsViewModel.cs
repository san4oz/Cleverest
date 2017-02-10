using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleverest.ViewModels;

namespace Cleverest.Mvc.ViewModels
{
    public class AccountDetailsViewModel : BaseViewModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string TeamId { get; set; }
    }
}
