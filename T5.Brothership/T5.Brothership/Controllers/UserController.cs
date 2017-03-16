using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T5.Brothership.BL.Managers;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        UserManager _usermanager = new UserManager();
        public ActionResult Index()
        {
            return View();
        }


    }
}