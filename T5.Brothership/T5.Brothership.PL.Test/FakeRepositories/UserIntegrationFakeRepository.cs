using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;
using T5.Brothership.PL.Repositories;

namespace T5.Brothership.PL.Test.FakeRepositories
{
    internal class UserIntegrationFakeRepository : IUserIntegrationRepository
    {
        List<UserIntegration> _fakeUserIntegrations = new List<UserIntegration>();

        public UserIntegrationFakeRepository()
        {
            InitializeFakeUserIntegrations();
        }

        public void Add(UserIntegration entity)
        {
            _fakeUserIntegrations.Add(entity);
        }

        public void Delete(UserIntegration entity)
        {
            var userIntegration = _fakeUserIntegrations.FirstOrDefault(p => p.UserID == entity.UserID &&
                                                                       p.IntegrationTypeID == entity.IntegrationTypeID);
            _fakeUserIntegrations.Remove(userIntegration);
        }

        public void Delete(int userID, int integrationTypeId)
        {
            var userIntegration = _fakeUserIntegrations.FirstOrDefault(p => p.UserID == userID &&
                                                             p.IntegrationTypeID == integrationTypeId);
            _fakeUserIntegrations.Remove(userIntegration);
        }

        public IQueryable<UserIntegration> GetAll()
        {
            return _fakeUserIntegrations.AsQueryable();
        }

        public IQueryable<UserIntegration> GetAllByUser(int userID)
        {
            return _fakeUserIntegrations.Where(p => p.UserID == userID).AsQueryable();
        }

        public UserIntegration GetById(int userId, int integrationTypeId)
        {
            return _fakeUserIntegrations.FirstOrDefault(p => p.UserID == userId && 
                                                        p.IntegrationTypeID == integrationTypeId);
        }

        public void SaveChanges()
        {
        }

        public void Update(UserIntegration entity)
        {
            int entityIndex = _fakeUserIntegrations.IndexOf(entity);
            _fakeUserIntegrations[entityIndex] = entity;
        }

        private void InitializeFakeUserIntegrations()
        {
            _fakeUserIntegrations.Add(new UserIntegration
            {
                UserID = 1,
                IntegrationTypeID = 1,
                Token = "ljlkj23lk4jlk23j",
            });

            _fakeUserIntegrations.Add(new UserIntegration
            {
                UserID = 3,
                IntegrationTypeID = 1,
                Token = "lj4l32j5lk43j5lk4j32lk5",
            });

            _fakeUserIntegrations.Add(new UserIntegration
            {
                UserID = 4,
                IntegrationTypeID = 1,
                Token = "lkjl2j4l5jlkk234j5l4",
            });
        }
    }
}
