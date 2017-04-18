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
        UserManager _usermanager = new UserManager();


        [Route("{userName}")]
        public async Task<ActionResult> User(string userName)
        {
            //Todo(Dave) Add error page of user not found
            User user = _usermanager.GetByUserName(userName);

            //Refresh to integration to get new url if user changed userName
            _twitterIntegration.Refresh(user.ID);

            List<IntegrationInfo> integrationInfos = await GetUserIntegrationInfo(user);
            var viewModel = new UserPageViewModel
            {
                User = user,
                UserIntegrationInfos = integrationInfos
            };

            return View("user", viewModel);
        }

        [Route("User/UserGames/{userName}")]
        public ActionResult UserGames(string userName)
        {
            var user = _usermanager.GetByUserName(userName);
            return View(user);
        }

        [Route("User/UserRatings/{userName}")]
        public ActionResult UserRatings(string userName)
        {
            var user = _usermanager.GetByUserName(userName);
            return View(user);
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