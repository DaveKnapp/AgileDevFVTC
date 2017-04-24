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
                Genders = _genderManager.GetAll(),
                Nationalities = _nationalityManager.GetAll(),
                CurrentUser = new User()
            };
            userViewModel.CurrentUser.Games = new List<Game>();

            return View(userViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateUserViewModel userViewModel)
        {
            //TODO(Dave) Add uploading of profile image
            //NOTE(Dave) This image path is set because it is not null-able in the database and ef throws validation error
            var newUser = userViewModel.CurrentUser;
            newUser.ProfileImagePath = "Default";
            newUser.UserTypeID = (int)UserType.UserTypes.User;

            if (ModelState.IsValid)
            {
                if (_userManger.UserNameExists(userViewModel.CurrentUser.UserName))
                {
                    ViewBag.Message = "Username is currently being used";
                    userViewModel.Genders = _genderManager.GetAll();
                    userViewModel.Nationalities = _nationalityManager.GetAll();

                    return View(nameof(Create), userViewModel);
                }
                else
                {
                    await _userManger.Add(newUser, userViewModel.Password);
                    var user = _userManger.Login(newUser.UserName, userViewModel.Password);

                    if (!(user is InvalidUser))
                    {
                        Session.Add("CurrentUser", user);
                        return RedirectToAction(nameof(EditIntegrations));
                    }
                }
            }

            ViewBag.Message = "An error occurred when creating the account.";
            return View(nameof(Create));
        }

        public ActionResult EditIntegrations()
        {
            User user = Session["CurrentUser"] as User;
            if (user == null)
            {
                return RedirectToAction("Login", "Login");
            }
            user = _userManger.GetById(user.ID);
            return View(user.UserIntegrations);
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

            currentUser.UserTypeID = (int)UserType.UserTypes.User;

            if (ModelState.IsValid)
            {
                await _userManger.Update(currentUser);

                userViewModel.Genders = _genderManager.GetAll();
                userViewModel.Nationalities = _nationalityManager.GetAll();
                ViewBag.UpdateMessage = "Account Successfully updated";

                return View(userViewModel);
            }
            else
            {
                ViewBag.Message = "An error occurred when updating the account.";
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
                //Refactor remove temp data
                TempData["error"] = "An error occurred when updating your password.";
                return RedirectToAction(nameof(ChangePassword));
            }

        }
    }
}