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
        UserManager _usermanager = new UserManager();


        [Route("{userName}")]
        public ActionResult User(string userName)
        {
            var user = _usermanager.GetByUserName(userName);
            return View(user);
        }

        [Route("User/UserRatings/{userName}")]
        public ActionResult UserRatings(string userName)
        {
            var user = _usermanager.GetByUserName(userName);
            return View(user);
        }

        [Route("User/UserGames/{userName}")]
        public ActionResult UserGames(string userName)
        {
            var user = _usermanager.GetByUserName(userName);
            return View(user);
        }
    }
}