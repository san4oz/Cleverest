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
using Cleverest.Mvc.Security;

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


            if(!viewModel.Email.Equals(WebSecurity.User.Email) && WebSecurity.AccountExists(viewModel.Email))
            {
                ModelState.AddModelError("Email", "Пробачте. Користувач з такою поштою вже існує.");
                return View(viewModel);
            }

            var account = Site.Services.Mapper.Map<ProfileViewModel, Account>(viewModel);

            Site.Managers.Account.Update(account);

            return RedirectToAction("MyProfile");
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

            if(WebSecurity.AccountExists(viewModel.Email))
            {
                ModelState.AddModelError("Email", "Користувач з такою поштою вже існує.");
                return View(viewModel);
            }

            var model = Site.Services.Mapper.Map<AccountRegisterViewModel, Account>(viewModel);

            Site.Managers.Account.Create(model);
            WebSecurity.SetAuthenticationCookie(model.Email, false);

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

            var account = WebSecurity.Authenticate(viewModel.Email, viewModel.Password);
            if (account == null)
            {
                ModelState.AddModelError("", "Пробачте. Наша система не змогла знайти вашу електронну адресу або пароль не вірний.");
                return View(viewModel);
            }

            WebSecurity.SetAuthenticationCookie(account.Email, viewModel.RememberMe);

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
