﻿using BAL.ViewModels.Authenticates;
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
		Task<MemberViewModel?> GetById(string id);
		Task<bool> GetBoolById(string id);
		bool GetByEmail(string email);
		Task<MemberViewModel?> GetByUserId(int id);
		/* void Create(UserViewModel entity);*/
		void Create(MemberViewModel entity);
		/*void Update(UserViewModel entity);*/
		void Update(MemberViewModel entity);
		Task<MemberViewModel?> GetByEmailModel(string email);
	}
}
