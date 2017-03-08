using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;
using T5.Brothership.PL.Repositories;

namespace T5.Brothership.PL.Test.FakeRepositories
{
    internal class NationalityFakeRepository : INationalityRepository
    {
        List<Nationality> _fakeNationalities = new List<Nationality>();

        NationalityFakeRepository()
        {
            InitializeFakeNationalities();
        }

        public void Add(Nationality entity)
        {
            entity.ID = GenerateUserTypeId();
            _fakeNationalities.Add(entity);
        }

        public void Delete(int id)
        {
            var nationality = _fakeNationalities.Single(p => p.ID == id);
            _fakeNationalities.Remove(nationality);
        }

        public void Delete(Nationality entity)
        {
            var nationality = _fakeNationalities.Single(p => p.ID == entity.ID);
            _fakeNationalities.Remove(nationality);
        }

        public void Dispose()
        {
            _fakeNationalities = null;
        }

        public IQueryable<Nationality> GetAll()
        {
            return _fakeNationalities.AsQueryable();
        }

        public Nationality GetById(int id)
        {
            return _fakeNationalities.FirstOrDefault(p => p.ID == id);
        }

        public void SaveChanges()
        {
        }

        public void Update(Nationality entity)
        {
            var entityIndex = _fakeNationalities.IndexOf(entity);
            _fakeNationalities[entityIndex] = entity;
        }

        private int GenerateUserTypeId()
        {
            return _fakeNationalities.Max(p => p.ID);
        }

        private void InitializeFakeNationalities()
        {
            _fakeNationalities.Add(new Nationality
            {
                ID = 1,
                Description = "US and A"
            });

            _fakeNationalities.Add(new Nationality
            {
                ID = 2,
                Description = "China"
            });

            _fakeNationalities.Add(new Nationality
            {
                ID = 3,
                Description = "Europe"
            });

            _fakeNationalities.Add(new Nationality
            {
                ID = 4,
                Description = "Mexico"
            });

            _fakeNationalities.Add(new Nationality
            {
                ID = 5,
                Description = "Canada"
            });
        }
    }
}
