using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleverest.Business.Entities;

namespace Cleverest.Business.InterfaceDefinitions.Providers
{
    public interface IGameProvider : IBaseProvider<Game>
    {
        void CreateRegistrationRequest(GameRegistrationRequest request);

        IList<GameRegistrationRequest> GetRegistrationRequests(string gameId);

        void DeleteRegistrationRequests(string gameId);
    }
}
