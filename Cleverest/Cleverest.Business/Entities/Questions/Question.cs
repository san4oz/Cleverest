using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest.Business.Entities
{
    public class Question : Entity
    {
        public virtual string GameId { get; set; }

        public virtual int RoundNo { get; set; }       

        public virtual int OrderNo { get; set; }

        public virtual string CorrectAnswer { get; set; }
    }
}
