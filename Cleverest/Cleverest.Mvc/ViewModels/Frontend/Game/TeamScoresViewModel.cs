using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleverest.Business.Entities;

namespace Cleverest.Mvc.ViewModels.Frontend.Game
{
    public class TeamScoresViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Dictionary<int, decimal> Scores { get; set; }

        public TeamScoresViewModel(Team team, IList<Score> scores)
        {
            this.Id = team.Id;
            this.Name = team.Name;
            this.Scores = new Dictionary<int, decimal>();

            foreach (var score in scores)
                this.Scores.Add(score.RoundNo, score.Value);
        }
    }
}
