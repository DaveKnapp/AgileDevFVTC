using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T5.Brothership.BL.Managers;
using T5.Brothership.Entities.Models;
using T5.Brothership.Helpers;

namespace T5.Brothership.Controllers
{
    public class LoginController : Controller
    {
        readonly IUserManager _usermanager;
        readonly ISessionHelper _sessionHelper;

        public LoginController() : this(new UserManager(), new SessionHelper()) { }

        public LoginController(IUserManager userManager, ISessionHelper sessionHelper)
        {
            _usermanager = userManager;
            _sessionHelper = sessionHelper;
        }

        public ActionResult Login()
        {
            return _sessionHelper.Get("CurrentUser") == null ? (ActionResult)View(nameof(Login)) : RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            User user;

            user = _usermanager.Login(login.Username, login.Password);
            if (!(user is InvalidUser))
            {
                _sessionHelper.Add("CurrentUser", user);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Message = "Invalid Username or Password";
            return View(nameof(login));
        }

        public ActionResult LogOut()
        {
            _sessionHelper.remove("CurrentUser");
            return View(nameof(Login));
        }

        //TODO(Dave) Refactor to combine into one partial
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
