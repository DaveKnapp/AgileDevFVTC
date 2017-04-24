using System.Collections.Generic;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.BL.Managers
{
    public interface IUserManager
    {
        Task Add(User user, string password);
        void Dispose();
        List<User> GetAllUsers();
        User GetById(int id);
        User GetByUserName(string userName);
        List<User> GetRandomFeaturedUsers(int randomCount, List<User> usersToExclude = null);
        List<User> GetRandomPopularUsers(int randomCount, int topUserCount, List<User> usersToExclude = null);
        List<User> GetSearchedUsers(string search);
        List<User> GetUsersByGame(int igdbid);
        User Login(string userNameOrEmail, string password);
        Task Update(User updatedUser);
        void UpdatePassword(string currentPassword, string newPassword, User user);
        bool UserNameExists(string userName);
    }
}