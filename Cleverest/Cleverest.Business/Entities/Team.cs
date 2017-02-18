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

        public virtual string OwnerId { get; set; }

        public virtual string Description { get; set; }

        public override bool Equals(object obj)
        {
            var team = obj as Team;
            if (team == null)
                return false;

            return team.Id.Equals(Id);
        }

        public override int GetHashCode()
        {
            return ((Id.GetHashCode()) * 1938) ^ OwnerId.GetHashCode();
        }
    }
}
