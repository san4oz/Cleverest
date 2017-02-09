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

namespace Cleverest.Mvc.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
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

            FormsAuthentication.SetAuthCookie(account.Name, viewModel.RememberMe);

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
