using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleverest.Business.Entities;
using Cleverest.Business.InterfaceDefinitions.Managers;
using Cleverest.Business.InterfaceDefinitions.Providers;

namespace Cleverest.Business.Managers
{
    public class ScoreManager : BaseManager<Score, IScoreProvider>, IScoreManager
    {
        public IList<Score> GetGameScores(string gameId)
        {
            return Provider.GetGameScores(gameId);
        }

        public override void Update(Score entity)
        {
            Provider.Update(entity.Id, entityToUpdate =>
            {
                entityToUpdate.TeamId = entity.TeamId;
                entityToUpdate.GameId = entity.GameId;
                entityToUpdate.RoundNo = entity.RoundNo;
                entityToUpdate.Value = entity.Value;
            });
        }
    }
}
