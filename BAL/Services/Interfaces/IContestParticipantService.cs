using BAL.ViewModels.Event;
using BAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IContestParticipantService
    {
        Task<IEnumerable<ContestParticipantViewModel>> GetAll();
        Task<IEnumerable<ContestParticipantViewModel>> GetAllByBirdId(int birdId);
        Task<IEnumerable<ContestParticipantViewModel>> GetAllByContestId(int contestId);
        Task<IEnumerable<GetEventParticipation>> GetAllByBirdIdInclude(int birdId);
        Task<int> Create(int birdId, int contestId);
        Task<int> GetCurrentParticipantAmounts(int contestId);
        Task<int> GetParticipationNo(int birdId, int contestId);
        Task<bool> Delete(int birdId, int contestId);
    }
}
