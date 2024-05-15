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
        Task<Contest?> GetContestById(int id);
        Task<Contest?> GetContestByIdWithoutInclude(int id);
        Task<Contest?> GetContestByIdTracking(int id);
        Task<bool> GetBoolContestId(int id);
    }
}
