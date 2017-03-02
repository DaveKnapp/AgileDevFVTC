using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T5.Brothership.BL.Managers;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.Controllers
{
    public class LoginController : Controller
    {

        public ActionResult Login()
        {
            if (Session["CurrentUser"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Details");
            }
        }

        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            User user;

            using (UserManager userManager = new UserManager())
            {
                user = userManager.Login(login.Username, login.Password);
                if (!(user is InvalidUser))
                {
                    Session.Add("CurrentUser", user);
                    return RedirectToAction("Details");
                }
            }

            ViewBag.Message = "Invalid Username or Password";
            return View();
        }

        public ActionResult Details()
        {
            User user = Session["CurrentUser"] as User;

            if (user == null)
            {
                return RedirectToAction("Login");
            }

            return View(user);
        }

        public ActionResult LogOut()
        {
            Session["CurrentUser"] = null;
            return RedirectToAction("Login");
        }
    }
}
