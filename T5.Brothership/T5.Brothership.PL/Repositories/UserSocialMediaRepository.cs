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
    public class UserSocialMediaRepository : IUserSocialMediaRepository, IDisposable
    {
        protected DbContext DbContext { get; set; }
        protected DbSet<UserSocialJunc> DbSet { get; set; }

        public UserSocialMediaRepository(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("Null DbContext");
            DbContext = dbContext;
            DbSet = DbContext.Set<UserSocialJunc>();
        }

        public void Add(UserSocialJunc entity)
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

        public void Delete(UserSocialJunc entity)
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

        public void Delete(int userId, int socialMediaTypeId)
        {
            var entity = DbSet.FirstOrDefault(p => p.UserID == userId  && p.SocialMediaTypeID == socialMediaTypeId);
            if (entity == null) return; // not found; assume already deleted.
            Delete(entity);
        }

        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }

        public void Update(UserSocialJunc entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            dbEntityEntry.State = EntityState.Modified;
        }

        public IQueryable<UserSocialJunc> GetAll()
        {
            return DbSet;
        }

        public UserSocialJunc GetById(int userId, int socialMediaTypeId)
        {
            return DbSet.FirstOrDefault(p => p.UserID == userId && p.SocialMediaTypeID == socialMediaTypeId);
        }

        public IQueryable<UserSocialJunc> GetAllByUser(int userId)
        {
            return DbSet.Where(p => p.UserID == userId);
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
