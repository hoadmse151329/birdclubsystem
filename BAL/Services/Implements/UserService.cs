using AutoMapper;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using DAL.Infrastructure;
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
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
        public UserViewModel GetById(int id)
        {
            var user = _unitOfWork.UserRepository.GetById(id);
            if (user != null)
            {
                var usr = _mapper.Map<UserViewModel>(user);
                return usr;
            }
            throw new Exception("No user with that id!");
        }
        public UserViewModel GetByIdNoTracking(int id)
        {
            var user = _unitOfWork.UserRepository.GetByIdNoTracking(id);
            if (user != null)
            {
                var usr = _mapper.Map<UserViewModel>(user);
                return usr;
            }
            throw new Exception("No user with that id!");
        }
    }
}
