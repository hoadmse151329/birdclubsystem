using AutoMapper;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using BAL.ViewModels.Authenticates;
using DAL.Infrastructure;
using DAL.Models;
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

        public UserService(IUnitOfWork unitOfWork, 
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

        public AuthenResponse AuthenticateUser(AuthenRequest request)
        {
            var user = _unitOfWork.UserRepository.GetByLogin(request.Username, request.Password);
            if (user != null)
            {
                //var role = _unitOfWork.UserRepository
                var accessToken = _jwtService.GenerateJWTToken(user.UserId, user.UserName, user.Member.Role, _configuration);
                return new AuthenResponse()
                {
                    UserId = user.UserId,
                    RoleName = user.Member.Role,
                    AccessToken = accessToken
                };
            }
            return null;
        }

        public AuthenResponse AuthenticateUserEmail(string email)
        {
            var user = _unitOfWork.UserRepository.GetByEmail(email);
            if (user != null)
            {
                var accessToken = _jwtService.GenerateJWTToken(user.UserId, user.UserName, user.Member.Role, _configuration);
                return new AuthenResponse()
                {
                    UserId = user.UserId,
                    RoleName = user.Member.Role,
                    AccessToken = accessToken
                };
            }

            return null;
        }

        public void Create(UserViewModel entity)
        {
            var usr = _mapper.Map<User>(entity);
            usr.Member = new Member();
            usr.Member.MemberId = 0;
            usr.Member.Role = "Member";
            usr.Member.Email = entity.Email;
            usr.UserId = 0;
            _unitOfWork.UserRepository.Create(usr);
            _unitOfWork.Save();
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

        public UserViewModel? GetByEmailModel(string email)
        {
            var user = _unitOfWork.UserRepository.GetByEmail(email);
            if (user != null)
            {
                var usr = _mapper.Map<UserViewModel>(user);
                return usr;
            }
            return null;
        }

        public UserViewModel? GetById(int id)
        {
            var user = _unitOfWork.UserRepository.GetByIdNoTracking(id);
            if (user != null)
            {
				var mem = _unitOfWork.MemberRepository.GetById(user.MemberId.Value);
				var usr = _mapper.Map<UserViewModel>(user);
                usr.Email = mem.Email;
                return usr;
            }
            return null;
        }

        public UserViewModel? GetByLogin(string username, string password)
        {
            var user = _unitOfWork.UserRepository.GetByLogin(username, password);
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
				var usrmem = _unitOfWork.MemberRepository.GetById(usr.MemberId.Value);
                if(usrmem == null)
                {
                    usr.Member = new Member()
                    {
                        Email = entity.Email
                    };
                } else
                usrmem.Email = entity.Email;
                usr.Member = usrmem;
			}
            _unitOfWork.UserRepository.Update(usr);
            _unitOfWork.Save();
        }
    }
}
