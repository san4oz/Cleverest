using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleverest.Business.Entities;

namespace Cleverest.Business.InterfaceDefinitions.Providers
{
    public interface IQuestionProvider : IBaseProvider<Question>
    {
        List<Question> Get(string gameId, int roundNo);

        Question Get(string gameId, int roundNo, int orderNo);

        void DeleteGameQuestions(string gameId);
    }
}
