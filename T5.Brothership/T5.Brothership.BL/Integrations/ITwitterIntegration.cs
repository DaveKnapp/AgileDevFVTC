using Tweetinvi.Models;

namespace T5.Brothership.BL.Integrations
{
    public interface ITwitterIntegration
    {
        void DeAuthorize(int userId);
        void Dispose();
        ConsumerCredentials GetCustomerCredentials();
        void Refresh(int userId);
        void ValidateTwitterAuth(int userId, string authId, string verifierCode);
    }
}