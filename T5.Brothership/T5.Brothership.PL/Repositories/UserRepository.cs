﻿using System;
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
            //TODO Remove set with clear
            user.Games.Clear();
            DbContext.Set<UserRating>().RemoveRange(user.RatedByUser);
            DbContext.Set<UserRating>().RemoveRange(user.UserRatings);
            DbContext.Set<UserLogin>().Remove(user.UserLogin);

            DbContext.SaveChanges();
        }

        public User GetByUsernameOrEmail(string userNameOrEmail)
        {
            return DbSet.FirstOrDefault(p => p.UserName == userNameOrEmail || p.Email == userNameOrEmail);
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
    }
}
