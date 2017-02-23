using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest.Business.Entities
{
    public class Score : Entity
    {
        public virtual string GameId { get; set; }

        public virtual string TeamId { get; set; }

        public virtual int RoundNo { get; set; }

        public virtual int Value { get; set; }
    }
}
