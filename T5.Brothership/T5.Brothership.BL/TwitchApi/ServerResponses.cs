using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T5.Brothership.BL.TwitchApi
{
    //TODO(Check if twitch API fails auth if properties in unit of work are not got.
    public class AuthorizationResponse
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public string[] scope { get; set; }
    }

    public class StreamResponse
    {
        public Stream stream { get; set; }
        public Channel channel { get; set; }
    }

    public class ChannelVideoResponse
    { 
        public int _total { get; set; }
        public Video[] videos { get; set; }
    }


    public class Video
    {
        public string _id { get; set; }
        public long broadcast_id { get; set; }
        public string broadcast_type { get; set; }
        public VideoChannel channel { get; set; }
        public DateTime created_at { get; set; }
        public string description { get; set; }
        public string description_html { get; set; }
        public Fps fps { get; set; }
        public string game { get; set; }
        public string language { get; set; }
        public int length { get; set; }
        public Preview preview { get; set; }
        public DateTime published_at { get; set; }
        public Resolutions resolutions { get; set; }
        public string status { get; set; }
        public string tag_list { get; set; }
        public Thumbnails thumbnails { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string viewable { get; set; }
        public object viewable_at { get; set; }
        public int views { get; set; }
    }

    public class VideoChannel
    {
        public string _id { get; set; }
        public string display_name { get; set; }
        public string name { get; set; }
    }

    public class Fps
    {
        public float chunked { get; set; }
        public float high { get; set; }
        public float low { get; set; }
        public float medium { get; set; }
        public float mobile { get; set; }
    }

    public class Preview
    {
        public string large { get; set; }
        public string medium { get; set; }
        public string small { get; set; }
        public string template { get; set; }
    }

    public class Resolutions
    {
        public string chunked { get; set; }
        public string high { get; set; }
        public string low { get; set; }
        public string medium { get; set; }
        public string mobile { get; set; }
    }

    public class Thumbnails
    {
        public Large[] large { get; set; }
        public Medium[] medium { get; set; }
        public Small[] small { get; set; }
        public Template[] template { get; set; }
    }

    public class Large
    {
        public string type { get; set; }
        public string url { get; set; }
    }

    public class Medium
    {
        public string type { get; set; }
        public string url { get; set; }
    }

    public class Small
    {
        public string type { get; set; }
        public string url { get; set; }
    }

    public class Template
    {
        public string type { get; set; }
        public string url { get; set; }
    }


    public class Stream
    {
        public long _id { get; set; }
        public string game { get; set; }
        public int viewers { get; set; }
        public int video_height { get; set; }
        public int average_fps { get; set; }
        public int delay { get; set; }
        public DateTime created_at { get; set; }
        public bool is_playlist { get; set; }
        public Preview preview { get; set; }
        public Channel channel { get; set; }
    }


    public class Channel
    {
        public bool? mature { get; set; }
        public string status { get; set; }
        public string broadcaster_language { get; set; }
        public string display_name { get; set; }
        public string game { get; set; }
        public string language { get; set; }
        public int _id { get; set; }
        public string name { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public bool partner { get; set; }
        public string logo { get; set; }
        public string video_banner { get; set; }
        public string profile_banner { get; set; }
        public object profile_banner_background_color { get; set; }
        public string url { get; set; }
        public int views { get; set; }
        public int followers { get; set; }
    }

}
