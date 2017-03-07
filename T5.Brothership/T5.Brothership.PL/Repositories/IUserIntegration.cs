using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.PL.Repositories
{
    public interface IUserIntegrationRepository
    {
        IQueryable<UserIntegration> GetAll();
        UserIntegration GetById(int userId, int integrationTypeId);
        IQueryable<UserIntegration> GetAllByUser(int userId);
        void Add(UserIntegration entity);
        void Update(UserIntegration entity);
        void Delete(UserIntegration entity);
        void Delete(int userID, int integrationTypeId);
        void SaveChanges();
    }
}
