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
        IEnumerable<Contest> GetSortedContests(
            int? contestId,
            string? contestName,
            DateTime? openRegistration,
            DateTime? registrationDeadline,
            DateTime? startDate,
            DateTime? endDate,
            int? numberOfParticipants,
            int? reqMinElo,
            int? reqMaxElo,
            List<string>? roads,
            List<string>? districts,
            List<string>? cities,
            List<string>? statuses,
            string? orderBy,
            bool isMemberOrGuest = false
            );
        Task<Contest?> GetContestById(int id);
        Task<Contest?> GetContestByIdWithoutInclude(int id);
        Task<Contest?> GetContestByIdTracking(int id);
        Task<bool> GetBoolContestId(int id);
        Task<int> CountContest();
        Task<int> CountContestByStatus(string status);
    }
}