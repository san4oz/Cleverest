using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleverest.Business.InterfaceDefinitions.Managers;
using Cleverest.Business.InterfaceDefinitions.Search;

namespace Cleverest.Business.Tasks
{
    public class TeamIndexTask : ITask
    {
        public string Name { get { return "TeamIndex"; } }

        protected ITeamManager Manager;

        protected ITeamSearchManager SearchManager;

        public TeamIndexTask(ITeamManager manager, ITeamSearchManager searcher)
        {
            this.Manager = manager;
            this.SearchManager = searcher;
        }

        public void Run()
        {
            var teams = Manager.All();
            SearchManager.AddToIndex(teams);
        }
    }
}
