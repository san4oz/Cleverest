using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleverest.Business.Entities;

namespace Cleverest.Business.Import
{
    public class ScoreValidator
    {
        protected IList<Func<Score, bool>> Rules = new List<Func<Score, bool>>();

        public bool IsScoreValid(Score score)
        {
            return Rules.All(rule => rule(score));
        }

        public void AddRule(Func<Score, bool> rule)
        {
            Rules.Add(rule);
        }
    }
}
