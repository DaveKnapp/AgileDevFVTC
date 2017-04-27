using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.BL.Managers;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.BL.Test.ManagerFakes
{
    public class RatingManagerFake : IRatingManager
    {
        public void Dispose()
        {
        }

        public List<Rating> GetAll()
        {
            return new List<Rating>
            {
                new Rating
                {
                    ID = 1,
                    Description = "OneStar"
                },
                new Rating
                {
                    ID = 2,
                    Description = "TwoStars"
                },
                new Rating
                {
                    ID = 3,
                    Description = "ThreeStars"
                },
                new Rating
                {
                    ID = 4,
                    Description = "FourStars"
                },
                new Rating
                {
                    ID = 5,
                    Description = "FiveStars"
                },
             };
        }
    }
}
