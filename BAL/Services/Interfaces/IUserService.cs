using BAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IUserService
    {
        UserViewModel GetById(int id);
        UserViewModel GetByIdNoTracking(int id);
        bool GetByEmail(string email);
    }
}
