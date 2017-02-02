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
    public class GameManager : BaseManager<Game, IGameProvider>, IGameManager
    {
        public override void Update(Game entity)
        {
            Provider.Update(entity.Id, entityToUpdate =>
            {
                entityToUpdate.ImageUrl = entity.ImageUrl;
                entityToUpdate.Location = entityToUpdate.Location;
                entityToUpdate.Title = entity.Title;
                entityToUpdate.GameDate = entity.GameDate;
            });
        }
    }
}
