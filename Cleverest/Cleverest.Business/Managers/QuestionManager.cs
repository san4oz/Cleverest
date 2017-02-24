using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleverest.Business.Entities;
using Cleverest.Business.InterfaceDefinitions.Managers;
using Cleverest.Business.InterfaceDefinitions.Providers;

namespace Cleverest.Business.Managers
{
    public class QuestionManager : BaseManager<Question, IQuestionProvider>, IQuestionManager
    {
        public List<Question> Get(string gameId, int roundNo)
        {
            return Provider.Get(gameId, roundNo);
        }

        public Question Get(string gameId, int roundNo, int orderNo)
        {
            return Provider.Get(gameId, roundNo, orderNo);
        }

        public override void Update(Question entity)
        {
            Provider.Update(entity.Id, question =>
            {
                question.GameId = entity.GameId;
                question.OrderNo = entity.OrderNo;
                question.RoundNo = entity.RoundNo;
                question.CorrectAnswer = entity.CorrectAnswer;
                question.QuestionBody = entity.QuestionBody;
            });
        }
    }
}
