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

            var teamIds = accounts.Select(ac => ac.TeamId).ToList();

            var teams = Site.Managers.Team.GetByIdList(teamIds);

            var viewModelList = accounts.Select(account =>
            {
                return new AccountListViewModel()
                {
                    Id = account.Id,
                    Email = account.Email,
                    Name = account.Name,
                    TeamName = teams.FirstOrDefault(t => t.Id.Equals(account.TeamId)) != null ? teams.FirstOrDefault(t => t.Id.Equals(account.TeamId)).Name : string.Empty
                };
            }).ToList();

            return View(viewModelList);
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

            if (Site.Api.Account.AccountExists(viewModel.Email))
            {
                ModelState.AddModelError("Email", "Користувач з такою поштою вже існує.");
                return View(viewModel);
            }

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
