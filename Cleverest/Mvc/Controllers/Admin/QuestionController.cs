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
            var questions = Site.Managers.Question.All();

            var list = Mapper.Map<IList<Questions>, IList<QuestionViewModel>>(questions);

            return View(list);
        }

        [HttpGet]
        public ActionResult Create(string GameId, int RoundNo = 1)
        {
            if (RoundNo == 8)
                return RedirectToAction("List", "Question");
            var viewModel = new List<QuestionViewModel>();
            var model = new QuestionViewModel();
            var questionCount = (RoundNo == 7) ? 10 : 7;
            for (int i = 0; i < questionCount; i++)
            {
                model.RoundNo = RoundNo;
                model.GameId = GameId;
                viewModel.Add(model);
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(List<QuestionViewModel> questionViewModel)
        {
            if (!ModelState.IsValid)
                return View(questionViewModel);
            foreach (var model in questionViewModel)
            {
                var question = Mapper.Map<QuestionViewModel, Questions>(model);
                Site.Managers.Question.Create(question);
            }
            return RedirectToAction("Create", "Question", new { GameID = questionViewModel[0].GameId, RoundNo = questionViewModel[0].RoundNo + 1 });
        }

        [HttpGet]
        public ActionResult Edit(string id, string GameId, int RoundNo)
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

            var question = Mapper.Map<QuestionViewModel, Questions>(questionModel);
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
