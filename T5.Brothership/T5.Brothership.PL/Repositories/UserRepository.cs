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
    {
        brothershipEntities _dataContext = new brothershipEntities();

        public void Dispose()
        {
            _dataContext.Dispose();
        }
        public void Delete(int Id)
        {
            _dataContext.Users.Remove(new User { ID = Id });
        }


        public IEnumerable<User> GetAll()
        {
            return _dataContext.Users;
        }

        public User GetByID(User entity)
        {
            return _dataContext.Users.FirstOrDefault(p => p.ID == entity.ID);
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
