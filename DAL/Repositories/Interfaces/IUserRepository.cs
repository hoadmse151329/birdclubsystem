using DAL.Infrastructure;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        User? GetByLogin(string userName, string passWord);
        User? GetByIdNoTracking(int id);
        User? GetByEmail(string email);

    }
}
