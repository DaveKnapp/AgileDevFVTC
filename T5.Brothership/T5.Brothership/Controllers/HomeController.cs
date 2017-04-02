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
        readonly UserManager _usermanager = new UserManager();

        //FrontPageUserList frontPageUsers;
        public ActionResult Index()
        {
            var viewModel = new HomeViewModel();
            viewModel.RandomFeaturedUsers = _usermanager.GetRandomFeaturedUsers(0);
            viewModel.RandomPopularUsers = _usermanager.GetRandomPopularUsers(4,5);

            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}