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
        RatingManager _ratingManager = new RatingManager();

        [Route("{userName}")]
        public async Task<ActionResult> User(string userName)
        {
            //Todo(Dave) Add error page of user not found
            User user = _userManager.GetByUserName(userName);

            //Refresh to integration to get new url if user changed userName
            _twitterIntegration.Refresh(user.ID);

            List<IntegrationInfo> integrationInfos = await GetUserIntegrationInfo(user);
            var viewModel = new UserPageViewModel
            {
                User = user,
                UserIntegrationInfos = integrationInfos,
                AverageRating = _userRatingManger.GetAverageRating(user.ID)
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

        //var user = Session["CurrentUser"] as User;
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
            //TOOD Check if model state is valid
            var loggedInUser = Session["CurrentUser"] as User;

            var userRating = viewModel.UserRating;
            userRating.RaterUserID = loggedInUser.ID;

            _userRatingManger.Add(userRating);

            string ratedUserName = _userManager.GetById(viewModel.UserRating.UserBeingRatedID).UserName;
            return RedirectToAction(nameof(User), new {userName = ratedUserName });
        }

        private async Task<List<IntegrationInfo>> GetUserIntegrationInfo(User user)
        {
            var integrationInfos = new List<IntegrationInfo>();

            foreach (var integration in user.UserIntegrations)
            {
                //TODO Add error handleing clients fail
                try
                {
                    switch (integration.IntegrationTypeID)
                    {
                        case (int)IntegrationType.IntegrationTypes.Twitch:
                            integrationInfos.Add(new IntegrationInfo
                            {
                                IntegrationType = (IntegrationType.IntegrationTypes)Enum.ToObject(typeof(IntegrationType.IntegrationTypes), integration.IntegrationTypeID),
                                IsUserLive = await _twitchIntegration.IsUserLive(user.ID),
                                UserLiveStreamURL = integration.URL
                            }
                            );
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception)
                {
                    //TODO(Dave) handel client fail
                }

            }
            return integrationInfos;
        }
    }
}