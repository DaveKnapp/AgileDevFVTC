using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.PL.Repositories
{
    public class NationalityRepository : BaseRepositoy<Nationality>, INationalityRepository
    {
        public NationalityRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
