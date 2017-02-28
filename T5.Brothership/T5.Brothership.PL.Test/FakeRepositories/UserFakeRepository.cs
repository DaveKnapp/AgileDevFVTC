using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;
using T5.Brothership.PL.Repositories;

namespace T5.Brothership.PL.Test.FakeRepositories
{
    internal class UserFakeRepository : IUserRepository
    {
        public UserFakeRepository()
        {
            InitializeUsers();
        }
        private List<User> _fakeUsers = new List<User>();

        public void Add(User entity)
        {
            entity.UserLogin.UserID = GenerateUserId();
            _fakeUsers.Add(entity);
        }

        public void Delete(int id)
        {
            User user = _fakeUsers.Single(p => p.ID == id);
            _fakeUsers.Remove(user);
        }

        public void Delete(User entity)
        {
            _fakeUsers.Remove(entity);
        }

        public void Dispose()
        {//TODO is this how to dispose?
            _fakeUsers = null;
        }

        public IQueryable<User> GetAll()
        {
            return _fakeUsers.AsQueryable();
        }

        public User GetById(int id)
        {
            return _fakeUsers.FirstOrDefault(p => p.ID == id);
        }

        public void SaveChanges()
        {
            
        }

        public void Update(User entity)
        {
            int userIndex = _fakeUsers.IndexOf(entity);
            _fakeUsers[userIndex] = entity;
        }

        private int GenerateUserId()
        {
            return _fakeUsers.Max(p => p.ID);
        }

        private void InitializeUsers()
        {
            _fakeUsers.Add(new User
            {
                ID = 1,
                UserName = "TestUserOne",
                Email = "Testing123@yahoo.com",
                Bio = "This is my bio",
                ProfileImagePath = "../Images/TestUserOne/Pofile.png",
                DateJoined = new DateTime(2017, 2, 23),
                DOB = new DateTime(1988, 11, 12),
                Gender = "M",
                UserType = new UserType { ID = 1, Description = "User" },
                Nationality = new Nationality { ID = 1, Description = "US and A" },
                NationalityID = 1,
                UserTypeID = 1,
                UserLogin = new UserLogin { Password = "Password", UserID = 1}
            });
        }
    }
}
