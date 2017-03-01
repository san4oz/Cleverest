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
    public class GameManager : BaseManager<Game, IGameProvider>, IGameManager
    {
        protected ITeamManager TeamManager;

        protected IAccountManager AccountManager;

        protected IQuestionProvider QuestionProvider;

        public GameManager(ITeamManager teamManager, IAccountManager accountManager, IQuestionProvider questionProvider)
        {
            this.TeamManager = teamManager;
            this.AccountManager = accountManager;
            this.QuestionProvider = questionProvider;
        }

        public override void Update(Game entity)
        {
            Provider.Update(entity.Id, entityToUpdate =>
            {
                entityToUpdate.Title = entity.Title;
                entityToUpdate.GameDate = entity.GameDate;
                entityToUpdate.MaxTeamCount = entity.MaxTeamCount;
            });
        }

        public IList<GameRegistrationRequest> GetRegistrationRequests(string gameId)
        {
            return Provider.GetRegistrationRequests(gameId);
        }

        public override void Delete(string id)
        {
            Provider.DeleteRegistrationRequests(id);
            QuestionProvider.DeleteGameQuestions(id);
            base.Delete(id);
        }
    }
}
