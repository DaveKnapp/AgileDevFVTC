using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.BL.Managers;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.BL.Test.ManagerFakes
{
    public class NationalityManagerFake : INationalityManager
    {
        private List<Nationality> _fakeNationalies;

        public NationalityManagerFake()
        {
            _fakeNationalies = CreateFakeNationalies();
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public List<Nationality> GetAll()
        {
            return _fakeNationalies;
        }

        private List<Nationality> CreateFakeNationalies()
        {
            return new List<Nationality>
            {
                new Nationality
                {
                    ID = 1,
                    Description = "US and A"
                },
                new Nationality
                {
                    ID = 2,
                    Description = "China"
                },
                new Nationality
                {
                    ID = 3,
                    Description = "Europe"
                },
                new Nationality
                {
                    ID = 4,
                    Description = "Mexico"
                },
                new Nationality
                {
                    ID = 5,
                    Description = "Canada"
                }
            };
        }
    }
}
