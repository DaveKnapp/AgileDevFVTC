using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T5.Brothership.BL.Integrations;
using T5.Brothership.BL.Managers;
using T5.Brothership.Entities.Models;
using T5.Brothership.ViewModels;
using System.Threading.Tasks;
using T5.Brothership.Helpers;

namespace T5.Brothership.Controllers
{
    public class UserController : Controller
    {
        ITwitchIntegration _twitchIntegration;
        ITwitterIntegration _twitterIntegration;
        IUserManager _userManager;
        IUserRatingManager _userRatingManger;
        AzureStorageManager _azureStorageManager = new AzureStorageManager();
        IRatingManager _ratingManager;
        ISessionHelper _sessionHelper;

        public UserController() : this(new TwitchIntegration(),
                                     new TwitterIntegration(),
                                     new UserManager(),
                                     new UserRatingManager(),
                                     new RatingManager(),
                                     new SessionHelper())
        { }

        public UserController(ITwitchIntegration twitchIntegration, ITwitterIntegration twitterintegration, IUserManager userManager,
                              IUserRatingManager userRatingManager, IRatingManager ratingManager, ISessionHelper sessionHelper)
        {
            _twitchIntegration = twitchIntegration;
            _twitterIntegration = twitterintegration;
            _userManager = userManager;
            _userRatingManger = userRatingManager;
            _ratingManager = ratingManager;
            _sessionHelper = sessionHelper;
        }

        [Route("{userName}")]
        public async Task<ActionResult> User(string userName)
        {
            User user = _userManager.GetByUserName(userName);

            if (user == null)
            {
                return HttpNotFound();
            }
            List<IntegrationInfo> integrationInfos = await GetUserIntegrationInfo(user);

            var viewModel = new UserPageViewModel
            {
                User = user,
                UserIntegrationInfos = integrationInfos,
                AverageRating = _userRatingManger.GetAverageRating(user.ID),
                IsUserLoggedIn = IsUserLoggedIn()
            };
            //TOOD Change to nameof
            return View(nameof(User), viewModel);
        }

        [Route("User/UserGames/{userName}")]
        public ActionResult UserGames(string userName)
        {
            var user = _userManager.GetByUserName(userName);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(nameof(UserGames), user);
        }

        [Route("User/UserRatings/{userName}")]
        public ActionResult UserRatings(string userName)
        {
            var user = _userManager.GetByUserName(userName);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(nameof(UserRatings), user);
        }

        [Route("User/Rate/{userName}")]
        public ActionResult Rate(string userName)
        {

            var loggedInUser = _sessionHelper.Get("CurrentUser") as User;

            if (loggedInUser == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var userToRate = _userManager.GetByUserName(userName);

            if (userToRate == null)
            {
                return HttpNotFound();
            }

            UserRatingViewModel viewModel = new UserRatingViewModel
            {
                Ratings = _ratingManager.GetAll(),
                UserRating = new UserRating
                {
                    UserBeingRatedID = userToRate.ID,
                    RaterUserID = loggedInUser.ID
                }
            };

            return View(nameof(Rate), viewModel);
        }

        [HttpPost]
        [Route("User/Rate/{userName}")]
        public ActionResult Rate(UserRatingViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var loggedInUser = _sessionHelper.Get("CurrentUser") as User;

                if (loggedInUser == null)
                {
                   return RedirectToAction("Login", "Login");
                }

                var userRating = viewModel.UserRating;
                userRating.RaterUserID = loggedInUser.ID;

                _userRatingManger.Add(userRating);

                string ratedUserName = _userManager.GetById(viewModel.UserRating.UserBeingRatedID).UserName;

                return RedirectToAction(nameof(User),"User", new { userName = ratedUserName });
            }
            else
            {
                viewModel.Ratings = _ratingManager.GetAll();
                ViewBag.Message = "An error occurred when submitting rating.";

                return View(nameof(Rate), viewModel);
            }
        }

        private async Task<List<IntegrationInfo>> GetUserIntegrationInfo(User user)
        {
            var integrationInfos = new List<IntegrationInfo>();

            foreach (var integration in user.UserIntegrations)
            {
                try
                {
                    switch (integration.IntegrationTypeID)
                    {
                        case (int)IntegrationType.IntegrationTypes.Twitch:
                            integrationInfos.Add(new IntegrationInfo
                            {
                                IntegrationType = (IntegrationType.IntegrationTypes)Enum.ToObject(typeof(IntegrationType.IntegrationTypes), integration.IntegrationTypeID),
                                IsUserLive = await _twitchIntegration.IsUserLive(user.ID),
                                UserLiveStreamURL = "https://www.twitch.tv/" + integration.UserName
                            }
                            );
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception)
                {
                    //TODO(Dave) Log error
                }

            }
            return integrationInfos;
        }

        private bool IsUserLoggedIn()
        {
            var LoggedInUser = _sessionHelper.Get("CurrentUser") as User;

            return LoggedInUser != null;
        }
    }
}