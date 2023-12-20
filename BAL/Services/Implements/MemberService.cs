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
			throw new NotImplementedException();
		}

		public bool GetByEmail(string email)
		{
			throw new NotImplementedException();
		}

		public MemberViewModel? GetByEmailModel(string email)
		{
			throw new NotImplementedException();
		}

		public MemberViewModel? GetById(int id)
		{
			throw new NotImplementedException();
		}

		public MemberViewModel? GetByLogin(string username, string password)
		{
			throw new NotImplementedException();
		}

		public void Update(MemberViewModel entity)
		{
			throw new NotImplementedException();
		}
	}
}
