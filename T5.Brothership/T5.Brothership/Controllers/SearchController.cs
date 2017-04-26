using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T5.Brothership.BL.Managers;
using T5.Brothership.Entities.Models;
using T5.Brothership.ViewModels;

namespace T5.Brothership.Controllers
{
    public class SearchController : Controller
    {
        //TODO Add tests
        readonly UserManager _userManager = new UserManager();
        readonly GameManager _gameManager = new GameManager();
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SearchResults(string search)
        {
            return View((List<User>)_userManager.GetSearchedUsers(search));
        }

        public ActionResult GameSearchResults(string search)
        {
            return View((List<Game>)_gameManager.GetSearchedGames(search));
        }

        public ActionResult UsersByGame(int igdbid)
        {
            return View((List<User>)_userManager.GetUsersByGame(igdbid));
        }


    }
}