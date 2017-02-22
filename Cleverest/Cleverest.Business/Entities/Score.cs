using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest.Business.Entities
{
    public class Score : Entity
    {
        public string GameId { get; set; }

        public string TeamId { get; set; }

        public string RoundId { get; set; }
    }
}
