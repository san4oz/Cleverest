using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Cleverest.Business.Entities;
using Cleverest.Business.InterfaceDefinitions.Managers;
using Cleverest.Business.InterfaceDefinitions.Providers;

namespace Cleverest.Business.Managers
{
    public class AccountManager: BaseManager<Account, IAccountProvider>, IAccountManager
    {
        public override void Update(Account entity)
        {
            Provider.Update(entity.Id, entityToUpdate =>
            {
                entityToUpdate.Name = entity.Name;
                entityToUpdate.TeamId = entity.TeamId;
                entityToUpdate.Password = entity.Password;
                entityToUpdate.PasswordSalt = entityToUpdate.PasswordSalt;

                TryUpdatePassword(entityToUpdate);
            });
        }

        public override void Create(Account entity)
        {
            TryUpdatePassword(entity);
            base.Create(entity);
        }

        //protected string GenerateHash(string password = null)
        //{
        //    using (var sha256 = new SHA256Managed())
        //    {
        //        var saltBytes = GenerateSalt(5);
        //        var plainTextBytes = Encoding.UTF8.GetBytes(password);
        //        var plainTextWithSaltBytes = AppendByteArray(plainTextBytes, saltBytes);

        //        byte[] hashBytes = sha256.ComputeHash(plainTextWithSaltBytes);

        //        var result = AppendByteArray(hashBytes, saltBytes);

        //        return Convert.ToString(result);
        //    }
        //}

#warning implement hashing with salt. 

        protected byte[] GenerateSalt(int saltSize)
        {
            using (var generator = new RNGCryptoServiceProvider())
            {
                var buff = new byte[saltSize];
                generator.GetBytes(buff);
                return buff;
            }
        }

        protected bool TryUpdatePassword(Account accountToUpdate)
        {
            if (!string.IsNullOrEmpty(accountToUpdate.Password))
                return false;

            var oldAccount = this.Get(accountToUpdate.Id);
            if (oldAccount == null)
                return false;

            accountToUpdate.Password = oldAccount.Password;
            accountToUpdate.PasswordSalt = oldAccount.PasswordSalt;

            return true;
        }

        protected byte[] AppendByteArray(byte[] byteArray1, byte[] byteArray2)
        {
            var byteArrayResult =
                    new byte[byteArray1.Length + byteArray2.Length];

            for (var i = 0; i < byteArray1.Length; i++)
                byteArrayResult[i] = byteArray1[i];
            for (var i = 0; i < byteArray2.Length; i++)
                byteArrayResult[byteArray1.Length + i] = byteArray2[i];

            return byteArrayResult;
        }
    }
}
