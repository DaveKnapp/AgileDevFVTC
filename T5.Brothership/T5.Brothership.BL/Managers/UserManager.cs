﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;
using T5.Brothership.PL.Repositories;
using T5.Brothership.PL;
using System.Data.Entity;
using T5.Brothership.BL.Helpers;
using T5.Brothership.BL.IGDBApi;
using T5.Brothership.BL.Exceptions;

namespace T5.Brothership.BL.Managers
{
    public class UserManager : IDisposable
    {
        IBrothershipUnitOfWork _unitOfWork;
        GameManager _gameManager;

        public UserManager()
        {
            _unitOfWork = new BrothershipUnitOfWork();
            _gameManager = new GameManager(_unitOfWork);
        }

        public UserManager(IBrothershipUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _gameManager = new GameManager(_unitOfWork);
        }

        public UserManager(IBrothershipUnitOfWork unitOfWork, IGameAPIService gameApiService)
        {
            _unitOfWork = unitOfWork;
            _gameManager = new GameManager(_unitOfWork, gameApiService);
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
            _gameManager?.Dispose();
            GC.SuppressFinalize(this);
        }

        public User GetById(int id)
        {
            return _unitOfWork.Users.GetById(id);
        }

        public User Login(string userNameOrEmail, string password)
        {
            var user = _unitOfWork.Users.GetByUsernameOrEmail(userNameOrEmail);

            if (user == null)
            {
                return new InvalidUser();
            }

            var passwordHelper = new PasswordHelper();
            var hashedPassword = new HashedPassword { PasswordHash = user.UserLogin.PasswordHash, Salt = user.UserLogin.Salt };

            return passwordHelper.IsPasswordMatch(password, hashedPassword) ? user : new InvalidUser();
        }

        public async Task Add(User user, string password)
        {
            //TODO Create Test for username in use
            var newUser = user;

            if (UserNameExists(user.UserName))
            {
                throw new UsernameAlreadyExistsException("UserName is being used by another user");
            }

            newUser.UserLogin = CreateUserLogin(password);
            newUser.DateJoined = DateTime.Now;

            await _gameManager.AddGamesIfNotExistsAsync(CreateIgdbIdArray(user.Games));
            newUser.Games = _gameManager.GetByIgdbIds(CreateIgdbIdArray(user.Games));

            _unitOfWork.Users.Add(newUser);
            _unitOfWork.Commit();
        }

        public bool UserNameExists(string userName)
        {
            return _unitOfWork.Users.GetByUsernameOrEmail(userName) != null;
        }

        public async Task Update(User updatedUser)
        {
            User currentUser = _unitOfWork.Users.GetById(updatedUser.ID);
            currentUser.Bio = updatedUser.Bio;
            currentUser.DOB = updatedUser.DOB;
            currentUser.Email = updatedUser.Email;
            currentUser.GenderId = updatedUser.GenderId;
            currentUser.NationalityID = updatedUser.NationalityID;
            currentUser.UserTypeID = updatedUser.UserTypeID;
            currentUser.ProfileImagePath = updatedUser.ProfileImagePath;

            currentUser.Games.Clear();

            await _gameManager.AddGamesIfNotExistsAsync(CreateIgdbIdArray(updatedUser.Games));

            currentUser.Games = _gameManager.GetByIgdbIds(CreateIgdbIdArray(updatedUser.Games));

            _unitOfWork.Users.Update(currentUser);
            _unitOfWork.Commit();
        }

        public List<User> GetAllUsers()
        {
            return _unitOfWork.Users.GetAll().ToList();
        }

        private int[] CreateIgdbIdArray(ICollection<Game> games)
        {
            var gameDbIds = new List<int>();

            foreach (var game in games)
            {
                if (game.igdbID != null)
                {
                    gameDbIds.Add((int)game.igdbID);
                }
            }

            return gameDbIds.ToArray();
        }

        private UserLogin CreateUserLogin(string password)
        {
            var passwordHelper = new PasswordHelper();
            var hashedPassword = passwordHelper.GeneratePasswordHash(password);

            return new UserLogin
            {
                PasswordHash = hashedPassword.PasswordHash,
                Salt = hashedPassword.Salt
            };
        }

    }
}
