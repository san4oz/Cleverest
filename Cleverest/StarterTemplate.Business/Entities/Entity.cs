using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest.Business.Entities
{
    public class Entity
    {
        public virtual string Id { get; set; }
        
        public Entity()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
