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
        protected ITeamSearchManager TeamSearchManager { get; set; }

        protected IGameProvider GameProvider { get; set; }

        protected IScoreManager ScoreManager { get; set; }

        protected IAccountProvider AccountProvider { get; set; }

        public TeamManager(ITeamSearchManager searchManager, IGameProvider gameProvider, IScoreManager scoreManager, IAccountProvider accountProvider)
        {
            this.TeamSearchManager = searchManager;
            this.GameProvider = gameProvider;
            this.ScoreManager = scoreManager;
            this.AccountProvider = accountProvider;
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

            AccountProvider.CreateAccountTeamPermission(new AccountTeamPermission() { AccountId = entity.OwnerId, TeamId = entity.Id });
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

            var game = GameProvider.Get(gameId);
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

        public IList<Team> GetRegisteredOnGameTeams(string gameId)
        {
            var game = GameProvider.Get(gameId);
            if (game == null)
                return new List<Team>();

            var accountIds = GameProvider.GetRegistrationRequests(gameId).Select(r => r.AccountId).ToList();

            var accounts = AccountProvider.GetByIdList(accountIds);
            var teamIds = accounts.Select(x => x.TeamId).Distinct().ToList();
            return GetByIdList(teamIds).ToList();
        }

        public bool RegisterTeamOnGame(string gameId, IList<string> accountIds)
        {
            if (gameId.IsEmpty() || accountIds == null || accountIds.Count() <= 0)
                return false;

            var game = GameProvider.Get(gameId);
            if (game.MaxTeamCount < accountIds.Count())
                return false;

            foreach (var accountId in accountIds)
            {
                var account = AccountProvider.Get(accountId);
                if (account == null)
                    continue;

                GameProvider.CreateRegistrationRequest(new GameRegistrationRequest()
                {
                    GameId = gameId,
                    AccountId = account.Id,
                    Date = DateTime.Now
                });
            }

            return true;
        }
    }
}
