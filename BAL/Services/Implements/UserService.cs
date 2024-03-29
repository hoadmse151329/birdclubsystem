﻿using AutoMapper;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using BAL.ViewModels.Authenticates;
using BAL.ViewModels.Member;
using DAL.Infrastructure;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTService _jwtService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public UserService( IUnitOfWork unitOfWork, 
            IMapper mapper, 
            IJWTService jWTServices, 
            IEmailService emailSender,
            IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtService = jWTServices;
            _emailService = emailSender;
            _configuration = configuration;
        }

        public async Task<AuthenResponse> AuthenticateUser(AuthenRequest request)
        {
            var user = await _unitOfWork.UserRepository.GetByLogin(request.Username, request.Password);
            if (user != null)
            {
                //var role = _unitOfWork.UserRepository
                var accessToken = _jwtService.GenerateJWTToken(user.MemberId, user.UserName, user.Role, _configuration);
                return new AuthenResponse()
                {
                    UserId = user.MemberId,
                    RoleName = user.Role,
                    AccessToken = accessToken
                };
            }
            return null;
        }

        public async Task<AuthenResponse> AuthenticateUserEmail(string email)
        {
            var user = await _unitOfWork.UserRepository.GetByEmail(email);
            if (user != null)
            {
                var accessToken = _jwtService.GenerateJWTToken(user.MemberId, user.UserName, user.Member.Role, _configuration);
                return new AuthenResponse()
                {
                    UserId = user.MemberId,
                    RoleName = user.Member.Role,
                    AccessToken = accessToken
                };
            }

            return null;
        }

        public void Create(UserViewModel entity, CreateNewMember newmem = null)
        {
            var usr = _mapper.Map<User>(entity);
			usr.Member = new Member();
			usr.Member.MemberId = Guid.NewGuid().ToString();
			usr.Member.Status = "Active";
			usr.Member.Email = entity.Email;
			if (newmem != null)
            {
                usr.Member.FullName = newmem.FullName;
                usr.Member.UserName = newmem.UserName;
				usr.Member.Gender = newmem.Gender;
				usr.Member.Address = newmem.Address;
                usr.Member.Phone = newmem.Phone;
			}
            _unitOfWork.UserRepository.Create(usr);
            _unitOfWork.Save();
        }

		public async Task<bool> GetBoolById(int id)
		{
            var user = await _unitOfWork.UserRepository.GetByIdNoTracking(id);
            if(user != null)
            {
                return true;
            }
            return false;
		}

		public bool GetByEmail(string email)
        {
            var user = _unitOfWork.UserRepository.GetByEmail(email);
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public async Task<UserViewModel?> GetByEmailModel(string email)
        {
            var user = await _unitOfWork.UserRepository.GetByEmail(email);
            if (user != null)
            {
                var usr = _mapper.Map<UserViewModel>(user);
                return usr;
            }
            return null;
        }

        public async Task<UserViewModel?> GetById(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdNoTracking(id);
            if (user != null)
            {
				var mem = await _unitOfWork.MemberRepository.GetByIdNoTracking(user.MemberId);
				var usr = _mapper.Map<UserViewModel>(user);
                usr.Email = mem.Email;
                return usr;
            }
            return null;
        }

        public async Task<UserViewModel?> GetByLogin(string username, string password)
        {
            var user = await _unitOfWork.UserRepository.GetByLogin(username, password);
            if (user != null)
            {
                var usr = _mapper.Map<UserViewModel>(user);
                return usr;
            }
            return null;
        }

        public async Task<UserViewModel?> GetByMemberId(string memId)
        {
            var user = await _unitOfWork.UserRepository.GetByMemberId(memId);
            if (user != null)
            {
                var usr = _mapper.Map<UserViewModel>(user);
                return usr;
            }
            return null;
        }

        public void Update(UserViewModel entity)
        {
            var usr = _mapper.Map<User>(entity);
            if (usr.MemberId != null)
            {
				var usrmem =  _unitOfWork.MemberRepository.GetByIdNoTracking(usr.MemberId).Result;
                if(usrmem == null)
                {
                    usr.Member = new Member()
                    {
                        Email = entity.Email
                    };
                } else
                usrmem.Email = entity.Email;

                usr.Role= entity.Role;
                usr.Member = usrmem;
			}
            _unitOfWork.UserRepository.Update(usr);
            _unitOfWork.Save();
        }

        public async Task<bool> UpdateUserAvatar(string memId, string imagePath)
        {
            var isChanged = await _unitOfWork.UserRepository.ChangeUserAvatar(memId, imagePath);
            if (isChanged) return true;
            return false;
        }
    }
}
