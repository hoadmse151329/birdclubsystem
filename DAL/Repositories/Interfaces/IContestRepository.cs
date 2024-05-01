using DAL.Infrastructure;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IContestRepository : IRepositoryBase<Contest>
    {
        Task<IEnumerable<Contest>> GetAllContests(string? role);
        public Task<Contest?> GetContestById(int id);
    }
}
