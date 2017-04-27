namespace T5.Brothership.Helpers
{
    public interface ISessionHelper
    {
        void Add(string sessionKey, object sessionValue);
        object Get(string sessionKey);
        void remove(string sessionKey);
    }
}