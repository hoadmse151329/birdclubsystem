using BAL.ViewModels.Authenticates;
using BAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IMemberService
    {
		public Task<MemberViewModel?> GetById(int id);
		bool GetByEmail(string email);
		public Task<MemberViewModel?> GetByUserId();
		/* void Create(UserViewModel entity);*/
		void Create(MemberViewModel entity);
		/*void Update(UserViewModel entity);*/
		void Update(MemberViewModel entity);
		public Task<MemberViewModel?> GetByEmailModel(string email);
	}
}
