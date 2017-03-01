using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.PL.Repositories
{
    public class UserRepository : BaseRepositoy<User>, IUserRepository
    {
        public UserRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public User GetByUsernameOrEmail(string userName, string email )
        {//TODO is there a better way to do this?  should type DbContext be different in contructor?
            brothershipEntities context = DbContext as brothershipEntities;
            return context.Users.FirstOrDefault(p => p.UserName == userName || p.Email == email);
        }
    }
}
