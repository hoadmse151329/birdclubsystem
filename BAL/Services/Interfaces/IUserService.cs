using BAL.ViewModels;
using BAL.ViewModels.Authenticates;
using BAL.ViewModels.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IUserService
    {
        Task<AuthenResponse> AuthenticateUser(AuthenRequest request);
		Task<AuthenResponse> CreateTemporaryNewUser(AuthenRequest request);
		Task<AuthenResponse> AuthenticateUserEmail(string email);
        Task<UserViewModel?> GetById(int id);
        Task<UserViewModel?> GetByMemberId(string memId);
        Task<bool> GetBoolById(int id);
        bool GetByEmail(string email);
        Task<bool> UpdateUserAvatar(string memId, string imagePath);
        Task<UserViewModel?> GetByLogin(string username, string password);
        /* void Create(UserViewModel entity);*/
        void Create(UserViewModel entity, CreateNewMember newmem = null);
        /*void Update(UserViewModel entity);*/
        void Update(UserViewModel entity);
        void UpdatePassword(UserViewModel entity);
        Task<UserViewModel?> GetByEmailModel(string email);
        Task<int> GetIdByUsername(string username);
    }
}
