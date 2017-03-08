using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;
using T5.Brothership.PL.Repositories;

namespace T5.Brothership.PL.Test.FakeRepositories
{
    internal class UserTypeFakeRepository : IUserTypeRepository
    {
        List<UserType> _fakeUserTypes = new List<UserType>();

        public UserTypeFakeRepository()
        {
            InitializeFakeUserTypes();
        }

        public void Add(UserType entity)
        {
            entity.ID = GenerateUserTypeId();
            _fakeUserTypes.Add(entity);
        }

        public void Delete(int id)
        {
            UserType userType = _fakeUserTypes.Single(p => p.ID == id);
            _fakeUserTypes.Remove(userType);
        }

        public void Delete(UserType entity)
        {
            UserType userType = _fakeUserTypes.Single(p => p.ID == entity.ID);
            _fakeUserTypes.Remove(userType);
        }

        public void Dispose()
        {
            _fakeUserTypes = null;
        }

        public void SaveChanges()
        {
        }

        public void Update(UserType entity)
        {
            int userTypeIndex = _fakeUserTypes.IndexOf(entity);
            _fakeUserTypes[userTypeIndex] = entity;
        }

        IQueryable<UserType> IRepository<UserType>.GetAll()
        {
            return _fakeUserTypes.AsQueryable();
        }

        UserType IRepository<UserType>.GetById(int id)
        {
            return _fakeUserTypes.FirstOrDefault(p => p.ID == id);
        }

        private int GenerateUserTypeId()
        {
            return _fakeUserTypes.Max(p => p.ID);
        }

        private void InitializeFakeUserTypes()
        {
            _fakeUserTypes.Add(new UserType
            {
                ID = 1,
                Description = "User"
            });

            _fakeUserTypes.Add(new UserType
            {
                ID = 2,
                Description = "Admin"
            });

            _fakeUserTypes.Add(new UserType
            {
                ID = 3,
                Description = "Moderator"
            });

            _fakeUserTypes.Add(new UserType
            {
                ID = 4,
                Description = "DeActivated Account"
            });
        }
    }
}
