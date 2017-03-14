using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.PL.Repositories
{
    public class GameRepository : BaseRepositoy<Game>, IGameRepository
    {
        public GameRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public Game GetByIgdbId(int id)
        {//TODO Add Tests
            return DbSet.FirstOrDefault(p => p.igdbID == id);
        }
    }
}
