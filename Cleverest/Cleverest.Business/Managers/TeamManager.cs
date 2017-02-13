using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Cleverest.Business.Entities;
using Cleverest.Business.InterfaceDefinitions.Managers;
using Cleverest.Business.InterfaceDefinitions.Providers;

namespace Cleverest.Business.Managers
{
    public class TeamManager : BaseManager<Team, ITeamProvider>, ITeamManager
    {
        public AccountTeamPermissionManager AccountTeamPermissionManager
        {
            get { return new AccountTeamPermissionManager(); }
        }

        public override void Update(Team entity)
        {
            Provider.Update(entity.Id, entityToUpdate =>
            {
                entityToUpdate.Name = entity.Name;
            });
        }

        public IList<Team> GetTeamsByAccountId(string accountId)
        {
            return Provider.GetTeamsByAccountId(accountId);
        }

        public Team GetTeamByName(string teamName)
        {
            return Provider.GetTeamByName(teamName);
        }

#warning decide what to do with this functionality...it's not the best practice
        public override void Create(Team entity)
        {
            base.Create(entity);

            AccountTeamPermissionManager.Create(new AccountTeamPermission() { AccountId = entity.OwnerId, TeamId = entity.Id });
        }
    }
}
