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
    public class GameProvider : BaseProvider<Game>, IGameProvider
    {
        public void CreateRegistrationRequest(GameRegistrationRequest request)
        {
            Execute(session =>
            {
                session.Save(request);
                session.Flush();
            });
        }

        public IList<GameRegistrationRequest> GetRegistrationRequests(string gameId)
        {
            return Execute(session =>
            {
                return session.CreateCriteria<GameRegistrationRequest>()
                            .Add(Expression.Eq("GameId", gameId))
                            .List<GameRegistrationRequest>()
                            .ToList();
            });
        }
    }
}
