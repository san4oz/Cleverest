using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest.Business.Entities
{
    public class GameRegistrationRequest : Entity
    {
        public virtual string GameId { get; set; }

        public virtual string AccountId { get; set; }

        public virtual DateTime Date { get; set; }
    }
}
