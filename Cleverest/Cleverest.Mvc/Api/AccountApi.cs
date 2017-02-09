using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleverest.Business.Entities;
using Cleverest.Core.Security;

namespace Cleverest.Mvc.Api
{
    public class AccountApi
    {      
        public Account Authenticate(string email, string password)
        {
            var account = Site.Managers.Account.GetByEmail(email);
            if (account == null)
                return null;

            var newPasswordHash = new CryptoHelper().GenerateHash(password, account.PasswordSalt);

            if (!newPasswordHash.Equals(account.Password))
                return null;

            return account;
        }

        public bool AccountExists(string email)
        {
            return Site.Managers.Account.GetByEmail(email) != null;
        }
    }
}
