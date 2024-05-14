using BAL.ViewModels.Event;
using BAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace BAL.Services.Interfaces
{
    public interface IContestParticipantService
    {
        Task<int> Create(int contestId, string memberId, int birdId);
        Task<bool> Delete(int contestId, string memberId);
        Task<IEnumerable<ContestParticipantViewModel>> GetAll();
        Task<IEnumerable<ContestParticipantViewModel>> GetAllByBirdId(int birdId);
        Task<IEnumerable<GetEventParticipation>> GetAllByBirdIdInclude(int birdId);
        Task<IEnumerable<ContestParticipantViewModel>> GetAllByContestId(int contestId);
        Task<IEnumerable<ContestParticipantViewModel>> GetAllByMemberId(string memberId);
        Task<IEnumerable<GetEventParticipation>> GetAllByMemberIdInclude(string memberId);
        Task<int> GetCurrentParticipantAmounts(int contestId);
        Task<int> GetParticipationNo(int contestId, string memberId, int? birdId = null);
        Task<bool> UpdateAllContestParticipantStatus(List<ContestParticipantViewModel> listPart);
        Task<bool> UpdateAllContestParticipantScore(List<ContestParticipantViewModel> listPart, bool isContestEnded = false);
    }
}
