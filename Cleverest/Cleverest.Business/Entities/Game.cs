using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest.Business.Entities
{
    public class Game : Entity
    {
        public virtual string Title { get; set; }

        public virtual DateTime GameDate { get; set; }

        public virtual string Location { get; set; }
    }
}
