using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleverest.Business.Entities;

namespace Cleverest.Business.InterfaceDefinitions.Providers
{
    public interface IAccountTeamRequestProvider : IBaseProvider<AccountTeamRequest>
    {
        IList<AccountTeamRequest> GetRequestsByReceiverId(string receiverId);
    }
}
