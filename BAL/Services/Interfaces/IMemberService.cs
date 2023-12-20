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
		MemberViewModel? GetById(int id);
		bool GetByEmail(string email);
		MemberViewModel? GetByLogin(string username, string password);
		/* void Create(UserViewModel entity);*/
		void Create(MemberViewModel entity);
		/*void Update(UserViewModel entity);*/
		void Update(MemberViewModel entity);
		MemberViewModel? GetByEmailModel(string email);
	}
}
