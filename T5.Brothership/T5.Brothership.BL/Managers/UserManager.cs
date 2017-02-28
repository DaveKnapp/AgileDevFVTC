﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;
using T5.Brothership.PL.Repositories;
using T5.Brothership.PL;
using System.Data.Entity;

namespace T5.Brothership.BL.Managers
{
    public class UserManager:IDisposable
    {//Business logic goes in this class
        IBrothershipUnitOfWork _unitOfWork = new BrothershipUnitOfWork();

        public UserManager(IBrothershipUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public User GetById(int id)
        {
            return _unitOfWork.Users.GetById(id);
        }

        public User Login(string userName, string password)
        {
            throw new NotImplementedException();
        }
    }
}
