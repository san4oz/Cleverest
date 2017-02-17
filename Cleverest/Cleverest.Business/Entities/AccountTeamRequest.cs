using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest.Business.Entities
{
    public class AccountTeamRequest : Entity
    {
        public virtual string FromId { get; set; }

        public virtual string ToId { get; set; }

        public virtual string TeamId { get; set; }

        public virtual AccountTeamRequestType RequestType { get; set; }

        public override int GetHashCode()
        {
            return (FromId.GetHashCode() * 128) ^ (ToId.GetHashCode() * 338); 
        }

        public override bool Equals(object obj)
        {
            var request = obj as AccountTeamRequest;
            if (request == null)
                return false;

            return FromId.Equals(request.FromId) && ToId.Equals(request.ToId);
        }
    }

    public enum AccountTeamRequestType
    {
        Invite = 1,
        Join
    }
}
