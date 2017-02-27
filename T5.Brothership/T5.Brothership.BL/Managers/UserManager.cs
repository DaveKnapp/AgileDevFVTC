using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;
using T5.Brothership.PL.Repositories;

namespace T5.Brothership.BL.Managers
{
    public class UserManager:IDisposable
    {//Business logic goes in this class
        UserRepository userRepository = new UserRepository();

        public void Dispose()
        {
            userRepository.Dispose();
        }

        public User GetById(int id)
        {
            return userRepository.GetByID(id);
        }
    }
}
