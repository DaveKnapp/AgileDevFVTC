using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T5.Brothership.BL.Managers;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;
using T5.Brothership.ViewModels;
using T5.Brothership.Helpers;
using T5.Brothership.BL.Exceptions;
using System.IO;

namespace T5.Brothership.Controllers
{
    public class AccountController : Controller
    {
        readonly IUserManager _userManger;
        readonly INationalityManager _nationalityManager;
        readonly IGenderManager _genderManager;
        readonly ISessionHelper _sessionHelper;
        AzureStorageManager _azureManager;

        public AccountController() : this(new UserManager(), new NationalityManager(), new GenderManager(), new SessionHelper())
        { }

        public AccountController(IUserManager userManger, INationalityManager nationalityManger, IGenderManager genderManager, ISessionHelper sessionHelper)
        {
            _userManger = userManger;
            _nationalityManager = nationalityManger;
            _genderManager = genderManager;
            _sessionHelper = sessionHelper;
            _azureManager = new AzureStorageManager();
        }

        public ActionResult Create()
        {
            var userViewModel = new CreateUserViewModel
            {
                Genders = _genderManager.GetAll(),
                Nationalities = _nationalityManager.GetAll(),
                CurrentUser = new User()
            };
            userViewModel.CurrentUser.Games = new List<Game>();

            return View(nameof(Create), userViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateUserViewModel userViewModel)
        {
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
                        _sessionHelper.Add("CurrentUser", user);
                        return RedirectToAction(nameof(EditIntegrations));
                    }
                }
            }

            ViewBag.Message = "An error occurred when creating the account.";
            return View(nameof(Create));
        }

        public ActionResult EditIntegrations()
        {
            User user = (User)_sessionHelper.Get("CurrentUser") as User;
            if (user == null)
            {
                return RedirectToAction("Login", "Login");
            }
            user = _userManger.GetById(user.ID);
            return View(nameof(EditIntegrations), user.UserIntegrations);
        }

        public ActionResult Update()
        {
            var user = _sessionHelper.Get("CurrentUser") as User;

            if (user != null)
            {
                var userViewModel = new UpdateUserViewModel
                {
                    CurrentUser = _userManger.GetById(user.ID),
                    Nationalities = _nationalityManager.GetAll(),
                    Genders = _genderManager.GetAll()
                };
                return View(nameof(Update), userViewModel);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        //[HttpPost]
        //public async Task<ActionResult> Update(UpdateUserViewModel userViewModel)
        //{
        //    var currentUser = userViewModel.CurrentUser;
        //    currentUser.ID = (_sessionHelper.Get("CurrentUser") as User).ID;
        //    //NOTE(Dave) This image path is set because it is not null-able in the database and ef throws validation error
        //    //NOTE(TH) Needs upload functionality from the UI.
        //    currentUser.ProfileImagePath = _azureManager.GetDefaultUrl();

        //    currentUser.UserTypeID = (int)UserType.UserTypes.User;

        //    if (ModelState.IsValid)
        //    {
        //        await _userManger.Update(currentUser);

        //        userViewModel.Genders = _genderManager.GetAll();
        //        userViewModel.Nationalities = _nationalityManager.GetAll();
        //        ViewBag.UpdateMessage = "Account Successfully updated.";

        //        return View(nameof(Update), userViewModel);
        //    }
        //    else
        //    {
        //        userViewModel.Genders = _genderManager.GetAll();
        //        userViewModel.Nationalities = _nationalityManager.GetAll();

        //        ViewBag.Message = "An error occurred when updating the account.";
        //        return View(nameof(Update), userViewModel);
        //    }
        //}

        [HttpPost] // (TH) Kept original above.
        public async Task<ActionResult> Update(UpdateUserViewModel userViewModel, HttpPostedFileBase file)
        {
            var currentUser = userViewModel.CurrentUser;
            currentUser.ID = (_sessionHelper.Get("CurrentUser") as User).ID;

            currentUser.UserTypeID = (int)UserType.UserTypes.User;

            if (ModelState.IsValid)
            {
                if (file == null || file.ContentLength <= 0)
                {
                    if (!_azureManager.UserImageExists(currentUser) || (currentUser.ProfileImagePath == _azureManager.GetDefaultUrl()))
                    {
                        currentUser.ProfileImagePath = _azureManager.GetDefaultUrl();
                        await _userManger.Update(currentUser);
                        ViewBag.UpdateMessage = "Account Successfully updated, No Profile Image."; // (TH) To know if no image was uploaded
                    }
                }
                else
                {
                    var convertedfile = ConvertToBytes(file.InputStream);
                    _azureManager.UploadImage(currentUser, convertedfile);
                    currentUser.ProfileImagePath = _azureManager.GetUserUrl(currentUser);
                    await _userManger.Update(currentUser);
                    ViewBag.UpdateMessage = "Account Successfully updated with new image."; // (TH) To know if an image was uploaded
                }

                userViewModel.Genders = _genderManager.GetAll();
                userViewModel.Nationalities = _nationalityManager.GetAll();
                //ViewBag.UpdateMessage = "Account Successfully updated.";

                return View(nameof(Update), userViewModel);
            }
            else
            {
                userViewModel.Genders = _genderManager.GetAll();
                userViewModel.Nationalities = _nationalityManager.GetAll();

                ViewBag.Message = "An error occurred when updating the account.";
                return View(nameof(Update), userViewModel);
            }
        }

        public ActionResult ChangePassword()
        {
            User currentUser = _sessionHelper.Get("CurrentUser") as User;

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

                return View(nameof(ChangePassword), viewModel);
            }
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel passwordViewModel)
        {
            if (ModelState.IsValid)
            {
                User user = _sessionHelper.Get("CurrentUser") as User;

                if (user != null)
                {
                    try
                    {
                        _userManger.UpdatePassword(passwordViewModel.CurrentPassword, passwordViewModel.NewPassword, user);
                    }
                    catch (InvalidPasswordException)
                    {
                        ViewBag.Message = "Invalid Password";
                        return View(nameof(ChangePassword));
                    }
                    return View("PasswordUpdated");
                }
            }

            ViewBag.Message = "An error occurred when updating your password.";
            return View(nameof(ChangePassword));
        }

        private byte[] ConvertToBytes(Stream file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}