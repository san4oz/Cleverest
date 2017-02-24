using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleverest.Business.Entities;
using Cleverest.Business.InterfaceDefinitions.Providers;
using NHibernate.Criterion;

namespace Cleverest.DataProvider.Providers
{
    public class QuestionProvider : BaseProvider<Question>, IQuestionProvider
    {
        public List<Question> Get(string gameId, int roundNo)
        {
            return Execute(session =>
            {
                return session.CreateCriteria<Question>()
                            .Add(Expression.Eq("GameId", gameId))
                            .Add(Expression.Eq("RoundNo", roundNo))
                            .List<Question>()
                            .ToList();
            });
        }

        public Question Get(string gameId, int roundNo, int orderNo)
        {
            return Execute(session =>
            {
                return session.CreateCriteria<Question>()
                            .Add(Expression.Eq("GameId", gameId))
                            .Add(Expression.Eq("RoundNo", roundNo))
                            .Add(Expression.Eq("OrderNo", orderNo))
                            .UniqueResult<Question>();
            });
        }
    }
}
