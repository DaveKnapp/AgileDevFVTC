using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T5.Brothership.BL.Managers;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.Controllers
{
    public class SearchController : Controller
    {

        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SearchResults()
        {
            UserManager _userManager = new UserManager();
            List<User> users = _userManager.GetSearchedUsers("User");
            return View(users);
        }
    }
}