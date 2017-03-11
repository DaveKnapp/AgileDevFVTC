using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T5.Brothership.BL.IGDBApi
{
    public class IGDBGame
    {
        public int id { get; set; }
        public string name { get; set; }
        public Cover cover { get; set; }
    }

    public class Cover
    {
        public string url { get; set; }
        public string cloudinary_id { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }
}
