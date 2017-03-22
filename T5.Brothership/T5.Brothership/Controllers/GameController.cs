using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T5.Brothership.BL.IGDBApi;
using T5.Brothership.Entities.Models;
using System.Threading.Tasks;

namespace T5.Brothership.Controllers
{
    public class GameController : Controller
    {
        GameAPIService _gameApiService = new GameAPIService();

        public PartialViewResult SearchIGDB(string keyword)
        {
            List<Game> game = new List<Game>();

            game = search(keyword).Result;


            return PartialView(game);
        }

        private async Task<List<Game>> search(string title)
        {
            int limit = 50;
            int page = 0;

            using (var gameApi = new GameAPIService())
            {
                List<Game> games = new List<Game>();
                games = await gameApi.SearchByTitleAsync(title,limit).ConfigureAwait(false);
                return games;
            }
        }
    }
}