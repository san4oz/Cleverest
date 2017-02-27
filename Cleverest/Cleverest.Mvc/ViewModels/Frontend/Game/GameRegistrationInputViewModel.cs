using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest.Mvc.ViewModels.Frontend.Game
{
    public class GameRegistrationInputViewModel
    {
        public List<Account> Accounts { get; set; }

        public class Account
        {
            public string Id { get; set; }

            public bool Checked;
        }
    }    
}
