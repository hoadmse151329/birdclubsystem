using BAL.ViewModels;
using BAL.ViewModels.Authenticates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IUserService
    {
        AuthenResponse AuthenticateUser(AuthenRequest request);
        AuthenResponse AuthenticateUserEmail(string email);
        UserViewModel? GetById(int id);
        bool GetByEmail(string email);
        UserViewModel? GetByLogin(string username, string password);
        /* void Create(UserViewModel entity);*/
        void Create(UserViewModel entity);
        /*void Update(UserViewModel entity);*/
        void Update(UserViewModel entity);
        UserViewModel? GetByEmailModel(string email);
    }
}
