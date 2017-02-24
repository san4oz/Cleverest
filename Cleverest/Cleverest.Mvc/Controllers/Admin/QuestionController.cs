using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Cleverest.Business.Entities;
using Cleverest.Business.Entities.Questions;
using Cleverest.Business.Helpers;
using Cleverest.Business.Helpers.ImageStorageFactory;
using Cleverest.Business.InterfaceDefinitions.Managers;
using Cleverest.Mvc.ViewModels.Admin.Resources;

namespace Cleverest.Mvc.Controllers.Admin
{
    public class QuestionController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var model = new QuestionSelectorViewModel()
            {
                Games = Site.Managers.Game.All()
                .Select(game => new SelectListItem() { Text = game.Title, Value = game.Id }).ToList(),
                Rounds = Enumerable.Range(1, 7).Select(x => new SelectListItem { Text = x.ToString(), Value = x.ToString() }).ToList()
            };                      
            return View(model);
        }

        [HttpPost]
        public ActionResult Questions(string gameId, int roundNo)
        {
            if (gameId.IsEmpty() || roundNo <= 0)
                return RedirectToAction("Index");

            var game = Site.Managers.Game.Get(gameId);
            if (game == null)
                return HttpNotFound();

            var questions = Site.Managers.Question.Get(gameId, roundNo);

            var model = new QuestionViewModel(questions) { GameId = gameId, RoundNo = roundNo };

            return PartialView(string.Format("Types/{0}QuestionEditor", GetViewSuffix(roundNo)), model);
        }

        [HttpPost]
        public ActionResult Save(List<Question> questions, string gameId, int roundNo)
        {
            if (gameId.IsEmpty() || roundNo <= 0 || roundNo > 7)
                return RedirectToAction("Index");

            foreach(var question in questions)
            {
                question.GameId = gameId;
                question.RoundNo = roundNo;

                if (question.QuestionBody.IsEmpty() || question.CorrectAnswer.IsEmpty())
                    continue;

                var existingQuestion = Site.Managers.Question.Get(gameId, roundNo, question.OrderNo);
                if(existingQuestion != null)
                {
                    question.Id = existingQuestion.Id;
                    Site.Managers.Question.Update(question);
                }
                else
                {
                    Site.Managers.Question.Create(question);
                }
            }

            return RedirectToAction("Index");
        }

        public string GetViewSuffix(int roundNo)
        {
            var type = CleverestHelper.GetQuestionType(roundNo);

            if (type == QuestionType.Text)
                return "_Text";

            if (type == QuestionType.Picture)
                return "_Picture";

            return "_Music";
        }
    }
}
