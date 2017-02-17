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
    public class AccountTeamRequestProvider : BaseProvider<AccountTeamRequest>, IAccountTeamRequestProvider
    {
        public IList<AccountTeamRequest> GetRequestsByReceiverId(string receiverId)
        {
            return Execute(session =>
            {
                var criteria = session.CreateCriteria<AccountTeamRequest>();
                criteria.Add(Expression.Eq("ToId", receiverId));
                return criteria.List<AccountTeamRequest>().ToList();
            });
        }
    }
}
