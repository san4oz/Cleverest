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

        public void CreateAccountTeamPermission(AccountTeamPermission permission)
        {
            Execute(session =>
            {
                session.SaveOrUpdate(permission);
                session.Flush();
            });
        }

        public IList<Account> GetAccountsByTeamId(string teamId)
        {
            return Execute(session =>
            {
                Account accountAlias = null;

                return session.QueryOver<Account>(() => accountAlias)
                    .WithSubquery.WhereProperty(ac => ac.Id)
                    .In(QueryOver.Of<AccountTeamPermission>()
                    .Where(atp => atp.TeamId == teamId)
                    .Select(atp => atp.AccountId))
                    .List();             
            });
        }

        public IList<AccountTeamRequest> GetRequestsByReceiverId(string receiverId)
        {
            return Execute(session =>
            {
                var criteria = session.CreateCriteria<AccountTeamRequest>();
                criteria.Add(Expression.Eq("ToId", receiverId));
                return criteria.List<AccountTeamRequest>().ToList();
            });
        }

        public AccountTeamRequest GetAccountTeamRequest(string requestId)
        {
            return Execute(session =>
            {
                return session.Get<AccountTeamRequest>(requestId);
            });
        }

        public void DeleteAccountTeamRequest(string requestId)
        {
            Execute(session =>
            {
                var valueToRemove = session.Get<AccountTeamRequest>(requestId);
                if (valueToRemove != null)
                    session.Delete(valueToRemove);
            });
        }

        public void CreateAccountTeamRequest(AccountTeamRequest request)
        {
            Execute(session =>
            {
                session.Save(request);
            });
        }
    }
}
