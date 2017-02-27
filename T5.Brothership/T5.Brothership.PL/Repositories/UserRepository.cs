using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.PL.Repositories
{
    public class UserRepository : IUserRepository
    {//This Class interacts with the database
        brothershipEntities _dataContext = new brothershipEntities();

        public void Dispose()
        {
            _dataContext.Dispose();
        }
        public void Delete(int Id)
        {
            User user = _dataContext.Users.FirstOrDefault(p => p.ID == Id);
            _dataContext.Users.Remove(user);
        }

        public IEnumerable<User> GetAll()
        {
            return _dataContext.Users;
        }

        public User GetByID(int id)
        {
            return _dataContext.Users.FirstOrDefault(p => p.ID == id);
        }

        public void Insert(User entity)
        {
            _dataContext.Users.Add(entity);
        }

        public void Save()
        {
            _dataContext.SaveChanges();
        }

        public void Update(User entity)
        {
            User user = _dataContext.Users.Find(entity.ID);
            user = entity;
        }
    }
}
