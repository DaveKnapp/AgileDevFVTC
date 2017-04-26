using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T5.Brothership.BL.Managers;
using T5.Brothership.Entities;
using T5.Brothership.ViewModels;

namespace T5.Brothership.Controllers
{
    public class HomeController : Controller
    {
        readonly IUserManager _usermanager;

        public HomeController() : this(new UserManager()) { }
        
        public HomeController(IUserManager userManger)
        {
            _usermanager = userManger;
        }

        public ActionResult Index()
        {
            var viewModel = new HomeViewModel
            {
                RandomFeaturedUsers = _usermanager.GetRandomFeaturedUsers(4),
                RandomPopularUsers = _usermanager.GetRandomPopularUsers(4, 5)
            };

            return View(nameof(Index), viewModel);
        }
    }
}