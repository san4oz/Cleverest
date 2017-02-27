using Cleverest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest.Mvc.ViewModels.Frontend.Game
{
    public class GameRegistrationViewModel : BaseViewModel
    {
        public IList<ProfileViewModel> TeamMembers { get; set; }

        public bool IsRegistered { get; set; }

        public bool HasSeveralTeams { get; set; }

        public IList<RegisteredTeam> RegisteredTeams { get; set; }

        public GameRegistrationViewModel()
        {
            this.RegisteredTeams = new List<RegisteredTeam>();
        }

        public class RegisteredTeam
        {
            public string TeamName { get; set; }

            public int MembersCount { get; set; }
        }
    }
}
