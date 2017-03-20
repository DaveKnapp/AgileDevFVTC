using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T5.Brothership.BL.Managers;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;
using T5.Brothership.ViewModels;

namespace T5.Brothership.Controllers
{
    public class AccountController : Controller
    {
        readonly UserManager _userManger = new UserManager();
        readonly NationalityManager _nationalityManager = new NationalityManager();
        public ActionResult Create()
        {
            var userViewModel = new UserViewModel
            {
                Nationalities = _nationalityManager.GetAll()
            };

            return View(userViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Create(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                if (_userManger.UserNameExists(userViewModel.User.UserName))
                {
                    ViewBag.Message = "Username is currently in use.";
                    return View();
                }
                else
                {
                    await _userManger.Add(userViewModel.User, userViewModel.Password);
                    return View("AccountCreated");
                }
            }
            else
            {
                ViewBag.Message = "An error occurred when creating the account.";
                return View(nameof(Create));
            }
        }
    }
}