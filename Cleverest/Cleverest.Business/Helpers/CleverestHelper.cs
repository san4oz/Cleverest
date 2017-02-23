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
            if (roundNo <= 0 || roundNo > 7)
                throw new ArgumentException("Round number should be from 1 to 7");

            var types = new Dictionary<int, QuestionType>()
            {
                { 1, QuestionType.Text },
                { 4, QuestionType.Text },
                { 5, QuestionType.Text },
                { 7, QuestionType.Text },
                { 2, QuestionType.Picture },
                { 6, QuestionType.Picture },
                { 3, QuestionType.Music }
            };

            return types[roundNo];
        }
    }
}
