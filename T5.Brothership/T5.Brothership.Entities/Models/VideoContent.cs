using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T5.Brothership.Entities.Models
{
    public class VideoContent
    {
        public string Id { get; set; }
        public DateTime UploadTime { get; set; }
        public IntegrationType.IntegrationTypes ContentType { get; set; }
        //Note Currently PreviewImageURL is only used for Twitch 
        public string PreviewImgUrl { get; set; }
        public string Title { get; set; }
    }
}
