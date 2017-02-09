using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest.Core.Security
{
    public class CryptoHelper
    {
        public string GenerateSalt(int saltSize = 10)
        {
            using (var generator = new RNGCryptoServiceProvider())
            {
                var buff = new byte[saltSize];
                generator.GetBytes(buff);
                return Encoding.UTF8.GetString(buff);
            }
        }

        public string GenerateHash(string plainText, string salt)
        {
            var textWithSalt = string.Concat(plainText, salt);

            using (var cryptoAlgorithm = new SHA256Managed())
            {
                var hashedText = cryptoAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(textWithSalt));

                return Encoding.UTF8.GetString(hashedText);
            }
        }
    }
}
