using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.PL;
using T5.Brothership.PL.Repositories;
using T5.Brothership.PL.Test.FakeRepositories;

namespace T5.Brothership.PL.Test
{
    public class FakeBrothershipUnitOfWork : IBrothershipUnitOfWork
    {
        IUserRepository _fakeUsers;
        public IUserRepository Users
        {
            get
            {
                if (_fakeUsers == null)
                {
                    _fakeUsers = new UserFakeRepository();
                }
                return _fakeUsers;
            }
        }

        public void Commit()
        {
            _fakeUsers.SaveChanges();
        }

        public void Dispose()
        {
            _fakeUsers.Dispose();
        }
    }
}
