using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.PL.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByUsernameOrEmail(string userNameOrEmail);
    }
}
