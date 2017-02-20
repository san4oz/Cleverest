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
    public class AccountTeamRequestManager : BaseManager<AccountTeamRequest, IAccountTeamRequestProvider>, IAccountTeamRequestManager
    {
        protected IAccountTeamPermissionManager AccountTeamPermissionManager { get; private set; }

        public AccountTeamRequestManager(IAccountTeamPermissionManager accountTeamPermissionsManager)
        {
            this.AccountTeamPermissionManager = accountTeamPermissionsManager;
        }

        public IList<AccountTeamRequest> GetRequestsByReceiverId(string receiverId)
        {
            return Provider.GetRequestsByReceiverId(receiverId);
        }

        public void ProcessRequest(string requestId, bool isApproved)
        {
            if (string.IsNullOrEmpty(requestId))
                return;

            if (!isApproved)
            { 
                Provider.Delete(requestId);
                return;
            }

            var request = Provider.Get(requestId);
            if (requestId == null)
                return;

            var accountTeamPermission = new AccountTeamPermission()
            {
                AccountId = request.FromId,
                TeamId = request.TeamId
            };

            AccountTeamPermissionManager.Create(accountTeamPermission);
            Provider.Delete(requestId);
        }

        public override void Update(AccountTeamRequest entity)
        {
            Provider.Update(entity.Id, entityToUpdate =>
            {
                entityToUpdate.FromId = entity.FromId;
                entityToUpdate.ToId = entity.ToId;
                entityToUpdate.TeamId = entity.TeamId;
                entityToUpdate.RequestType = entity.RequestType;
            });
        }


    }
}
