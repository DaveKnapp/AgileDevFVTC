using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.PL.Repositories
{
    public interface IUserSocialMediaRepository
    {
        IQueryable<UserSocialJunc> GetAll();
        IQueryable<UserSocialJunc> GetAllByUser(int userId);
        UserSocialJunc GetById(int userId, int socialMediaTypeId);
        void Add(UserSocialJunc entity);
        void Update(UserSocialJunc entity);
        void Delete(UserSocialJunc entity);
        void Delete(int userId, int socialMediaTypeId);
        void SaveChanges();
    }
}
