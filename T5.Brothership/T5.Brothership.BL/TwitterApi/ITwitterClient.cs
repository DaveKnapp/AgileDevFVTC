namespace T5.Brothership.BL.TwitterApi
{
    public interface ITwitterClient
    {
        Tweetinvi.Models.ConsumerCredentials GetCustomerCredentials();
        string GetUserName(string accessToken, string accessSecret);
        TwitterApiCredentials ValidateTwitterAuth(string authId, string verifierCode);
    }
}