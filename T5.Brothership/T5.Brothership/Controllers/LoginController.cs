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
        readonly UserManager _usermanager = new UserManager();

        public ActionResult Login()
        {
            return Session["CurrentUser"] == null ? (ActionResult)View() : RedirectToAction("Index", "Home");
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
                    return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.Message = "Invalid Username or Password";
            return View(nameof(login));
        }

        public ActionResult LogOut()
        {
            Session["CurrentUser"] = null;
            return RedirectToAction(nameof(Login));
        }

        [ChildActionOnly]
        public PartialViewResult LoginLink()
        {
            User currentUser = Session["CurrentUser"] as User;
            if (currentUser == null)
            {
                return PartialView("LoginLink");
            }
            else
            {
                return PartialView("AccountMenu", currentUser);
            }
        }
        [ChildActionOnly]
        public PartialViewResult LogoutLink()
        {
            User currentUser = Session["CurrentUser"] as User;
            if (currentUser == null)
            {
                return null;
            }
            else
            {
                return PartialView("LogoutLink");
            }
        }
        [ChildActionOnly]
        public PartialViewResult SignupLink()
        {
            User currentUser = Session["CurrentUser"] as User;
            if (currentUser == null)
            {
                return PartialView("SignupLink");
            }
            else
            {
                return null;
            }
        }
        [ChildActionOnly]
        public PartialViewResult AccountUpdateLink()
        {
            User currentUser = Session["CurrentUser"] as User;
            if (currentUser == null)
            {
                return null;
            }
            else
            {
                return PartialView("AccountUpdateLink");
            }
        }
    }
}
