using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.PL.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByUsernameOrEmail(string userNameOrEmail);
        User GetByUsername(string username);
        IQueryable<User> GetTopRatedUsers(int count);
        IQueryable<User> GetMostPopularUsers(int count);
        IQueryable<User> GetFeaturedUsers();
        IQueryable<User> GetSearchedUsers(string search);
        IQueryable<User> GetNewSearchedUsers(string search);
        IQueryable<User> GetNewUsers();
        IQueryable<User> GetTwitchUsers(string search);

    }
}
