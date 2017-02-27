using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleverest.Business.Entities;
using Cleverest.Business.Helpers;
using Cleverest.Business.Entities.Questions;

namespace Cleverest.Mvc.ViewModels.Admin.Resources
{
    public class QuestionViewModel
    {
        public int RoundNo { get; set; }
        
        public string GameId { get; set; }

        public List<Question> Questions { get; set; }

        public QuestionViewModel(List<Question> questions, int roundNo)
        {
            this.RoundNo = roundNo;

            if (questions != null && questions.Any())
                this.Questions = questions;
            else
                this.Questions = new List<Question>();

            InitializeEmptyQuestions();
        }

        protected void InitializeEmptyQuestions()
        {
            var index = this.Questions.Count();

            while(this.Questions.Count() < CleverestHelper.GetQuestionsCount(this.RoundNo))
            {
                Questions.Add(new Question() { GameId = this.GameId, RoundNo = this.RoundNo, OrderNo = ++index });
            }
        }
    }
}
