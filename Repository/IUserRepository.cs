using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User? GetByLogin(string userName, string password);
        User? GetById(int id);
        User? GetByEmail(string email);
    }
}
