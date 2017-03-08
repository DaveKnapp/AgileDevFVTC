using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.PL.Repositories;

namespace T5.Brothership.PL
{//TODO Add more repos
    public class BrothershipUnitOfWork : IBrothershipUnitOfWork
    {
        private brothershipEntities dbContext = new brothershipEntities();
        private UserRepository _userRepository;

        public IUserRepository Users
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(dbContext);
                }
                return _userRepository;
            }
        }

        public void Commit()
        {
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
