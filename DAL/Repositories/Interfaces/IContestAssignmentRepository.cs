using DAL.Infrastructure;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IContestAssignmentRepository : IRepositoryBase<ContestAssignment>
    {
        Task<ContestAssignment> GetContestAssignmentByIdNoTracking(int contestId, string memberId);
        Task<bool> IsContestAssignmentExistByIdNoTracking(int contestId, string memberId);
        Task<IEnumerable<ContestAssignment>> GetContestAssignmentsByContestIdNoTracking(int contestId);
        Task<IEnumerable<ContestAssignment>> GetSortedContestAssignmentsNoTracking(
            int? contestId = null,
            string? memberId = null,
            DateTime? assignedDate = null,
            List<string>? role = null,
            string? orderBy = null
            );
        Task<IEnumerable<ContestAssignment>> GetContestAssignmentsByMemberIdNoTracking(string memberId);
    }
}
