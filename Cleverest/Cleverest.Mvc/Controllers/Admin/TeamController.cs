using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Cleverest.Business.Entities;
using Cleverest.Mvc.Helpers;
using Cleverest.Mvc.ViewModels.Admin;

namespace Cleverest.Mvc.Controllers.Admin
{
    public class TeamController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var team = Site.Managers.Team.All().ToList();

            var viewModel = Mapper.Map<List<Team>, List<TeamViewModel>>(team);

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new TeamViewModel());
        }

        [HttpPost]
        public ActionResult Create(TeamViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var model = Mapper.Map<TeamViewModel, Team>(viewModel);

#warning danger. I'm not sure this functionality is the right choise
            model.OwnerId = WebSecurity.User.Id;

            Site.Managers.Team.Create(model);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return HttpNotFound();

            var team = Site.Managers.Team.Get(id);
            if (team == null)
                return HttpNotFound();

            var viewModel = Mapper.Map<Team, TeamViewModel>(team);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(TeamViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var model = Mapper.Map<TeamViewModel, Team>(viewModel);

            Site.Managers.Team.Update(model);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return Json(false);


            var team = Site.Managers.Team.Get(id);
            if (team == null)
                return Json(false);

            Site.Managers.Team.Delete(id);

            return Json(true);
        }
    }
}
