using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.PL.Repositories
{
    public class UserRatingRepository : IUserRatingRepository, IDisposable
    {

        public UserRatingRepository(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("Null DbContext");
            DbContext = dbContext;
            DbSet = DbContext.Set<UserRating>();
        }
        protected DbContext DbContext { get; set; }
        protected DbSet<UserRating> DbSet { get; set; }

        public void Add(UserRating entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                DbSet.Add(entity);
            }
        }

        public void Delete(UserRating entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }
        }

        public void Delete(int raterId, int userBeingRatedId)
        {
            var entity = DbSet.FirstOrDefault(p => p.RaterUserID == raterId && p.UserBeingRatedID == userBeingRatedId);
            if (entity == null) return; // not found; assume already deleted.
            Delete(entity);
        }

        public void Dispose()
        {
            DbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public IQueryable<UserRating> GetAll()
        {
            return DbSet;
        }

        public IQueryable<UserRating> GetAllByUserId(int ratedUserId)
        {
            return DbSet.Where(p => p.UserBeingRatedID == ratedUserId);
        }

        public UserRating GetById(int raterId, int userBeingRatedId)
        {
            return DbSet.FirstOrDefault(p => p.RaterUserID == raterId && p.UserBeingRatedID == userBeingRatedId);
        }

        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }

        public void Update(UserRating entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            dbEntityEntry.State = EntityState.Modified;
        }
    }
}
