using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;
using T5.Brothership.PL.Repositories;

namespace T5.Brothership.PL.Test.FakeRepositories
{
    internal class GenderFakeRepo : IGenderRepository
    {
        private List<Gender> _fakeGenders = new List<Gender>();

        internal GenderFakeRepo()
        {
            InitializeFakeGenders();
        }

        public void Add(Gender entity)
        {
            _fakeGenders.Add(entity);
        }

        public void Delete(int id)
        {
            var gender = _fakeGenders.FirstOrDefault(p => p.Id == id);
            _fakeGenders.Remove(gender);
        }

        public void Delete(Gender entity)
        {
            var gender = _fakeGenders.Single(p => p.Id == entity.Id);
            _fakeGenders.Remove(gender);
        }

        public void Dispose()
        {
            _fakeGenders = null;
        }

        public IQueryable<Gender> GetAll()
        {
            return _fakeGenders.AsQueryable();
        }

        public Gender GetById(int id)
        {
            return _fakeGenders.FirstOrDefault(p => p.Id == id);
        }

        public void SaveChanges()
        {
        }

        public void Update(Gender entity)
        {
            var entityIndex = _fakeGenders.IndexOf(entity);
            _fakeGenders[entityIndex] = entity;
        }

        private void InitializeFakeGenders()
        {
            _fakeGenders.Add(new Gender
            {
                Id =1,
                Description = "Male"
            });

            _fakeGenders.Add(new Gender
            {
                Id = 2,
                Description = "Female"
            });

            _fakeGenders.Add(new Gender
            {
                Id = 3,
                Description = "Human"
            });
        }
    }
}
