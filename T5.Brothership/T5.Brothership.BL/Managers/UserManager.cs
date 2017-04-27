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
using T5.Brothership.BL.IGDBApi;
using T5.Brothership.BL.Exceptions;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace T5.Brothership.BL.Managers
{
    public class UserManager : IDisposable, IUserManager
    {
        GameManager _gameManager;
        IBrothershipUnitOfWork _unitOfWork;

        public UserManager()
        {
            _unitOfWork = new BrothershipUnitOfWork();
            _gameManager = new GameManager(_unitOfWork);

        }

        public UserManager(IBrothershipUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _gameManager = new GameManager(_unitOfWork);
        }

        public UserManager(IBrothershipUnitOfWork unitOfWork, IGameAPIClient gameApiClient)
        {
            _unitOfWork = unitOfWork;
            _gameManager = new GameManager(_unitOfWork, gameApiClient);
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
            _gameManager?.Dispose();
            GC.SuppressFinalize(this);
        }

        public List<User> GetSearchedUsers(string search)
        {
            return _unitOfWork.Users.GetSearchedUsers(search).ToList();
        }

        public List<User> GetUsersByGame(int igdbid)
        {
            return _unitOfWork.Games.GetByIgdbId(igdbid).Users.ToList();

            //For paging
            //return _unitOfWork.Games.GetByIgdbId(igdbid).Users.Skip(10).Take(5).ToList();
        }

        public async Task Add(User user, string password)
        {
            var newUser = user;
           
            newUser.ProfileImagePath = _unitOfWork.AzureBlobStorage.GetDefaultUserImage(); 

            if (UserNameExists(user.UserName))
            {
                throw new UsernameAlreadyExistsException("UserName is being used by another user");
            }
            newUser.UserLogin = CreateUserLogin(password);
           
            newUser.DateJoined = DateTime.Now;

            // Sets a default image for new users. Change later if we decide to allow uploads at sign up.
         

            await _gameManager.AddGamesIfNotExistsAsync(CreateIgdbIdArray(user.Games));
            newUser.Games = _gameManager.GetByIgdbIds(CreateIgdbIdArray(user.Games));

            _unitOfWork.Users.Add(newUser);
            _unitOfWork.Commit();
        }


        public List<User> GetAllUsers()
        {
            return _unitOfWork.Users.GetAll().ToList();
        }

        public User GetById(int id)
        {
            return _unitOfWork.Users.GetById(id);
        }

        public User GetByUserName(string userName)
        {
            return _unitOfWork.Users.GetByUsername(userName);
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

        public async Task Update(User updatedUser)
        {
            User currentUser = _unitOfWork.Users.GetById(updatedUser.ID);
            currentUser.Bio = updatedUser.Bio;
            currentUser.DOB = updatedUser.DOB;
            currentUser.Email = updatedUser.Email;
            currentUser.GenderId = updatedUser.GenderId;
            currentUser.NationalityID = updatedUser.NationalityID;
            currentUser.UserTypeID = updatedUser.UserTypeID;
            currentUser.ProfileImagePath = updatedUser.ProfileImagePath;

            currentUser.Games.Clear();

            await _gameManager.AddGamesIfNotExistsAsync(CreateIgdbIdArray(updatedUser.Games));

            currentUser.Games = _gameManager.GetByIgdbIds(CreateIgdbIdArray(updatedUser.Games));

            _unitOfWork.Users.Update(currentUser);
            _unitOfWork.Commit();
        }

        public void UpdatePassword(string currentPassword, string newPassword, User user)
        {
            var currentUser = _unitOfWork.Users.GetByUsernameOrEmail(user.UserName);

            var passwordHelper = new PasswordHelper();
            var hashedPassword = new HashedPassword { PasswordHash = currentUser.UserLogin.PasswordHash, Salt = currentUser.UserLogin.Salt };

            if (passwordHelper.IsPasswordMatch(currentPassword, hashedPassword))
            {
                UserLogin login = CreateUserLogin(newPassword);
                currentUser.UserLogin.PasswordHash = login.PasswordHash;
                currentUser.UserLogin.Salt = login.Salt;

                _unitOfWork.Users.Update(currentUser);
                _unitOfWork.Commit();
            }
            else
            {
                throw new InvalidPasswordException("Password is incorrect");
            }
        }

        public bool UserNameExists(string userName)
        {
            return _unitOfWork.Users.GetByUsername(userName) != null;
        }

        public List<User> GetRandomFeaturedUsers(int randomCount, List<User> usersToExclude = null)
        {
            var premiumUsers = _unitOfWork.Users.GetFeaturedUsers().ToList();

            if (premiumUsers.Count > randomCount)
            {
                return GetRandomUsersFromList(premiumUsers, randomCount, usersToExclude);
            }
            else
            {
                return premiumUsers;
            }
        }

        public List<User> GetRandomPopularUsers(int randomCount, int topUserCount, List<User> usersToExclude = null)
        {
            var popularUsers = _unitOfWork.Users.GetMostPopularUsers(topUserCount).ToList();

            if (popularUsers.Count > randomCount)
            {
                return GetRandomUsersFromList(popularUsers, randomCount, usersToExclude);
            }
            else
            {
                return popularUsers;
            }
        }


        private List<User> GetRandomUsersFromList(List<User> users, int qtyUsersToReturn, List<User> usersToExclude = null)
        {
            if (qtyUsersToReturn > users.Count)
            {
                throw new ArgumentException("qtyToReturn must be less than users");
            }
            //if (usersToExclude == null)
            //{
            //    usersToExclude = new List<User>();
            //}
            //This is the same as above
            usersToExclude = usersToExclude ?? new List<User>();

            var randomUsers = new User[qtyUsersToReturn];
            Random rand = new Random();

            for (int i = 0; i < randomUsers.Length; i++)
            {
                User randomUser;
                do
                {
                    int randomIndex = rand.Next(users.Count);
                    randomUser = users[randomIndex];

                } while (randomUsers.Contains(randomUser) || usersToExclude.Contains(randomUser));

                randomUsers[i] = randomUser;
            }

            return randomUsers.ToList();
        }

        private int[] CreateIgdbIdArray(ICollection<Game> games)
        {
            var gameDbIds = new List<int>();

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
