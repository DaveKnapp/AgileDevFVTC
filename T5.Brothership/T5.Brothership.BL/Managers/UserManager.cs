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
    public class UserManager:IDisposable
    {//Business logic goes in this class
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
        }

        public User GetById(int id)
        {
            return _unitOfWork.Users.GetById(id);
        }

        public User Login(string userNameOrEmail, string password)
        {//TODO(Dave) Add tests
            //TODO(Dave) Refactor?  Is this how I want to do this?
            User user = _unitOfWork.Users.GetByUsernameOrEmail(userNameOrEmail);

            if (user == null)
            {
               return new InvalidUser();
            }

            var passwordHelper = new PasswordHelper();
            HashedPassword hashedPassword = new HashedPassword { Password = user.UserLogin.PasswordHash, Salt = user.UserLogin.Salt };

            if (passwordHelper.IsPasswordMatch(password, hashedPassword))
            {
                return user;
            }

            return new InvalidUser();
        }
    }
}
