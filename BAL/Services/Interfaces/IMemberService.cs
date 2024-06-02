using BAL.ViewModels.Authenticates;
using BAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAL.ViewModels.Manager;
using BAL.ViewModels.Admin;

namespace BAL.Services.Interfaces
{
    public interface IMemberService
    {
        /*Task<IEnumerable<GetMembershipExpire?>> GetAllMemberStatusWithExpireByRole(string role);*/
        Task<IEnumerable<GetMemberStatus>?> GetSortedMembers(
            string? memberId = null,
            string? memberUserName = null,
            string? memberFullName = null,
            DateTime? expiryDateTime = null,
            List<string>? roles = null,
            List<string>? statuses = null,
            string? orderBy = null,
            bool isManager = false,
            bool isAdmin = false
            );
        Task<IEnumerable<GetEmployeeStatus>?> GetSortedEmployees(
            string? memberId = null,
            string? memberUserName = null,
            string? memberFullName = null,
            DateTime? expiryDateTime = null,
            List<string>? roles = null,
            List<string>? statuses = null,
            string? orderBy = null,
            bool isManager = false,
            bool isAdmin = false
            );
        Task<IEnumerable<GetEmployeeStatus>?> GetAvailableStaffList(
            DateTime? startAvailableDate = null,
            DateTime? endAvailableDate = null
            );
        Task<MemberViewModel?> GetById(string id);
		Task<bool> GetBoolById(string id);
		bool GetByEmail(string email);
		Task<MemberViewModel?> GetByUserId(int id);
		/* void Create(UserViewModel entity);*/
		void Create(MemberViewModel entity);
		/*void Update(UserViewModel entity);*/
		void Update(MemberViewModel entity);
        void UpdateMemberStatus(GetMembershipExpire entity);
        Task<bool> UpdateAllMemberStatus (List<GetMemberStatus> listMem);
        Task<bool> UpdateAllEmployeeStatus(List<GetEmployeeStatus> listMem);
        Task<MemberViewModel?> GetByEmailModel(string email);
	}
}
