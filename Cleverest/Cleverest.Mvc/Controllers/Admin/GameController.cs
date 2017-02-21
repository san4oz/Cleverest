using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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
    public class GameController : AdminEntityEditorController<Game, GameInputViewModel>
    {
        protected override ImageStorage ContentStorage
        {
            get
            {
                return ImageStorageFactory.Current.GetStorage(SiteConstants.ImageStorages.Game);
            }
        }

        protected override IBaseManager<Game> DataManager
        {
            get
            {
                return Site.Managers.Game;
            }
        }

        public override ActionResult Create(GameInputViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Editor", model);

            ContentStorage.SaveLogo(model.Image.InputStream, model.Id, model.Image.FileName);

            return base.Create(model);
        }

        public override ActionResult Edit(GameInputViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Editor", model);

            ContentStorage.SaveLogo(model.Image.InputStream, model.Id, model.Image.FileName);

            return base.Edit(model);
        }
    }
}
