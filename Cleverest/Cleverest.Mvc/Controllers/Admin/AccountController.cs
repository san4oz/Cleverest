using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Cleverest.Business.Entities;
using Cleverest.Mvc.ViewModels.Admin;

namespace Cleverest.Mvc.Controllers.Admin
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var accounts = Site.Managers.Account.All().ToList();

            var viewModel = Mapper.Map<List<Account>, List<AccountViewModel>>(accounts);

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = new AccountViewModel();

            var teams = Site.Managers.Team.All().ToList();

            teams.ForEach(t =>
            {
                viewModel.TeamsIds.Add(new SelectListItem() {  Text = t.Name, Value = t.Id });
            });

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(AccountViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var model = Mapper.Map<AccountViewModel, Account>(viewModel);
           
            Site.Managers.Account.Create(model);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return HttpNotFound();

            var account = Site.Managers.Account.Get(id);
            if (account == null)
                return HttpNotFound();

            var viewModel = Mapper.Map<Account, AccountViewModel>(account);

            var teams = Site.Managers.Team.All().ToList();

            teams.ForEach(t =>
            {
                viewModel.TeamsIds.Add(new SelectListItem() { Text = t.Name, Value = t.Id });
            });

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(AccountViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var model = Mapper.Map<AccountViewModel, Account>(viewModel);

            Site.Managers.Account.Update(model);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return Json(false);


            var account = Site.Managers.Account.Get(id);
            if (account == null)
                return Json(false);

            Site.Managers.Account.Delete(id);

            return Json(true);
        }
    }
}
