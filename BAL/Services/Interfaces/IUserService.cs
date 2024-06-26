﻿using BAL.ViewModels;
using BAL.ViewModels.Admin;
using BAL.ViewModels.Authenticates;
using BAL.ViewModels.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IUserService
    {
        Task<AuthenResponse> AuthenticateUser(AuthenRequest request);
		Task<AuthenResponse> CreateTemporaryNewUser(AuthenRequest request);
        Task<AuthenResponse> CreateGuestUser(AuthenRequest request);
        Task<AuthenResponse> AuthenticateUserEmail(string email);
        Task<UserViewModel?> GetById(int id);
        Task<UserViewModel?> GetByMemberId(string memId);
        Task<bool> GetBoolById(int id);
        Task<bool> IsUserExistByEmail(string email);
        Task<bool> IsUserExistByUsername(string username);
        Task<bool> UpdateUserAvatar(string memId, string imagePath);
        Task<UserViewModel?> GetByLogin(string username, string password);
        /* void Create(UserViewModel entity);*/
        void Create(UserViewModel entity, CreateNewMember newmem = null);
        void Create(UserViewModel entity, CreateNewEmployee newmem = null);
        /*void Update(UserViewModel entity);*/
        void Update(UserViewModel entity);
        void UpdatePassword(UserViewModel entity);
        Task<UserViewModel?> GetByEmailModel(string email);
        Task<int> GetIdByUsername(string username);
        Task<int> CountUser();
        Task<int> CountUserByRole(string role);
    }
}
