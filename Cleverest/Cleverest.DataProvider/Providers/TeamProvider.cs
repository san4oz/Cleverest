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
    public class TeamProvider : BaseProvider<Team>, ITeamProvider
    {
        public IList<Team> GetTeamsByAccountId(string accountId)
        {
            return Execute(session =>
            {
                Team teamAlias = null;

                return session.QueryOver<Team>(() => teamAlias)
                    .WithSubquery.WhereProperty(t => t.Id)
                    .In(QueryOver.Of<AccountTeamPermission>()
                        .Where(atp => atp.AccountId == accountId)
                        .Select(atp => atp.TeamId)).List<Team>().ToList();
            });
        }

        public Team GetTeamByName(string teamName)
        {
            return Execute(session =>
            {
                var criteria = session.CreateCriteria<Team>();
                criteria.Add(Expression.Eq("Name", teamName));
                return criteria.UniqueResult<Team>();
            });
        }        
    }
}
