using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace T5.Brothership.BL.Helpers
{
    public class PasswordHasher
    {
        private readonly RNGCryptoServiceProvider cryptoProcider;

        public PasswordHasher()
        {
            cryptoProcider = new RNGCryptoServiceProvider();
        }

        public string GenerateSalt()
        {
            const int SALT_SIZE = 24;
            // Lets create a byte array to store the salt bytes
            var saltBytes = new byte[SALT_SIZE];

            // lets generate the salt in the byte array
            cryptoProcider.GetNonZeroBytes(saltBytes);

            // Let us get some string representation for this salt
            var saltString = Convert.ToBase64String(saltBytes);

            // Now we have our salt string ready lets return it to the caller
            return saltString;
        }
        public string GenrateHash(string password)
        {
            using (SHA256 sha = new SHA256CryptoServiceProvider())
            {

                // Let us use SHA256 algorithm to
                // generate the hash from this salted password

                var dataBytes = Encoding.UTF8.GetBytes(password);
                var resultBytes = sha.ComputeHash(dataBytes);

                // return the hash string to the caller
                return Convert.ToBase64String(resultBytes);
            }
        }
    }
}
