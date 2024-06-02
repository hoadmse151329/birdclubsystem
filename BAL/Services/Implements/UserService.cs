using AutoMapper;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using BAL.ViewModels.Admin;
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
            if (user != null && user.MemberDetails != null)
            {
                if(user.MemberDetails.Status != "Active")
                {
                    return new AuthenResponse()
                    {
                        UserId = user.MemberId,
                        UserName = request.Username,
                        Status = user.MemberDetails.Status
                    };
                }
                //var role = _unitOfWork.UserRepository
                var accessToken = _jwtService.GenerateJWTToken(user.MemberId, user.UserName, user.Role, _configuration);
                return new AuthenResponse()
                {
                    UserId = user.MemberId,
                    RoleName = user.Role,
                    UserName = user.UserName,
                    AccessToken = accessToken,
                    ImagePath = user.ImagePath,
                    Status = user.MemberDetails.Status
                };
            }
            return null;
        }

        public async Task<AuthenResponse> AuthenticateUserEmail(string email)
        {
            var user = await _unitOfWork.UserRepository.GetByEmail(email);
            if (user != null)
            {
                var accessToken = _jwtService.GenerateJWTToken(user.MemberId, user.UserName, user.MemberDetails.Role, _configuration);
                return new AuthenResponse()
                {
                    UserId = user.MemberId,
                    RoleName = user.MemberDetails.Role,
                    UserName = user.UserName,
                    AccessToken = accessToken,
                    ImagePath = user.ImagePath
                };
            }

            return null;
        }

		public async Task<AuthenResponse> CreateTemporaryNewUser(AuthenRequest request)
		{
			var accessToken = _jwtService.GenerateJWTToken(request.Username, "TempMember", _configuration);
			return new AuthenResponse()
			{
				RoleName = "TempMember",
				UserName = request.Username,
				AccessToken = accessToken
			};
		}

		public void Create(UserViewModel entity, CreateNewMember newmem = null)
        {
            var usr = _mapper.Map<User>(entity);
			usr.MemberDetails = new Member();
			usr.MemberDetails.MemberId = Guid.NewGuid().ToString();
			usr.MemberDetails.Status = "Inactive";
            usr.MemberDetails.Role = "Member";
			usr.MemberDetails.Email = entity.Email;

			if (newmem != null)
            {
                usr.MemberDetails.FullName = newmem.FullName;
                usr.MemberDetails.UserName = newmem.UserName;
				usr.MemberDetails.Gender = newmem.Gender;
                usr.MemberDetails.Phone = newmem.Phone;
			}
            _unitOfWork.UserRepository.Create(usr);
            _unitOfWork.Save();
        }

        public void Create(UserViewModel entity, CreateNewEmployee newmem = null)
        {
            var usr = _mapper.Map<User>(entity);
            usr.MemberDetails = new Member();
            usr.MemberDetails.MemberId = Guid.NewGuid().ToString();
            usr.MemberDetails.Status = "Inactive";
            usr.MemberDetails.Role = newmem.Role;
            usr.MemberDetails.Email = entity.Email;
            if (newmem != null)
            {
                usr.MemberDetails.FullName = newmem.FullName;
                usr.MemberDetails.UserName = newmem.UserName;
                usr.MemberDetails.Gender = newmem.Gender;
                usr.MemberDetails.Phone = newmem.Phone;
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

		public async Task<bool> IsUserExistByEmail(string email)
        {
            var user = await _unitOfWork.UserRepository.GetByEmail(email);
            if (user != null)
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
                    usr.MemberDetails = new Member()
                    {
                        Email = entity.Email
                    };
                } else
                usrmem.Email = entity.Email;

                usr.Role= entity.Role;
                usr.MemberDetails = usrmem;
			}
            _unitOfWork.UserRepository.Update(usr);
            _unitOfWork.Save();
        }

        public void UpdatePassword(UserViewModel entity)
        {
            var usr = _mapper.Map<User>(entity);
            if (usr.MemberId != null)
            {
                var usrmem = _unitOfWork.MemberRepository.GetByIdNoTracking(usr.MemberId).Result;
                usr.Role = entity.Role;
                usr.MemberDetails = usrmem;
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

        public async Task<int> GetIdByUsername(string username)
        {
            return await _unitOfWork.UserRepository.GetIdByUsername(username);
        }

        public async Task<AuthenResponse> CreateGuestUser(AuthenRequest request)
        {
            var accessToken = _jwtService.GenerateJWTToken(request.Username, "Guest", _configuration);
            return new AuthenResponse()
            {
                RoleName = "Guest",
                UserName = request.Username,
                AccessToken = accessToken
            };
        }
    }
}
