using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace InfionicCommonServices
{
    public class PasswordModel
    {
        private static string CreateSalt(int size)
        {
            // Generate a cryptographic random number using the cryptographic 
            // service provider
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);
            // Return a Base64 string representation of the random number
            return Convert.ToBase64String(buff);
        }

        public static string GenereateHashValue(string password, out string passwordSalt)
        {
            passwordSalt = CreateSalt(50);
            string pwdSalt = password + passwordSalt;
            // Create a new instance of the hash crypto service provider.
            HashAlgorithm hashAlg = new SHA256CryptoServiceProvider();
            // Convert the data to hash to an array of Bytes.
            byte[] bytValue = Encoding.UTF8.GetBytes(pwdSalt);
            // Compute the Hash. This returns an array of Bytes.
            byte[] bytHash = hashAlg.ComputeHash(bytValue);                       
            string base64 = Convert.ToBase64String(bytHash);

            return base64;
        }

        public static string GetHashValue(string password, string existingPasswordSalt)
        {
            string pwdSalt = password + existingPasswordSalt;
            // Create a new instance of the hash crypto service provider.
            HashAlgorithm hashAlg = new SHA256CryptoServiceProvider();
            // Convert the data to hash to an array of Bytes.
            byte[] bytValue = Encoding.UTF8.GetBytes(pwdSalt);
            // Compute the Hash. This returns an array of Bytes.
            byte[] bytHash = hashAlg.ComputeHash(bytValue);
            string base64 = Convert.ToBase64String(bytHash);

            return base64;
        }

        /* Adding started by Saurabh */
        public static string GenerateRndomPassword()
        {
            string password = string.Empty;
            try
            {
                password = RandomString(6, true);
            }
            catch
            {
                password = "demo";
            }

            return password;
        }

        private static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
    }
}