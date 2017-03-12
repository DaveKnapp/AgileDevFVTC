using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.PL.Repositories
{
    public class GenderRepository : BaseRepositoy<Gender>, IGenderRepository
    {
        public GenderRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
