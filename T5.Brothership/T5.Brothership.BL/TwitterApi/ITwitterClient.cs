namespace T5.Brothership.BL.TwitterApi
{
    public interface ITwitterClient
    {
        Tweetinvi.Models.ConsumerCredentials GetCustomerCredentials();
        string GetUserURL(string accessToken, string accessSecret);
        TwitterApiCredentials ValidateTwitterAuth(string authId, string verifierCode);
    }
}