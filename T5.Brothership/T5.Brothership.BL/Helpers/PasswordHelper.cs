using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace T5.Brothership.BL.Helpers
{
    public class PasswordHelper
    {
        private readonly PasswordHasher passwordHasher;

        public PasswordHelper()
        {
            passwordHasher = new PasswordHasher();
        }

        public HashedPassword GeneratePasswordHash(string _plainTextPassword)
        {
            var hashedPassword = new HashedPassword();
            hashedPassword.Salt = passwordHasher.GenerateSalt();
            hashedPassword.Password = passwordHasher.GenrateHash(_plainTextPassword + hashedPassword.Salt);

            return hashedPassword;
        }

        public bool IsPasswordMatch(string password, HashedPassword hash)
        {
            //byte[] bytes = Encoding.ASCII.GetBytes(password);
            //password = Encoding.Convert(Encoding.ASCII, Encoding.UTF8, bytes);

            return hash.Password == passwordHasher.GenrateHash(password + hash.Salt);
        }

    }
}
