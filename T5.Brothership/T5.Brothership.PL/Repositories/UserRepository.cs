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
            var user = DbSet.FirstOrDefault(p => p.ID == id);

            user.Games.Clear();
            user.RatedByUser.Clear();
            user.UserRatings.Clear();
            DbSet.Remove(user);
            DbContext.SaveChanges();
        }

        public override void Delete(User user)
        {
            user.Games.Clear();
            user.RatedByUser.Clear();
            user.UserRatings.Clear();
            DbSet.Remove(user);

            DbContext.SaveChanges();
        }



        public User GetByUsernameOrEmail(string userNameOrEmail)
        {
            return DbSet.FirstOrDefault(p => p.UserName.ToLower() == userNameOrEmail.ToLower() || p.Email.ToLower() == userNameOrEmail.ToLower());
        }

        public IQueryable<User> GetSearchedUsers(string search)
        {
            try
            {
                return DbSet.Where(p => p.UserName.ToLower().Contains(search.ToLower()));
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }

        public IQueryable<User> GetNewSearchedUsers(string search)
        {
            return DbSet.Where(p => p.UserName.ToLower().Contains(search.ToLower())).OrderByDescending(p => p.DateJoined);
        }

        public IQueryable<User> GetTwitchUsers(string search)
        {
            return DbSet.Where(p => p.UserIntegrations.Any(x => x.UserName.Contains(search)));
        }

        public IQueryable<User> GetNewUsers()
        {
            return DbSet.OrderByDescending(p => p.DateJoined);
        }        

        public IQueryable<User> GetFeaturedUsers()
        {
            return DbSet.Where(p => p.UserTypeID == (int)UserType.UserTypes.FeaturedUser);
        }

        public IQueryable<User> GetMostPopularUsers(int count)
        {
            return DbSet.OrderByDescending(p => (p.UserRatings.Average(i => i.RatingID) * p.UserRatings.Count)).Take(count);
        }

        public IQueryable<User> GetTopRatedUsers(int count)
        {
            //TODO(Dave) this method currently returns top average ratings.  Should it only return user with over x amount of ratings.
            return DbSet.OrderByDescending(p => p.UserRatings.Average(i => i.RatingID)).Take(count);
        }

        public User GetByUsername(string username)
        {
            return DbSet.FirstOrDefault(p => p.UserName.ToLower() ==  username.ToLower());
        }
    }
}
