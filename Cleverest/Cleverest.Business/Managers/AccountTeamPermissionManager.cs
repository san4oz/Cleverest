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
    public class AccountTeamPermissionManager : BaseManager<AccountTeamPermission, IAccountTeamPermissionProvider>, IAccountTeamPermissionManager
    {
        public override void Update(AccountTeamPermission entity)
        {
            Provider.Update(entity.Id, entityToUpdate =>
            {
                entityToUpdate.AccountId = entity.AccountId;
                entityToUpdate.TeamId = entity.TeamId;
            });
        }
    }
}
