using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest.Business.Entities
{
    public class Team : Entity
    {
        public virtual string Name { get; set; }

        public virtual string PhoneNumber { get; set; }

        public virtual string SocialNetworkLink { get; set; }
    }
}
