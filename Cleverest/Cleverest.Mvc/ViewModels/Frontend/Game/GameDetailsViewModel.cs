using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleverest.ViewModels;
using Cleverest.Business.Entities;

namespace Cleverest.Mvc.ViewModels.Frontend.Game
{
    public class GameDetailsViewModel : BaseViewModel
    {
        public GameInfoViewModel Info { get; set; }

        public IList<TeamScoresViewModel> TeamScores { get; set; }

        public TeamDetailsInfoViewModel Winner { get; set; }

        public GameRegistrationViewModel RegistrationModel { get; set; }

        public GameDetailsViewModel()
        {
            TeamScores = new List<TeamScoresViewModel>();
        }
    }
}
