using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleverest.Business.Entities.Questions;

namespace Cleverest.Business.Helpers
{
    public static class CleverestHelper
    {
        public static int GetQuestionsCount(int roundNo)
        {
            return roundNo != 7 ? 7 : 10;
        }

        public static QuestionType GetQuestionType(int roundNo)
        {
            switch(roundNo)
            {
                case 1:
                    return QuestionType.Text;
                case 4:
                    return QuestionType.Text;
                case 5:
                    return QuestionType.Text;
                case 7:
                    return QuestionType.Text;
                case 2:
                    return QuestionType.Picture;
                case 6:
                    return QuestionType.Picture;
                case 3:
                    return QuestionType.Music;
                default:
                    return QuestionType.Text;
            }

        }
    }
}
