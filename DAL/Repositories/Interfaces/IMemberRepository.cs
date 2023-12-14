using DAL.Infrastructure;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IMemberRepository : IRepositoryBase<Member>
    {
        Member? GetByIdNoTracking(int id);
        Member? GetByEmail(string email);
    }
}
