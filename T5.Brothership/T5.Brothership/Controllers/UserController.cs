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

namespace T5.Brothership.Controllers
{
    public class UserController : Controller
    {
        TwitchIntegration _twitchIntegration = new TwitchIntegration();
        TwitterIntegration _twitterIntegration = new TwitterIntegration();
        UserManager _userManager = new UserManager();
        UserRatingManager _userRatingManger = new UserRatingManager();
        AzureStorageManager _azureStorageManager = new AzureStorageManager();
        RatingManager _ratingManager = new RatingManager();

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

            return View("user", viewModel);
        }

        [Route("User/UserGames/{userName}")]
        public ActionResult UserGames(string userName)
        {
            var user = _userManager.GetByUserName(userName);
            return View(user);
        }

        [Route("User/UserRatings/{userName}")]
        public ActionResult UserRatings(string userName)
        {
            var user = _userManager.GetByUserName(userName);
            return View(user);
        }

        [Route("User/Rate/{userName}")]
        public ActionResult Rate(string userName)
        {

            var loggedInUser = Session["CurrentUser"] as User;
            var userToRate = _userManager.GetByUserName(userName);

            UserRating userRating = new UserRating
            {
                UserBeingRatedID = userToRate.ID ,
                RaterUserID = loggedInUser.ID
            };

            UserRatingViewModel viewModel = new UserRatingViewModel
            {
                UserRating = userRating,
                Ratings = _ratingManager.GetAll()
            };

            return View(viewModel);
        }

        [HttpPost]
        [Route("User/Rate/{userName}")]
        public ActionResult Rate(UserRatingViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var loggedInUser = Session["CurrentUser"] as User;

                var userRating = viewModel.UserRating;
                userRating.RaterUserID = loggedInUser.ID;

                _userRatingManger.Add(userRating);

                string ratedUserName = _userManager.GetById(viewModel.UserRating.UserBeingRatedID).UserName;
                return RedirectToAction(nameof(User), new { userName = ratedUserName });
            }
            else
            {
                ViewBag.Message = "An error occurred when creating the account.";

                viewModel.Ratings = _ratingManager.GetAll();
                ViewBag.Message = "An error occurred when submitting rating";
                return View("Rate", viewModel);
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
            var LoggedInUser = Session["CurrentUser"] as User;

            return LoggedInUser != null;
        }
    }
}