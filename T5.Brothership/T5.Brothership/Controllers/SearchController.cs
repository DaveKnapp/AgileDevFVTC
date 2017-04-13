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
        readonly UserManager _userManager = new UserManager();
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SearchResults(string search)
        {
            return View((List<User>)_userManager.GetSearchedUsers(search));
        }

    }
}