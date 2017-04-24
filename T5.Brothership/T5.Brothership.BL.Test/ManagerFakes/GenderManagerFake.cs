using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.BL.Managers;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.BL.Test.ManagerFakes
{
    public class GenderManagerFake : IGenderManager
    {
        private List<Gender> _fakeGenders;

        public GenderManagerFake()
        {
            _fakeGenders = CreateFakeGenders();
        }

        public void Dispose()
        {
        }

        public List<Gender> GetAll()
        {
            return _fakeGenders;
        }

        private List<Gender> CreateFakeGenders()
        {
            return new List<Gender>
           {
                new Gender
                {
                    Id = 1,
                    Description = "Male"
                },

                new Gender
                {
                    Id = 2,
                    Description = "Female"
                },

                new Gender
                {
                    Id = 3,
                    Description = "Human"
                }
            };
        }
    }
}