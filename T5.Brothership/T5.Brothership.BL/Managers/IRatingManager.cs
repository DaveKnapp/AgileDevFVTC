using System.Collections.Generic;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.BL.Managers
{
    public interface IRatingManager
    {
        void Dispose();
        List<Rating> GetAll();
    }
}