using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;
using T5.Brothership.PL.Repositories;

namespace T5.Brothership.PL.Test.FakeRepositories
{
    public class SocialMediaTypeFakeRepository : ISocialMediaTypeRepository
    {
       private List<SocialMediaType> _fakeSocialMediaTypes = new List<SocialMediaType>();

        SocialMediaTypeFakeRepository()
        {
            InitializeFakeSocialMediaTypes();
        }

        public void Add(SocialMediaType entity)
        {
            entity.ID = GenerateSocialMediaTypeId();
            _fakeSocialMediaTypes.Add(entity);
        }

        public void Delete(int id)
        {
            var socialMediaType = _fakeSocialMediaTypes.Single(p => p.ID == id);
            _fakeSocialMediaTypes.Remove(socialMediaType);
        }

        public void Delete(SocialMediaType entity)
        {
            var socialMediaType = _fakeSocialMediaTypes.Single(p => p.ID == entity.ID);
            _fakeSocialMediaTypes.Remove(socialMediaType);
        }

        public void Dispose()
        {
            _fakeSocialMediaTypes = null;
        }

        public IQueryable<SocialMediaType> GetAll()
        {
            return _fakeSocialMediaTypes.AsQueryable();
        }

        public SocialMediaType GetById(int id)
        {
            return _fakeSocialMediaTypes.FirstOrDefault(p => p.ID == id);
        }

        public void SaveChanges()
        {
        }

        public void Update(SocialMediaType entity)
        {
            var entityIndex = _fakeSocialMediaTypes.IndexOf(entity);
            _fakeSocialMediaTypes[entityIndex] = entity;
        }

        private int GenerateSocialMediaTypeId()
        {
           return _fakeSocialMediaTypes.Max(p => p.ID);
        }

        private void InitializeFakeSocialMediaTypes()
        {
            _fakeSocialMediaTypes.Add(new SocialMediaType
            {
                ID = 1,
                Description = "FaceBook"
            });

            _fakeSocialMediaTypes.Add(new SocialMediaType
            {
                ID = 2,
                Description = "Twitter"
            });

            _fakeSocialMediaTypes.Add(new SocialMediaType
            {
                ID = 3,
                Description = "Instagram"
            });
        }
    }
}
