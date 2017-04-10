using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T5.Brothership.BL.Integrators;
using T5.Brothership.BL.Managers;
using T5.Brothership.Entities.Models;
using T5.Brothership.ViewModels;
using System.Threading.Tasks;

namespace T5.Brothership.Controllers
{
    public class UserController : Controller
    {
        TwitchIntegrator _twitchIntegrator = new TwitchIntegrator();
        UserManager _usermanager = new UserManager();


        [Route("{userName}")]
        public async Task<ActionResult> User(string userName)
        {

            User user = _usermanager.GetByUserName(userName);
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
                switch (integration.IntegrationTypeID)
                {
                    case (int)IntegrationType.IntegrationTypes.Twitch:
                        integrationInfos.Add(new IntegrationInfo
                        {
                            IntegrationType = (IntegrationType.IntegrationTypes)Enum.ToObject(typeof(IntegrationType.IntegrationTypes), integration.IntegrationTypeID),
                            IsUserLive = await _twitchIntegrator.IsUserLive(user.ID),
                            UserLiveStreamURL = integration.URL
                        }
                        );
                        break;
                    default:
                        break;
                }
            }
            return integrationInfos;
        }
    }
}