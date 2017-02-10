using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Cleverest.Business.Entities;
using Cleverest.Business.InterfaceDefinitions.Managers;
using Cleverest.Business.InterfaceDefinitions.Providers;
using Cleverest.Core.Security;

namespace Cleverest.Business.Managers
{
    public class AccountManager: BaseManager<Account, IAccountProvider>, IAccountManager
    {
        public override void Update(Account entity)
        {
            Provider.Update(entity.Id, entityToUpdate =>
            {
                entityToUpdate.Email = entity.Email;
                entityToUpdate.Name = entity.Name;
                entityToUpdate.TeamId = entity.TeamId;
                entityToUpdate.Password = entity.Password;
                entityToUpdate.PasswordSalt = entityToUpdate.PasswordSalt;

                TryUpdatePassword(entityToUpdate);
            });
        }

        public Account GetByEmail(string email)
        {
            return Provider.GetByEmail(email);
        }

        public override void Create(Account entity)
        {
            var crypto = new CryptoHelper();

            entity.PasswordSalt = crypto.GenerateSalt();
            entity.Password = crypto.GenerateHash(entity.Password, entity.PasswordSalt);

            base.Create(entity);
        }

        protected bool TryUpdatePassword(Account accountToUpdate)
        {
            if (!string.IsNullOrEmpty(accountToUpdate.Password))
                return false;

            var oldAccount = this.Get(accountToUpdate.Id);
            if (oldAccount == null)
                return false;

            if(string.IsNullOrEmpty(accountToUpdate.Password))
            {
                accountToUpdate.Password = oldAccount.Password;
                accountToUpdate.PasswordSalt = oldAccount.PasswordSalt;
            }
            else if(!accountToUpdate.Password.Equals(oldAccount.Password))
            {
                var crypto = new CryptoHelper();
                accountToUpdate.PasswordSalt = crypto.GenerateSalt();
                accountToUpdate.Password = crypto.GenerateHash(accountToUpdate.Password, accountToUpdate.PasswordSalt);
            }

            return true;
        }       
    }
}
