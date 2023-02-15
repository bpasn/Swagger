using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Swagger.Core.AuthService
{
    public class Helper
    {
        public static string  HashCode(string pwd)
        {

            byte[] salt = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }

            string hashed = Convert.ToBase64String(
                    KeyDerivation.Pbkdf2(
                        password: pwd,
                        salt: salt,
                        prf: KeyDerivationPrf.HMACSHA256,
                        iterationCount: 50,
                        numBytesRequested: 256 / 8
                        )
                    );
            return hashed;
           /*MD5 md5 = MD5.Create();
           byte[] result = md5.ComputeHash(Encoding.ASCII.GetBytes(pwd));
           return Encoding.ASCII.GetString(result);*/
        }
    }
}