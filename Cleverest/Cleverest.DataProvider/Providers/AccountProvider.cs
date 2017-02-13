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
    public class AccountProvider : BaseProvider<Account>, IAccountProvider
    {
        public Account GetByEmail(string email)
        {
            return Execute(session =>
            {
                var criteria = session.CreateCriteria<Account>();
                criteria.Add(Expression.Eq("Email", email));
                return criteria.UniqueResult<Account>();
            });
        }

        public IList<Account> GetAccountsByTeamId(string teamId)
        {
            return Execute(session =>
            {
                Account accountAlias = null;

                return session.QueryOver<Account>(() => accountAlias)
                    .WithSubquery.WhereAll(ac => ac.Id ==
                        QueryOver.Of<AccountTeamPermission>()
                        .Where(atp => atp.TeamId == teamId)
                        .Select(atp => atp.AccountId)
                        .As<string>())
                        .List<Account>().ToList();               
            });
        }
    }
}
