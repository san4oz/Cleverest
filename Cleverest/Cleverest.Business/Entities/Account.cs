using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest.Business.Entities
{
    public class Account : Entity
    {
        public virtual string Name { get; set; }

        public virtual string TeamId { get; set; }

        public virtual string Password { get; set; }

        public virtual string PasswordSalt { get; set; }
    }
}
