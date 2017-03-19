using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;
using T5.Brothership.PL.Repositories;

namespace T5.Brothership.PL.Test.FakeRepositories
{
    internal class IntegrationTypeFakeRepository : IIntegrationTypeRepository
    {
        List<IntegrationType> _fakeIntegrationTypes = new List<IntegrationType>();

        public IntegrationTypeFakeRepository()
        {
            InitializeFakeIntegrationTypes();
        }

        public void Add(IntegrationType entity)
        {
            entity.ID = GenerateIntegrationTypeId();
            _fakeIntegrationTypes.Add(entity);
        }

        public void Delete(int id)
        {
            var integrationType = _fakeIntegrationTypes.Single(p => p.ID == id);
            _fakeIntegrationTypes.Remove(integrationType);
        }

        public void Delete(IntegrationType entity)
        {
            var integrationType = _fakeIntegrationTypes.Single(p => p.ID == entity.ID);
            _fakeIntegrationTypes.Remove(integrationType);
        }

        public void Dispose()
        {
            _fakeIntegrationTypes = null;
        }

        public IQueryable<IntegrationType> GetAll()
        {
            return _fakeIntegrationTypes.AsQueryable();
        }

        public IntegrationType GetById(int id)
        {
            return _fakeIntegrationTypes.FirstOrDefault(p => p.ID == id);
        }

        public void SaveChanges()
        {
        }

        public void Update(IntegrationType entity)
        {
            var entityIndex = _fakeIntegrationTypes.IndexOf(entity);
            _fakeIntegrationTypes[entityIndex] = entity;
        }

        private void InitializeFakeIntegrationTypes()
        {
            _fakeIntegrationTypes.Add(new IntegrationType
            {
                ID = 1,
                Description = "Twitch"
            });

            _fakeIntegrationTypes.Add(new IntegrationType
            {
                ID = 2,
                Description = "YouTube"
            });

            _fakeIntegrationTypes.Add(new IntegrationType
            {
                ID = 3,
                Description = "Twitter"
            });
        }

        private int GenerateIntegrationTypeId()
        {
            return _fakeIntegrationTypes.Max(p => p.ID) + 1;
        }
    }
}
