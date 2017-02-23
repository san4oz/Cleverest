using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleverest.ViewModels;

namespace Cleverest.Mvc.ViewModels.Frontend.Game
{
    public class GameDetailsViewModel : BaseViewModel
    {
        public GameInfoViewModel Info { get; set; }

        public IList<TeamScoresViewModel> TeamScores { get; set; }

        public TeamDetailsInfoViewModel Winner { get; set; }

        public GameDetailsViewModel()
        {
            TeamScores = new List<TeamScoresViewModel>();
        }
    }
}
