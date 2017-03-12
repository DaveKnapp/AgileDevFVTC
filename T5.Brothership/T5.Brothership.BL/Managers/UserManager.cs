using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;
using T5.Brothership.PL.Repositories;
using T5.Brothership.PL;
using System.Data.Entity;
using T5.Brothership.BL.Helpers;

namespace T5.Brothership.BL.Managers
{
    public class UserManager : IDisposable
    {
        IBrothershipUnitOfWork _unitOfWork = new BrothershipUnitOfWork();

        public UserManager()
        {
        }

        public UserManager(IBrothershipUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
            GC.SuppressFinalize(this);
        }

        public User GetById(int id)
        {
            return _unitOfWork.Users.GetById(id);
        }

        public User Login(string userNameOrEmail, string password)
        {
            var user = _unitOfWork.Users.GetByUsernameOrEmail(userNameOrEmail);

            if (user == null)
            {
                return new InvalidUser();
            }

            var passwordHelper = new PasswordHelper();
            var hashedPassword = new HashedPassword { PasswordHash = user.UserLogin.PasswordHash, Salt = user.UserLogin.Salt };

            return passwordHelper.IsPasswordMatch(password, hashedPassword) ? user : new InvalidUser();
        }

        public void Add(User user, string password)
        {
            throw new NotImplementedException();
            
            var newUser = user;
            var passwordHelper = new PasswordHelper();
            var hashedPassword = passwordHelper.GeneratePasswordHash(password);

            newUser.UserLogin = new UserLogin
            {
                PasswordHash = hashedPassword.PasswordHash,
                Salt = hashedPassword.Salt
            };

            //Add games if not exist
            //Add user games
            newUser.DateJoined = DateTime.Now;
            _unitOfWork.Users.Add(user);
            _unitOfWork.Commit();
        }
    }
}
