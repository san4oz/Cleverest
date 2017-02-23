using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleverest.Business.Entities;
using Cleverest.Business.InterfaceDefinitions.Providers;
using NHibernate.Criterion;

namespace Cleverest.DataProvider.Providers
{
    public class ScoreProvider : BaseProvider<Score>, IScoreProvider
    {
        public IList<Score> GetGameScores(string gameId)
        {
            return Execute(session =>
            {
                return session.CreateCriteria<Score>()
                            .Add(Expression.Eq("GameId", gameId))
                                .List<Score>().ToList();
            });
        }
    }
}
