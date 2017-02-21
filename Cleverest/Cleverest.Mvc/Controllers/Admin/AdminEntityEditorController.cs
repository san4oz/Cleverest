using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Cleverest.Business.Entities;
using Cleverest.Business.Helpers.ImageStorageFactory;
using Cleverest.Business.InterfaceDefinitions.Managers;
using Cleverest.Mvc.ViewModels.Admin;

namespace Cleverest.Mvc.Controllers.Admin
{
    [Authorize]
    public abstract class AdminEntityEditorController<TEntity, TInputModel> : Controller
        where TEntity : Entity
        where TInputModel : BaseInputViewModel, new()
    {
        protected abstract IBaseManager<TEntity> DataManager { get; }

        protected abstract ImageStorage ContentStorage { get; }

        protected virtual IList<TEntity> GetEntities()
        {
            return DataManager.All();
        }

        [HttpGet]
        public virtual ActionResult Index()
        {
            var model = new EntityListViewModel<TEntity>
            {
                Entities = GetEntities()
            };

            return View(model);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View("Editor", new TInputModel() { IsCreateModel = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(TInputModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var entity = Site.Services.Mapper.Map<TInputModel, TEntity>(model);
            DataManager.Create(entity);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public virtual ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return HttpNotFound();

            var entity = DataManager.Get(id);
            if (entity == null)
                return HttpNotFound();

            var model = Site.Services.Mapper.Map<TEntity, TInputModel>(entity);

            return View("Editor", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(TInputModel model)
        {
            if (!ModelState.IsValid)
                return View("Editor", model);

            var entity = Site.Services.Mapper.Map<TInputModel, TEntity>(model);
            DataManager.Update(entity);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public virtual ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return Json(false);

            DataManager.Delete(id);

            return Json(true);
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase[] files, string entityId)
        {
            foreach(var file in files)
            {
                ContentStorage.SaveImage(file.InputStream, entityId, file.FileName);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Media(string entityId)
        {
            var images = ContentStorage.GetImages(entityId).Select(x => x.ApplicationRelativePath).ToList();
            return View(images);
        }
    }
}
