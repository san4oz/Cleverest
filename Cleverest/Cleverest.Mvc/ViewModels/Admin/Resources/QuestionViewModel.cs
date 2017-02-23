using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleverest.Business.Entities;
using Cleverest.Business.Helpers;

namespace Cleverest.Mvc.ViewModels.Admin.Resources
{
    public class QuestionViewModel
    {
        public int RoundNo { get; set; }
        
        public string GameId { get; set; }

        List<Question> Questions { get; set; }

        public QuestionViewModel(List<Question> questions)
        {
            if (questions != null &&  questions.Any())
                this.Questions = questions;
            else
                InitializeAsEmpty();
        }

        protected void InitializeAsEmpty()
        {
            this.Questions = new List<Question>();
            for(int i = 1; i <= CleverestHelper.GetQuestionsCount(this.RoundNo); i++)
            {
                Questions.Add(new Question() { GameId = this.GameId, RoundNo = this.RoundNo, OrderNo = i });
            }
        }
    }
}
