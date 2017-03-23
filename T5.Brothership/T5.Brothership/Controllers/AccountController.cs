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
                NewUser = new User(),
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
            var newUser = userViewModel.NewUser;
            newUser.ProfileImagePath = "Default";

            //TODO(Dave) refactor make setting user type more clear
            newUser.UserTypeID = 1;

            if (ModelState.IsValid)
            {
                if (_userManger.UserNameExists(userViewModel.NewUser.UserName))
                {
                    TempData["error"] = "Username currently being used";
                    //TODO(Dave) make it so the information the user submitted stays on form after validation fails
                    return RedirectToAction(nameof(Create));
                }
                else
                {
                    await _userManger.Add(newUser, userViewModel.Password);
                    return View("AccountCreated");
                }
            }
            else
            {
                ViewBag.Message = "An error occurred when creating the account.";
                return View(nameof(Create));
            }
        }

        public ActionResult Update()
        {
            var user = Session["CurrentUser"] as User;

            if (user != null)
            {
                var userViewModel = new UpdateUserViewModel
                {
                    CurrentUser = _userManger.GetById(user.ID),
                    Nationalities = _nationalityManager.GetAll(),
                    Genders = _genderManager.GetAll()
                };
                ViewBag.Message = TempData["error"];
                return View(userViewModel);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Update(UpdateUserViewModel userViewModel)
        {
            var currentUser = userViewModel.CurrentUser;
            currentUser.ID = (Session["CurrentUser"] as User).ID;
            //NOTE(Dave) This image path is set because it is not null-able in the database and ef throws validation error
            currentUser.ProfileImagePath = "Default";

            currentUser.UserTypeID = 1;

            var val = ModelState.Values;
            if (ModelState.IsValid)
            {
                await _userManger.Update(currentUser);
                return View("AccountUpdated");
            }
            else
            {
                TempData["error"] = "An error occurred when updating the account.";
                return RedirectToAction(nameof(Update));
            }
        }

        public ActionResult ChangePassword()
        {
            User currentUser = Session["CurrentUser"] as User;

            if (currentUser == null)
            {
                return RedirectToAction("Login", "Login");

            }
            else
            {
                currentUser = _userManger.GetById(currentUser.ID);
                var viewModel = new ChangePasswordViewModel
                {
                    UserName = currentUser.UserName
                };

                ViewBag.Message = TempData["error"];
                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel passwordViewModel)
        {
            if (ModelState.IsValid)
            {
                User user = Session["CurrentUser"] as User;

                if (user != null)
                {
                    _userManger.UpdatePassword(passwordViewModel.CurrentPassword, passwordViewModel.NewPassword, user);
                    return View("PasswordUpdated");
                }
                else
                {
                    return View(nameof(UserLogin), passwordViewModel);
                }
            }
            else
            {
                TempData["error"] = "An error occurred when updating your password.";
                return RedirectToAction(nameof(ChangePassword));
            }

        }
    }
}