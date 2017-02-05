using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest.Business.Entities
{
    public class Questions : Entity
    {
        public virtual string Question { get; set; }

        public virtual string Answer { get; set; }

        public virtual string GameId { get; set; }

        public virtual int RoundNo { get; set; }

        public virtual string ImageUrl { get; set; }

        public virtual string SongUrl { get; set; }
    }
}
