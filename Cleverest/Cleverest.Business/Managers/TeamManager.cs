using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Cleverest.Business.Entities;
using Cleverest.Business.Entities.Search;
using Cleverest.Business.InterfaceDefinitions.Managers;
using Cleverest.Business.InterfaceDefinitions.Providers;
using Cleverest.Business.InterfaceDefinitions.Search;

namespace Cleverest.Business.Managers
{
    public class TeamManager : BaseManager<Team, ITeamProvider>, ITeamManager
    {
        protected IAccountTeamPermissionManager AccountTeamPermissionManager { get; set; }

        protected ITeamSearchManager TeamSearchManager { get; set; }

        protected IGameManager GameManager { get; set; }

        protected IScoreManager ScoreManager { get; set; }

        public TeamManager(IAccountTeamPermissionManager permissionsManager, ITeamSearchManager searchManager, IGameManager gameManager, IScoreManager scoreManager)
        {
            this.AccountTeamPermissionManager = permissionsManager;
            this.TeamSearchManager = searchManager;
            this.GameManager = gameManager;
            this.ScoreManager = scoreManager;
        }

        public override void Update(Team entity)
        {
            Provider.Update(entity.Id, entityToUpdate =>
            {
                entityToUpdate.Name = entity.Name;
                entityToUpdate.Description = entity.Description;
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

        public IList<Team> Search(string query)
        {
            var request = new SearchRequest();
            request.Keywords = query;

            var response = TeamSearchManager.Search(request);

            var ids = response.Results.Select(r => r.Id).ToList();

            return GetByIdList(ids);
        }

        public Team GetGameWinner(string gameId)
        {
            if (gameId.IsEmpty())
                return null;

            var game = GameManager.Get(gameId);
            if (game == null)
                return null;

            if (game.GameDate > DateTime.Now)
                return null;

            var scores = ScoreManager.GetGameScores(gameId);
            if (!scores.Any())
                return null;

            var winnerTeamId = scores.GroupBy(score => score.TeamId)
                .OrderByDescending(gr => gr.Sum(score => score.Value))
                .First().Select(gr => gr.TeamId).First();

            return Get(winnerTeamId);
        }
    }
}
