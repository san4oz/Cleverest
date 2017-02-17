using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleverest.Business.Entities;

namespace Cleverest.Business.InterfaceDefinitions.Managers
{
    public interface IAccountTeamRequestManager : IBaseManager<AccountTeamRequest>
    {
        IList<AccountTeamRequest> GetRequestsByReceiverId(string receiverId);

        void ProcessRequest(string requestId, bool isApproved);
    }
}
