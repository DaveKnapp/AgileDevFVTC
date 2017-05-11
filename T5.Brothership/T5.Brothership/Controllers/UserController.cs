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
    {//TODO(Dave) Add Video ContentCombinder fake
        ITwitchIntegration _twitchIntegration;
        ITwitterIntegration _twitterIntegration;
        IUserManager _userManager;
        IUserRatingManager _userRatingManger;
        AzureStorageManager _azureStorageManager = new AzureStorageManager();
        IRatingManager _ratingManager;
        ISessionHelper _sessionHelper;
        IYoutubeIntegration _youtubeIntegration;
        IntegrationVideoCombiner _videoContentGetter = new IntegrationVideoCombiner();


        public UserController() : this(new TwitchIntegration(),
                                     new TwitterIntegration(),
                                     new UserManager(),
                                     new UserRatingManager(),
                                     new RatingManager(),
                                     new SessionHelper(),
                                     new YoutubeIntegration())
        { }

        public UserController(ITwitchIntegration twitchIntegration, ITwitterIntegration twitterIntegration, IUserManager userManager,
                              IUserRatingManager userRatingManager, IRatingManager ratingManager, ISessionHelper sessionHelper, IYoutubeIntegration youtubeInegration)
        {
            _twitchIntegration = twitchIntegration;
            _twitterIntegration = twitterIntegration;
            _userManager = userManager;
            _userRatingManger = userRatingManager;
            _ratingManager = ratingManager;
            _sessionHelper = sessionHelper;
            _youtubeIntegration = youtubeInegration;
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
                IsUserLoggedIn = IsUserLoggedIn(),
                HasLoggedInUserRated = HasLoggedInUserRated(user.ID),
                IsUserFollowing = IsUserFollowedToUser(user.ID)
            };
            try
            {
                viewModel.RecentContent = await _videoContentGetter.GetRecentVideos(user.ID, 7);
            }
            catch (Exception)
            {
                ViewBag.Message = "An error occured when loading recent content.";
            }

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

        [Route("User/UserContent/{userName}")]
        public async Task<ActionResult> UserContent(string userName)
        {
            var user = _userManager.GetByUserName(userName);

            if (user == null)
            {
                return HttpNotFound();
            }

            var userVideos = await _videoContentGetter.GetRecentVideos(user.ID, 100);

            return View(nameof(UserContent), userVideos);
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

                return RedirectToAction(nameof(User), "User", new { userName = ratedUserName });
            }
            else
            {
                viewModel.Ratings = _ratingManager.GetAll();
                ViewBag.Message = "An error occurred when submitting rating.";

                return View(nameof(Rate), viewModel);
            }
        }

        [Route("User/EditRating/{userName}")]
        public ActionResult EditRating(string userName)
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
                UserRating = _userRatingManger.GetById(loggedInUser.ID, userToRate.ID)
            };

            if (viewModel.UserRating == null)
            {
                return HttpNotFound();
            }

            return View(nameof(EditRating), viewModel);
        }

        [HttpPost]
        [Route("User/EditRating/{userName}")]
        public ActionResult EditRating(UserRatingViewModel viewModel)
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

                _userRatingManger.Update(userRating);

                string ratedUserName = _userManager.GetById(viewModel.UserRating.UserBeingRatedID).UserName;

                return RedirectToAction(nameof(User), "User", new { userName = ratedUserName });
            }
            else
            {
                viewModel.Ratings = _ratingManager.GetAll();
                ViewBag.Message = "An error occurred when submitting rating.";

                return View(nameof(Rate), viewModel);
            }
        }

        [Route("User/{userName}/following")]
        public ActionResult Following(string userName)
        {
            {
                var user = _userManager.GetByUserName(userName);

                return View(nameof(Following), user.FollowedUsers);
            }
        }

        [Route("User/{userName}/follow)")]
        public ActionResult Follow(string userName)
        {
            {
                var loggedInUser = _sessionHelper.Get("CurrentUser") as User;

                if (loggedInUser == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                var userToFollow = _userManager.GetByUserName(userName);
                _userManager.FollowUser(userToFollow.ID, loggedInUser.ID);

                return RedirectToAction(nameof(User), "User", new { userName = userToFollow.UserName });
            }
        }

        [Route("User/{userName}/unFollow)")]
        public ActionResult UnFollow(string userName)
        {
            {
                var loggedInUser = _sessionHelper.Get("CurrentUser") as User;

                if (loggedInUser == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                var userToUnFollow = _userManager.GetByUserName(userName);
                _userManager.UnFollowUser(userToUnFollow.ID, loggedInUser.ID);

                return RedirectToAction(nameof(User), "User", new { userName = userToUnFollow.UserName });
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
                        case (int)IntegrationType.IntegrationTypes.Youtube:

                            var integrationInfo = new IntegrationInfo
                            {
                                UserLiveStreamURL = await _youtubeIntegration.GetLiveStreamURLIfLive(user.ID),
                                IntegrationType = (IntegrationType.IntegrationTypes)Enum.ToObject(typeof(IntegrationType.IntegrationTypes), integration.IntegrationTypeID)
                            };

                            integrationInfo.IsUserLive = integrationInfo.UserLiveStreamURL == null ? false : true;
                            integrationInfos.Add(integrationInfo);
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

        private bool HasLoggedInUserRated(int ratedUserID)
        {
            var loggedInUser = _sessionHelper.Get("CurrentUser") as User;
            if (loggedInUser == null)
            {
                return false;
            }
            return _userRatingManger.GetById(loggedInUser.ID, ratedUserID) == null ? false : true;
        }

        private bool IsUserFollowedToUser(int UserId)
        {
            var loggedInUser = _sessionHelper.Get("CurrentUser") as User;
            if (loggedInUser == null)
            {
                return false;
            }
            var userFollow = _userManager.GetById(UserId);
            return _userManager.GetById(loggedInUser.ID).FollowedUsers.Contains(userFollow);
        }
    }
}