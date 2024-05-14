using DAL.Infrastructure;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IContestParticipantRepository : IRepositoryBase<ContestParticipant>
    {
        Task<IEnumerable<ContestParticipant>> GetContestParticipantsByContestId(int contestId);
		Task<IEnumerable<ContestParticipant>> GetContestParticipantsByBirdId(int birdId);
		Task<IEnumerable<ContestParticipant>> GetContestParticipantsByBirdIdInclude(int birdId);
		Task<IEnumerable<ContestParticipant>> GetContestParticipantsByMemberId(string memberId);
		Task<IEnumerable<ContestParticipant>> GetContestParticipantsByMemberIdInclude(string memberId);
		Task<int> GetCountContestParticipantsByContestId(int contestId);
		Task<int> GetCountContestParticipantsByBirdId(int birdId);
		Task<int> GetCountContestParticipantsByMemberId(string memberId);
		Task<bool> GetBoolContestParticipantById(int contestId, string memberId, int? birdId = null);
        Task<int> GetParticipationNoContestParticipantById(int contestId, string memberId, int? birdId = null);
        Task<ContestParticipant> GetContestParticipantById(int contestId, string memberId, int? birdId = null);
		Task<IEnumerable<ContestParticipant>> UpdateAllContestParticipantStatus(List<ContestParticipant> part);
        Task<IEnumerable<ContestParticipant>> UpdateAllContestParticipantScore(List<ContestParticipant> part, bool isContestEnded = false);
    }
}
