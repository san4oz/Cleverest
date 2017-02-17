using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleverest.Business.Entities;

namespace Cleverest.Business.InterfaceDefinitions.Managers
{
    public interface IAccountManager : IBaseManager<Account>
    {
        Account GetByEmail(string email);

        IList<Account> GetAccountsByTeamId(string teamId);      
    }
}
