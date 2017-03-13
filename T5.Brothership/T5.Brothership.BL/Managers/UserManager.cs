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
            //TODO(Dave) Finish Method

            var newUser = user;

            newUser.UserLogin = CreateUserLogin(password);

            //Add User
            newUser.DateJoined = DateTime.Now;
            _unitOfWork.Users.Add(user);
            _unitOfWork.Commit();

            //Add gamesToDb if not exist
            using (var gameManager = new GameManager())
            {
               var newGamesAdded = gameManager.AddGamesByIgdbIdIfNotExist(CreateIgdbIdArray(user.Games));
            }

            //Add user games
           
        }

        private int[] CreateIgdbIdArray(ICollection<Game> games)
        {
            List<int> gameDbIds = new List<int>();

            foreach (var game in games)
            {
                if (game.igdbID != null)
                {
                    gameDbIds.Add((int)game.igdbID);
                }
            }

            return gameDbIds.ToArray();
        }

        private UserLogin CreateUserLogin(string password)
        {
            var passwordHelper = new PasswordHelper();
            var hashedPassword = passwordHelper.GeneratePasswordHash(password);

            return new UserLogin
            {
                PasswordHash = hashedPassword.PasswordHash,
                Salt = hashedPassword.Salt
            };
        }
    }
}
