using BAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IContestService
    {
        Task<ContestViewModel?> GetById(int id);
        Task<IEnumerable<ContestViewModel>> GetAllContests(string? role);
        void Create(ContestViewModel entity);
        void Update(ContestViewModel entity);
        Task<bool> GetBoolContestId(int id);
    }
}
