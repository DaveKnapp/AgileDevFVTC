using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.PL.Repositories
{
    //TODO Add integration test
    public class UserTypeRepository : BaseRepositoy<UserType>, IUserTypeRepository
    {
        public UserTypeRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
