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

        public User GetByUsernameOrEmail(string userNameOrEmail)
        {
            //brothershipEntities context = DbContext as brothershipEntities;
            //return context.Users.FirstOrDefault(p => p.UserName == userNameOrEmail || p.Email == userNameOrEmail);
            
            // TH - Leaving the code above in case we run into issues with the code below. 
            return DbSet.FirstOrDefault(p => p.UserName == userNameOrEmail || p.Email == userNameOrEmail);
        }
    }
}
