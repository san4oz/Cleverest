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


            var viewModel = Mapper.Map<Account, ProfileViewModel>(account);

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

            var account = Mapper.Map<ProfileViewModel, Account>(viewModel);

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

            var model = Mapper.Map<AccountRegisterViewModel, Account>(viewModel);

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
    }
}
