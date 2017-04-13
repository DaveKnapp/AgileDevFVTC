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
        GameAPIClient _gameApiClient = new GameAPIClient();

        public PartialViewResult SearchIGDB(string gameTitle, int limit = 50, int page = 0)
        {
            List<Game> game = new List<Game>();

            game = search(gameTitle, limit, page).Result;


            return PartialView(game);
        }

        private async Task<List<Game>> search(string title, int limit, int page)
        {

            using (var gameApi = new GameAPIClient())
            {
                List<Game> games = new List<Game>();
                games = await gameApi.SearchByTitleAsync(title, limit, page).ConfigureAwait(false);

                return games;
            }
        }
    }
}