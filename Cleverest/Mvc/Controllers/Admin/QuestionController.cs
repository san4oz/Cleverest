using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Cleverest.Business.Entities;
using Cleverest.Core.Helpers;
using Cleverest.Mvc.ViewModels.Admin;

namespace Cleverest.Mvc.Controllers.Admin
{
    public class QuestionController : Controller
    {
        public ActionResult List()
        {
            var question = Site.Managers.Question.All();

            var list = Mapper.Map<IList<Questions>, IList<QuestionViewModel>>(question);

            return View(list);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var viewModel = new QuestionViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(QuestionViewModel questionModel)
        {
            if (!ModelState.IsValid)
                return View(questionModel);

            FileHelper.Save(questionModel.Image, FileHelper.GetFilePath(questionModel.Image, SiteConstants.AppSettings.ImageFolderPath));
            FileHelper.Save(questionModel.Song, FileHelper.GetFilePath(questionModel.Song, SiteConstants.AppSettings.SongFolderPath));

            var question = Mapper.Map<QuestionViewModel, Questions>(questionModel);
            question.ImageUrl = FileHelper.GetFileRelativePath(questionModel.Image, SiteConstants.AppSettings.ImageFolderPath);
            question.SongUrl = FileHelper.GetFileRelativePath(questionModel.Song, SiteConstants.AppSettings.SongFolderPath);
            Site.Managers.Question.Create(question);

            return RedirectToAction("List", "Question");
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return HttpNotFound();

            var question = Site.Managers.Question.Get(id);
            if (question == null)
                return HttpNotFound();

            var questionModel = Mapper.Map<Questions, QuestionViewModel>(question);

            return View(questionModel);
        }

        [HttpPost]
        public ActionResult Edit(QuestionViewModel questionModel)
        {
            if (!ModelState.IsValid)
                return View(questionModel);
    
            FileHelper.Save(questionModel.Image, FileHelper.GetFilePath(questionModel.Image, SiteConstants.AppSettings.ImageFolderPath));

            var question = Mapper.Map<QuestionViewModel, Questions>(questionModel);
            question.ImageUrl = FileHelper.GetFileRelativePath(questionModel.Image, SiteConstants.AppSettings.ImageFolderPath);
            Site.Managers.Question.Update(question);

            return RedirectToAction("List", "Question");
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return HttpNotFound();

            Site.Managers.Question.Delete(id);

            return Json(true);
        }
    }
}
