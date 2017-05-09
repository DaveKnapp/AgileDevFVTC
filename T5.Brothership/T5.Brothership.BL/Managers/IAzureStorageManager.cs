using T5.Brothership.Entities.Models;

namespace T5.Brothership.BL.Managers
{
    public interface IAzureStorageManager
    {
        void DeleteImage(User user);
        void Dispose();
        string GetDefaultUrl();
        string UploadImage(User user, byte[] imageArray);
        string GetUserUrl(User user);
    }
}