using System.Collections.Generic;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.BL.Integrations
{
    public interface ITwitchVideoLoader
    {
        bool HasMoreItems { get; set; }

        Task<List<VideoContent>> GetVideos();
    }
}