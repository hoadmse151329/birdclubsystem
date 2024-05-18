using AutoMapper;
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
            if (user != null && user.MemberDetail != null)
            {
                if(user.MemberDetail.Status != "Active")
                {
                    return new AuthenResponse()
                    {
                        UserId = user.MemberId,
                        UserName = request.Username,
                        Status = user.MemberDetail.Status
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
                    Status = user.MemberDetail.Status
                };
            }
            return null;
        }

        public async Task<AuthenResponse> AuthenticateUserEmail(string email)
        {
            var user = await _unitOfWork.UserRepository.GetByEmail(email);
            if (user != null)
            {
                var accessToken = _jwtService.GenerateJWTToken(user.MemberId, user.UserName, user.MemberDetail.Role, _configuration);
                return new AuthenResponse()
                {
                    UserId = user.MemberId,
                    RoleName = user.MemberDetail.Role,
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
			usr.MemberDetail = new Member();
			usr.MemberDetail.MemberId = Guid.NewGuid().ToString();
			usr.MemberDetail.Status = "Inactive";
            usr.MemberDetail.Role = "Member";
			usr.MemberDetail.Email = entity.Email;
			if (newmem != null)
            {
                usr.MemberDetail.FullName = newmem.FullName;
                usr.MemberDetail.UserName = newmem.UserName;
				usr.MemberDetail.Gender = newmem.Gender;
				//usr.MemberDetail.Address = newmem.Address;
                usr.MemberDetail.Phone = newmem.Phone;
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
                    usr.MemberDetail = new Member()
                    {
                        Email = entity.Email
                    };
                } else
                usrmem.Email = entity.Email;

                usr.Role= entity.Role;
                usr.MemberDetail = usrmem;
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
                usr.MemberDetail = usrmem;
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
    }
}
