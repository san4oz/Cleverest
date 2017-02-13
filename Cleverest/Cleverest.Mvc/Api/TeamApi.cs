using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest.Mvc.Api
{
    public class TeamApi
    {
        public bool TeamExists(string teamName)
        {
            return Site.Managers.Team.GetTeamByName(teamName) != null;
        }
    }
}
