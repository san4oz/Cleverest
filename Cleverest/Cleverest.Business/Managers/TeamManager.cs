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
    public class TeamManager : BaseManager<Team, ITeamProvider>, ITeamManager
    {
        public override void Update(Team entity)
        {
            Provider.Update(entity.Id, entityToUpdate =>
            {
                entityToUpdate.Name = entity.Name;
                entityToUpdate.PhoneNumber = entity.PhoneNumber;
                entityToUpdate.SocialNetworkLink = entity.SocialNetworkLink;
            });
        }
    }
}
