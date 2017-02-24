using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleverest.App_Start.AutoMapper;
using Cleverest.Business.Entities;

namespace Cleverest.Mvc.ViewModels.Frontend.Game
{
    public class QuestionViewModel : IMapFrom<Question>
    {
        public string QuestionBody { get; set; }

        public int RoundNo { get; set; }

        public int OrderNo { get; set; }

        public string CorrectAnswer { get; set; }

        public string GameId { get; set; }

        public bool NextQuestionExists { get; set; }

        public bool PreviousQuestionExists { get; set; }
    }
}
