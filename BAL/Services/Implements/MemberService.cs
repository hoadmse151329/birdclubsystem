using AutoMapper;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using DAL.Infrastructure;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Implements
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MemberService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

		public void Create(MemberViewModel entity)
		{
			var mem = _mapper.Map<Member>(entity);
			mem.Status = "Active";
			mem.Role = "Member";
			_unitOfWork.MemberRepository.Create(mem);
			_unitOfWork.Save();
		}

        public async Task<bool> GetBoolById(string id)
        {
            var mem = await _unitOfWork.MemberRepository.GetByIdNoTracking(id);
            /*if (mem != null)
			{
				var mem = _unitOfWork.MemberRepository.GetById(user.MemberId.Value);
				var usr = _mapper.Map<UserViewModel>(user);
				usr.Email = mem.Email;
				return usr;
			}*/
            if (mem != null)
            {
                return true;
            }
            return false;
        }

        public bool GetByEmail(string email)
		{
			throw new NotImplementedException();
		}

		public async Task<MemberViewModel?> GetByEmailModel(string email)
		{
			throw new NotImplementedException();
		}

		public async Task<MemberViewModel?> GetById(string id)
		{
			var mem = await _unitOfWork.MemberRepository.GetByIdNoTracking(id);
			/*if (mem != null)
			{
				var mem = _unitOfWork.MemberRepository.GetById(user.MemberId.Value);
				var usr = _mapper.Map<UserViewModel>(user);
				usr.Email = mem.Email;
				return usr;
			}*/
			if(mem != null)
			{
				var memb = _mapper.Map<MemberViewModel>(mem);
				return memb;
			}
			return null;
		}

		public Task<MemberViewModel?> GetByUserId()
		{
			throw new NotImplementedException();
		}

		public void Update(MemberViewModel entity)
		{
			var mem = _mapper.Map<Member>(entity);
			_unitOfWork.MemberRepository.Update(mem);
			_unitOfWork.Save();
		}
	}
}
