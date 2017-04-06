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

namespace T5.Brothership.BL.Managers
{
    public class UserManager : IDisposable
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

        public UserManager(IBrothershipUnitOfWork unitOfWork, IGameAPIService gameApiService)
        {
            _unitOfWork = unitOfWork;
            _gameManager = new GameManager(_unitOfWork, gameApiService);
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

        public async Task Add(User user, string password)
        {
            var newUser = user;

            if (UserNameExists(user.UserName))
            {
                throw new UsernameAlreadyExistsException("UserName is being used by another user");
            }

            newUser.UserLogin = CreateUserLogin(password);
            newUser.DateJoined = DateTime.Now;

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
            //TODO(dave) Add tests
            //TODO(Dave) Do I want to add to repo to get by username?
            return _unitOfWork.Users.GetByUsernameOrEmail(userName);
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

            //TODO Add tests for social media
            currentUser.UserSocialJuncs.Clear();
            currentUser.UserSocialJuncs = updatedUser.UserSocialJuncs;

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
            return _unitOfWork.Users.GetByUsernameOrEmail(userName) != null;
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

    //TODO Remove region
    #region FrontPage
    //there might be a better place to move this and almost definitley a better way to do this
    //this can be moved to a different class if needed
    public class FrontPageUser
    {
        //Note:(Dave)  I moved this class to a viewModel in the viewmodel folder in the MVC project
        //In my opinion this class is a little confusing.  The name is FrontPageUser, but it contains two users, featured and popular.
        //I think you are bettor just using the user class since your using most of the properties anyway.

        public string FeaturedUserName { get; set; }
        public string FeaturedUserImagePath { get; set; }
        //public int FeaturedUserId { get; set; } //In case we need it
        public string PopularUserName { get; set; }
        public string PopularImagePath { get; set; }
        //public int PopularUserId { get; set; } //In case we need it
    }

    public class FrontPageUserList : List<FrontPageUser>
    {

        public void Load()
        {
            //Note:(Dave)  This method Does two things it gets premium users and gets popular users. What if we want a different qty of Featured and Popular?
            //I think it would be better to re-factor it into two methods
            //and call them from load.  Methods should only do one thing.  Even better, you can notice that we are repeating the same code
            //Can we make this into one method and use it for both premiumUsers and featured users... I think so
            //I moved this logic to GetTopRandomFeaturedUsers() and GetTopRandomPopularUsers...
            //And after doing that I noticed that your code makes it so a streamer cant be in the featured section and popular section at the same time.
            //So I guess its a bussiness rule. Can a Streamer Appear in Featured and popular at the same time?.. I don't know...
            //I added code to allow either way
            UserManager users = new UserManager();
            Random rnd = new Random();
            List<int> previousSelections = new List<int>();
            List<User> premiumUsers = users.GetAllUsers(); //This will need to be changed to a get premium users method (which needs to be written)
            List<User> popularUsers = users.GetAllUsers(); //This will need to be changed to a get popular users method (which needs to be written)
            int premiumUserNum = Convert.ToInt32(rnd.Next(premiumUsers.Count));
            int popularUserNum = Convert.ToInt32(rnd.Next(popularUsers.Count));

            //To create a random list of premium users and popular users
            for (int i = 0; i <= 3; i++) //EDIT THIS TO CHANGE HOW MANY USERS APPEAR
            {//might need to add a userid field so the profile can be viewed
                FrontPageUser user = new FrontPageUser();

                while (previousSelections.Contains(premiumUserNum))
                    premiumUserNum = Convert.ToInt32(rnd.Next(premiumUsers.Count));

                while (previousSelections.Contains(popularUserNum))
                    popularUserNum = Convert.ToInt32(rnd.Next(popularUsers.Count));

                user.FeaturedUserName = premiumUsers[premiumUserNum].UserName;
                user.FeaturedUserImagePath = premiumUsers[premiumUserNum].ProfileImagePath;

                user.PopularUserName = popularUsers[popularUserNum].UserName;
                user.PopularImagePath = popularUsers[popularUserNum].ProfileImagePath;

                previousSelections.Add(premiumUserNum);
                previousSelections.Add(popularUserNum);
                this.Add(user);
            }
        }
    }
    #endregion
}
