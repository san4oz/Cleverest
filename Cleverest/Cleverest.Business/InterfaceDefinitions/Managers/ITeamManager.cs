using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleverest.Business.Entities;

namespace Cleverest.Business.InterfaceDefinitions.Managers
{
    public interface ITeamManager : IBaseManager<Team>
    {
        IList<Team> GetTeamsByAccountId(string accountId);

        Team GetTeamByName(string teamName);

        IList<Team> Search(string query);

        Team GetGameWinner(string gameId);

        IList<Team> GetRegisteredOnGameTeams(string gameId);

        bool RegisterTeamOnGame(string gameId, IList<string> accountIds);
    }
}
