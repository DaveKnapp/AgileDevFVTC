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
    //TODO Add integration test
    public class UserIntegrationRepository : IUserIntegrationRepository
    {
        protected DbContext DbContext { get; set; }
        protected DbSet<UserIntegration> DbSet { get; set; }

        public UserIntegrationRepository(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("Null DbContext");
            DbContext = dbContext;
            DbSet = DbContext.Set<UserIntegration>();
        }

        public void Add(UserIntegration entity)
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

        public void Delete(UserIntegration entity)
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

        public void Delete(int userID, int integrationTypeId)
        {
            var entity = DbSet.FirstOrDefault(p => p.UserID == userID && p.IntegrationTypeID == integrationTypeId);
            if (entity == null) return; // not found; assume already deleted.
            Delete(entity);
        }

        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }

        public void Update(UserIntegration entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            dbEntityEntry.State = EntityState.Modified;
        }

        public IQueryable<UserIntegration> GetAll()
        {
            return DbSet;
        }

        public UserIntegration GetById(int userId, int integrationId)
        {
            return DbSet.FirstOrDefault(p => p.UserID == userId && p.IntegrationTypeID == p.IntegrationTypeID);
        }

        public IQueryable<UserIntegration> GetAllUserIntegrations(int userID)
        {
            return DbSet.Where(p => p.UserID == userID);
        }
    }
}
