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
using Cleverest.Business.InterfaceDefinitions.Managers;
using Cleverest.Mvc.Security;
using Cleverest.Mvc.ViewModels.Admin;
using Cleverest.Mvc.ViewModels.Admin.Resources;

namespace Cleverest.Mvc.Controllers.Admin
{
    public class TeamController : AdminEntityEditorController<Team, TeamInputViewModel>
    {
        protected override ImageStorage ContentStorage
        {
            get
            {
                return ImageStorageFactory.Current.GetStorage(SiteConstants.ImageStorages.Team);
            }
        }

        protected override IBaseManager<Team> DataManager
        {
            get
            {
                return Site.Managers.Team;
            }
        }

        public override ActionResult Create(TeamInputViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Editor", model);

            if(model.Image != null)
                ContentStorage.SaveLogo(model.Image.InputStream, model.Id, model.Image.FileName);

            model.OwnerId = WebSecurity.User.Id;

            return base.Create(model);
        }

        public override ActionResult Edit(TeamInputViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Editor", model);

            if(model.Image != null)
                ContentStorage.SaveLogo(model.Image.InputStream, model.Id, model.Image.FileName);

            return base.Edit(model);
        }
    }
}
