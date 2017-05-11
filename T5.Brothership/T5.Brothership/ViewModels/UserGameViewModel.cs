using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using T5.Brothership.BL.Managers;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.ViewModels
{
    public class UserGameViewModel
    {
        UserManager UserManager = new UserManager();
        GameManager GameManager = new GameManager();
    }
}