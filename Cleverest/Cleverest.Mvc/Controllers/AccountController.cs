using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Cleverest.Mvc.ViewModels;
using Cleverest.Business.Entities;
using System.Web.Security;
using Cleverest.Mvc.Helpers;

namespace Cleverest.Mvc.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult MyProfile()
        {
            var account = Site.Managers.Account.GetByEmail(WebSecurity.User.Email);
            if (account == null)
                return HttpNotFound();


            var viewModel = Site.Services.Mapper.Map<Account, ProfileViewModel>(account);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyProfile(ProfileViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);


            if(!viewModel.Email.Equals(WebSecurity.User.Email) && Site.Api.Account.AccountExists(viewModel.Email))
            {
                ModelState.AddModelError("Email", "Пробачте. Користувач з такою поштою вже існує.");
                return View(viewModel);
            }

            var account = Site.Services.Mapper.Map<ProfileViewModel, Account>(viewModel);

            Site.Managers.Account.Update(account);

            return RedirectToAction("MyProfile");
        }

        [HttpGet]
        public ActionResult MyTeams()
        {
            var teams = Site.Managers.Team.GetTeamsByAccountId(WebSecurity.User.Id).ToList();

            var teamViewModels = new List<TeamViewModel>();

#warning refactor this piece of shit(write something like object[] GetTeamLookups(string teamId))
            teams.ForEach(t =>
            {
                teamViewModels.Add(new TeamViewModel() { Name = t.Name, ParticipantsCount = Site.Managers.Account.GetAccountsByTeamId(t.Id).Count() });
            });

            return View(teamViewModels);
        }

        [HttpGet]
        public ActionResult CreateTeam()
        {
            var viewModel = new TeamViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateTeam(TeamViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            if(Site.Api.Team.TeamExists(viewModel.Name))
            {
                ModelState.AddModelError("Name", "Пробачте. Команда з такою назвою вже існує.");
                return View(viewModel);
            }

            var team = new Team() { Name = viewModel.Name, OwnerId = WebSecurity.User.Id};

            Site.Managers.Team.Create(team);

            return RedirectToAction("MyTeams");
        }

        [HttpGet]
        public ActionResult MyInvitations()
        {
            var result = new List<AccountTeamRequestViewModel>();

            var requests = Site.Managers.AccountTeamRequest.GetRequestsByReceiverId(WebSecurity.User.Id).ToList();
            requests.ForEach(request =>
            {
                var viewModel = new AccountTeamRequestViewModel();
                viewModel.FromAccount = Site.Services.Mapper.Map<Account, ProfileViewModel>(Site.Managers.Account.Get(request.FromId));
                viewModel.Team = Site.Services.Mapper.Map<Team, ViewModels.TeamViewModel>(Site.Managers.Team.Get(request.TeamId));
                viewModel.Type = request.RequestType;
                viewModel.Id = request.Id;

                result.Add(viewModel);
            });

            return View(result);
        }

        [HttpPost]
        public ActionResult ProcessRequest(string requestId, bool approved)
        {
            Site.Managers.AccountTeamRequest.ProcessRequest(requestId, approved);
            return Json(true);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            var viewModel = new AccountRegisterViewModel();

            return View(viewModel);
        }

        
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(AccountRegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            if(Site.Api.Account.AccountExists(viewModel.Email))
            {
                ModelState.AddModelError("Email", "Користувач з такою поштою вже існує.");
                return View(viewModel);
            }

            var model = Site.Services.Mapper.Map<AccountRegisterViewModel, Account>(viewModel);

            Site.Managers.Account.Create(model);
            Site.Api.Account.SetAuthenticationCookie(model.Email, false);

            return RedirectToAction("List", "Game");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            var viewModel = new LoginViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var account = Site.Api.Account.Authenticate(viewModel.Email, viewModel.Password);
            if (account == null)
            {
                ModelState.AddModelError("", "Пробачте. Наша система не змогла знайти вашу електронну адресу або пароль не вірний.");
                return View(viewModel);
            }

            Site.Api.Account.SetAuthenticationCookie(account.Email, viewModel.RememberMe);

            return RedirectToAction("List", "Game");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Search(string query)
        {
            var teams = Site.Managers.Team.Search(query);

            var myTeams = Site.Managers.Team.GetTeamsByAccountId(WebSecurity.User.Id);

            var result = teams.Except(myTeams).ToList();

            var viewModel = Site.Services.Mapper.Map<IList<Team>, IList<TeamViewModel>>(result);

            return PartialView("_TeamSuggestions", viewModel);
        }

        [HttpPost]
        public ActionResult SuggestionDetails(string id)
        {
            var team = Site.Managers.Team.Get(id);

            TeamViewModel viewModel = null;

            if(team != null)
            {
                viewModel = Site.Services.Mapper.Map<Team, TeamViewModel>(team);
            }

            return PartialView("_SuggestionDetails", viewModel);
        }

        [HttpPost]
        public ActionResult SendJoinRequest(string teamId)
        {
            if (string.IsNullOrEmpty(teamId))
                return Json(false);


            var teamToJoin = Site.Managers.Team.Get(teamId);
            if (teamToJoin == null)
                return Json(false);

            var request = new AccountTeamRequest() { FromId = WebSecurity.User.Id, ToId = teamToJoin.OwnerId, RequestType = AccountTeamRequestType.Join, TeamId = teamToJoin.Id };

            Site.Managers.AccountTeamRequest.Create(request);

            return Json(true);
        }

        [HttpPost]
        public ActionResult SendInviteRequest()
        {
            return View();
        }
    }
}
