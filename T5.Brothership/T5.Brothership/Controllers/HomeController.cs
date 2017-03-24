using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T5.Brothership.BL.Managers;
using T5.Brothership.Entities;

namespace T5.Brothership.Controllers
{
    public class HomeController : Controller
    {
        FrontPageUserList frontPageUsers;
        public ActionResult Index()
        {
            frontPageUsers = new FrontPageUserList();
            frontPageUsers.Load();
            return View(frontPageUsers); 
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