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
        readonly GenderManager _genderManager = new GenderManager();


        public ActionResult Create()
        {
            var userViewModel = new CreateUserViewModel
            {
                Nationalities = _nationalityManager.GetAll(),
                Genders = _genderManager.GetAll()
            };

            ViewBag.Message = TempData["error"];
            return View(userViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateUserViewModel userViewModel)
        {
            //TODO(Dave) Add uploading of profile image
            //NOTE(Dave) This image path is set because it is not null-able in the database and ef throws validation error
            userViewModel.User.ProfileImagePath = "Default";

            //TODO(Dave) refactor make setter user type more clear
            userViewModel.User.UserTypeID = 1;

            if (ModelState.IsValid)
            {
                if (_userManger.UserNameExists(userViewModel.User.UserName))
                {
                    TempData["error"] = "Username currently being used";
                    return RedirectToAction(nameof(Create));
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