using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.BL.Managers;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.BL.Test.ManagerFakes
{
    public class AzureStorgeManagerStub : IAzureStorageManager
    {
        public void DeleteImage(User user)
        {
        }

        public void Dispose()
        {
        }

        public string GetDefaultUrl()
        {
            return "www.default.com";
        }

        public string UploadImage(User user, byte[] imageArray)
        {
            return "www." + user;
        }
    }
}
