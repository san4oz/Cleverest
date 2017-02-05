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
    public class QuestionManager : BaseManager<Questions, IQuestionProvider>, IQuestionManager
    {
        public override void Update(Questions entity)
        {
            Provider.Update(entity.Id, entityToUpdate =>
            {
                entityToUpdate.Question = entityToUpdate.Question;
                entityToUpdate.Answer = entity.Answer;
                entityToUpdate.GameId = entity.GameId;
                entityToUpdate.RoundNo = entity.RoundNo;
                entityToUpdate.ImageUrl = entity.ImageUrl;
                entityToUpdate.SongUrl = entity.SongUrl;
            });
        }
    }
}
