﻿using System;
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
        {
            return DbSet.FirstOrDefault(p => p.igdbID == id);
        }
        
        public IQueryable<Game> GetSearchedGames(string search)
        {
            return DbSet.Where(p => p.Title.ToLower().Contains(search.ToLower()));
        }
    }
}
