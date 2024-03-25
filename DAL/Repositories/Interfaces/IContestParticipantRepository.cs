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
        Task<int> GetCountContestParticipantsByContestId(int contestId);
        Task<bool> GetBoolContestParticipantById(int contestId, int birdId);
        Task<int> GetParticipationNoContestParticipantById(int contestId, int birdId);
        Task<ContestParticipant> GetContestParticipantById(int contestId, int birdId);
        Task<IEnumerable<ContestParticipant>> GetContestParticipantsByBirdId(int birdId);
        Task<IEnumerable<ContestParticipant>> GetContestParticipantsByBirdIdInclude(int birdId);
        Task<int> GetCountContestParticipantsByBirdId(int birdId);
    }
}
