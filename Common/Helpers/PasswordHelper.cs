using System;
using System.Security.Cryptography;
using System.Text;

namespace Common.Helpers
{
    public class PasswordHelper
    {
        public static string GenerateSalt(int length)
        {
            if (length<=0) throw new ArgumentException();
                
            var cryptoService = new RNGCryptoServiceProvider();
            var saltBytes = new byte[length];
            cryptoService.GetNonZeroBytes(saltBytes);
            return Encoding.Unicode.GetString(saltBytes);
        }

        public static string ComputeHash(string password, string salt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (salt == null) throw new ArgumentNullException("salt");

            var resultPassword = string.Concat(password, salt);
            var resultBytes = Encoding.Unicode.GetBytes(resultPassword);
            var hashBytes = SHA512.Create().ComputeHash(resultBytes);
            return Encoding.Unicode.GetString(hashBytes);
        }

        public static string Generate(int length)
        {
            if (length <= 0) throw new ArgumentException();

            var randomString = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Trim();
            return length <= randomString.Length ? randomString.Substring(0, length) : randomString;
        }
    }
}
