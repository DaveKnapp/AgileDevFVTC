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

        public override void Delete(int id)
        {
            User user = DbSet.FirstOrDefault(p => p.ID == id);

            DbContext.Set<UserRating>().RemoveRange(user.RatedByUser);
            DbContext.Set<UserRating>().RemoveRange(user.UserRatings);
            DbContext.Set<UserLogin>().Remove(user.UserLogin);

            DbContext.SaveChanges();
        }

        public override void Delete(User user)
        {
            DbContext.Set<UserRating>().RemoveRange(user.RatedByUser);
            DbContext.Set<UserRating>().RemoveRange(user.UserRatings);
            DbContext.Set<UserLogin>().Remove(user.UserLogin);

            DbContext.SaveChanges();
        }

        public User GetByUsernameOrEmail(string userNameOrEmail)
        {
            return DbSet.FirstOrDefault(p => p.UserName == userNameOrEmail || p.Email == userNameOrEmail);
        }
    }
}
