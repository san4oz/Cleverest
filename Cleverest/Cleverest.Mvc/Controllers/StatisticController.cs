using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Cleverest.Business.Entities;
using Cleverest.Business.Helpers.ImageStorageFactory;
using Cleverest.Business.Helpers.ImageStorageFactory.Storages;
using Cleverest.Mvc.ViewModels.Frontend.Statistic;
using static Cleverest.Mvc.ViewModels.Frontend.Statistic.BestResultsModel;

namespace Cleverest.Mvc.Controllers
{
    public class StatisticController : Controller
    {
        public ActionResult Index()
        {
            var model = new StatisticViewModel()
            {
                GeneralCounters = CreateGeneralCountersModel(),
                BestResults = CreateBestResultsModel()
            };

            return View(model);
        }

        protected GeneralCountersModel CreateGeneralCountersModel()
        {
            var model = new GeneralCountersModel();

            model.TotalGameCount = Site.Managers.Game.All().Count();
            model.TotalPeopleCount = Site.Managers.Account.All().Count();
            model.TotalQuestionsCount = Site.Managers.Question.All().Count();

            return model;
        }

        protected BestResultsModel CreateBestResultsModel()
        {
            var model = new BestResultsModel();

            model.TheMostSuccessful = CreateTheMostSuccessfulTeamModel();
            model.TheMostRegularVisitors = CreateTheMostRegularVisitorsTeamModel();
            model.TheFunniestOne = CreateUnknownModel();

            return model;
        }

        protected TeamInfo CreateTheMostSuccessfulTeamModel()
        {
            var games = Site.Managers.Game.All();
            Dictionary<Team, int> scoreTable = new Dictionary<Team, int>();
            foreach (var game in games)
            {
                IncrementTeamCounter(scoreTable, Site.Managers.Team.GetGameWinner(game.Id));
            }

            if (!scoreTable.Any())
                return CreateUnknownModel();

            var resultTeam = scoreTable.OrderBy(r => r.Value).First();

            return new TeamInfo()
            {
                Name = resultTeam.Key.Name,
                Score = resultTeam.Value,
                ImageSrc = ImageStorageFactory.Current.GetStorage(SiteConstants.ImageStorages.Team).GetLogo(resultTeam.Key.Id).ApplicationRelativePath
            };
        }

        protected TeamInfo CreateTheMostRegularVisitorsTeamModel()
        {
            var scoreTable = new Dictionary<Team, int>();

            var games = Site.Managers.Game.All();

            foreach(var game in games)
            {
                var teams = Site.Managers.Team.GetRegisteredOnGameTeams(game.Id);
                foreach(var team in teams)
                {
                    IncrementTeamCounter(scoreTable, team);
                }
            }

            if (!scoreTable.Any())
                return CreateUnknownModel();

            var resultTeam = scoreTable.OrderBy(r => r.Value).First();

            return new TeamInfo()
            {
                Name = resultTeam.Key.Name,
                Score = resultTeam.Value,
                ImageSrc = ImageStorageFactory.Current.GetStorage(SiteConstants.ImageStorages.Team).GetLogo(resultTeam.Key.Id).ApplicationRelativePath
            };
        }

        private TeamInfo CreateUnknownModel()
        {
            var model = new TeamInfo();

            model.Name = "Невизначено";
            model.ImageSrc = ImageStorageFactory.Current.GetStorage(SiteConstants.ImageStorages.Team).GetLogo(Guid.NewGuid().ToString()).ApplicationRelativePath;
            model.Score = 0;

            return model;
        }

        private void IncrementTeamCounter(Dictionary<Team, int> scoreTable, Team  team)
        {
            if(scoreTable.ContainsKey(team))
            {
                scoreTable[team]++;
            }
            else
            {
                scoreTable[team] = 1;
            }
        }
    }
}
