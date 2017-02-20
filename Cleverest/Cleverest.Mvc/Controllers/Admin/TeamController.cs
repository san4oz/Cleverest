using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Cleverest.Business.Entities;
using Cleverest.Business.Helpers.ImageStorageFactory;
using Cleverest.Mvc.Security;
using Cleverest.Mvc.ViewModels.Admin;

namespace Cleverest.Mvc.Controllers.Admin
{
    public class TeamController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var team = Site.Managers.Team.All().ToList();

            var viewModel = Site.Services.Mapper.Map<List<Team>, List<TeamViewModel>>(team);

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

            var model = Site.Services.Mapper.Map<TeamViewModel, Team>(viewModel);
           
            ImageStorageFactory.Current.GetStorage(SiteConstants.ImageStorages.Team)
                .SaveLogo(viewModel.Image.InputStream, viewModel.Id, Path.GetExtension(viewModel.Image.FileName));

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

            var viewModel = Site.Services.Mapper.Map<Team, TeamViewModel>(team);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(TeamViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var model = Site.Services.Mapper.Map<TeamViewModel, Team>(viewModel);

            ImageStorageFactory.Current.GetStorage(SiteConstants.ImageStorages.Team)
              .SaveLogo(viewModel.Image.InputStream, viewModel.Id, Path.GetExtension(viewModel.Image.FileName));

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
