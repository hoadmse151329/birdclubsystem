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
        public Task<AuthenResponse> AuthenticateUser(AuthenRequest request);
        public Task<AuthenResponse> AuthenticateUserEmail(string email);
        public Task<UserViewModel?> GetById(int id);
        bool GetByEmail(string email);
        public Task<UserViewModel?> GetByLogin(string username, string password);
        /* void Create(UserViewModel entity);*/
        void Create(UserViewModel entity, CreateNewMember newmem = null);
        /*void Update(UserViewModel entity);*/
        void Update(UserViewModel entity);
        public Task<UserViewModel?> GetByEmailModel(string email);
    }
}
