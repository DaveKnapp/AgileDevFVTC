﻿using System;

namespace T5.Brothership.BL.TwitchApi
{
    //TODO(Dave) Do I want to store this somewhere else for security?
    public static class ApiCredentials
    {
        public const string CLIENT_ID = "6ivvf370iamtajba1s4tq6fewg9tua";
        public const string SECRET = "ra16qnvzs0ipifhcczm7egv5kqrvdc";
        //TOOD(Dave) Is there a way to Set redirect automaticaly?
   //     public const string REDIRECT_URL = "http://localhost:60920/Twitch/Authorize";
        public const string REDIRECT_URL = "http://brothership.azurewebsites.net/Twitch/Authorize";
      
    }
}