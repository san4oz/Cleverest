using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest.Business.Entities
{
    public class AccountTeamPermission : Entity
    {
        public virtual string AccountId { get; set; }

        public virtual string TeamId { get; set; }
    }
}
