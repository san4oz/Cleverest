using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Cleverest.Business.Entities;
using Cleverest.Business.Helpers.ImageStorageFactory;
using Cleverest.Mvc.Security;
using Cleverest.Mvc.ViewModels;
using Cleverest.Mvc.ViewModels.Frontend.Game;
using static Cleverest.Mvc.ViewModels.Frontend.Game.GameRegistrationViewModel;

namespace Cleverest.Mvc.Controllers
{
    public class GameController : Controller
    {
        public const int PageSize = 10;

        public ActionResult List(int page = 0)
        {
            var games = Site.Managers.Game.All();

            var viewModel = new GamesListViewModel()
            {
                Games = Site.Services.Mapper.Map<IList<Game>, IList<GameViewModel>>(games).OrderBy(m => m.GameDate).ToList(),
                Page = page,
                PageSize = PageSize
            };

            viewModel.Games.ToList().ForEach(game =>
            {
                var imgUrl = ImageStorageFactory.Current.GetStorage(SiteConstants.ImageStorages.Game).GetLogo(game.Id);
                game.ImageUrl = imgUrl == null ? null : imgUrl.ApplicationRelativePath;
            });
                            
            return View(viewModel);
        }
        
        [HttpGet]
        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
                return HttpNotFound();

            var game = Site.Managers.Game.Get(id);
            if (game == null)
                return HttpNotFound();

            var model = new GameDetailsViewModel()
            {
                Info = CreateInfoModel(game),
                TeamScores = CreateScoresModel(game),
                Winner = CreateWinnerModel(game),
                RegistrationModel = CreateRegistrationModel(game)
            };
            
            return View(model);
        }

        [HttpGet]
        public ActionResult Questions(string gameId, int roundNo, int orderNo)
        {
            if(orderNo > 7)
            {
                orderNo = 1;
                roundNo++;
            }
            if(orderNo < 1)
            {
                orderNo = 7;
                roundNo--;
            }

            var question = Site.Managers.Question.Get(gameId, roundNo, orderNo);
            if (question == null)
                return HttpNotFound();
           
            var model = Site.Services.Mapper.Map<Question, QuestionViewModel>(question);

            var nextQuestion = Site.Managers.Question.Get(gameId, roundNo, orderNo + 1);
            var previousQuestion = Site.Managers.Question.Get(gameId, roundNo, orderNo - 1);

            model.NextQuestionExists = nextQuestion != null;
            model.PreviousQuestionExists = previousQuestion != null;

            return View(model);
        }

        [HttpPost]
        public ActionResult RegisterTeam(string gameId, GameRegistrationInputViewModel model)
        {
            var game = Site.Managers.Game.Get(gameId);
            if (game == null)
                return HttpNotFound();
           
            Site.Managers.Team.RegisterTeamOnGame(gameId, model.Accounts.Where(a => a.Checked).Select(a => a.Id).ToList());

            return RedirectToAction("Details", new { Id = gameId });
        }
        
        protected GameInfoViewModel CreateInfoModel(Game game)
        {
            var storage = ImageStorageFactory.Current.GetStorage(SiteConstants.ImageStorages.Game);
            var logo = storage.GetLogo(game.Id);

            var model = new GameInfoViewModel()
            {
                Id = game.Id,
                Title = game.Title,
                Location = game.Location,
                GameDate = game.GameDate,
                ImageUrl = ImageStorageFactory.Current.GetStorage(SiteConstants.ImageStorages.Game).GetLogo(game.Id)?.ApplicationRelativePath
            };

            model.GalleryPhotos = storage.GetImages(game.Id).Select(x => x.ApplicationRelativePath).ToList();

            return model;
        }   
        
        protected IList<TeamScoresViewModel> CreateScoresModel(Game game)
        {
            var result = new List<TeamScoresViewModel>();

            var scores = Site.Managers.Score.GetGameScores(game.Id).GroupBy(score => score.TeamId);

            foreach(var teamScores in scores)
            {
                var team = Site.Managers.Team.Get(teamScores.Key);
                var model = new TeamScoresViewModel(team, teamScores.ToList());

                result.Add(model);
            }                       

            return result;
        }    

        protected TeamDetailsInfoViewModel CreateWinnerModel(Game game)
        {
            var entity = Site.Managers.Team.GetGameWinner(game.Id);
            if (entity == null)
                return null;

            var model = Site.Services.Mapper.Map<Team, TeamDetailsInfoViewModel>(entity);
            model.PhotoPath = Site.Services.ContentStorage.Team.GetLogo(entity.Id)?.ApplicationRelativePath;

            return model;
        }        

        protected GameRegistrationViewModel CreateRegistrationModel(Game game)
        {
            var model = new GameRegistrationViewModel();

            if (!User.Identity.IsAuthenticated)
                return model;

            var currentAccount = Site.Managers.Account.GetByEmail(WebSecurity.User.Email);
            if (currentAccount == null)
                return model;

            var accountTeams = Site.Managers.Team.GetTeamsByAccountId(currentAccount.Id);
            if(accountTeams.Count() > 1)
            {
                model.HasSeveralTeams = true;
            }

            var registeredTeams = Site.Managers.Team.GetRegisteredOnGameTeams(game.Id);
            if(registeredTeams.Select(t => t.Id).Contains(currentAccount.TeamId))
            {
                model.IsRegistered = true;

                foreach(var team in registeredTeams)
                {
                    var registeredTeam = new RegisteredTeam();
                    registeredTeam.TeamName = team.Name;

                    registeredTeam.MembersCount =  Site.Managers.Game.GetRegistrationRequests(game.Id)
                        .Select(r => Site.Managers.Account.Get(r.AccountId))
                        .Where(a => a.TeamId.Equals(team.Id, StringComparison.InvariantCultureIgnoreCase))
                        .Count();

                    model.RegisteredTeams.Add(registeredTeam);
                }

                return model;
            }

            var teamAccounts = Site.Managers.Account.GetAccountsByTeamId(currentAccount.TeamId);

            var profiles = Site.Services.Mapper.Map<IList<Account>, IList<ProfileViewModel>>(teamAccounts);

            foreach (var profile in profiles)
                profile.PhotoPath = Site.Services.ContentStorage.Account.GetLogo(profile.Id)?.ApplicationRelativePath;

            model.TeamMembers = profiles;

            return model;
        }
    }
}
