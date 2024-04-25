using BAL.ViewModels;
using BAL.ViewModels.Event;
using BAL.ViewModels.Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IMeetingParticipantService
    {
        Task<IEnumerable<MeetingParticipantViewModel>> GetAll();
        Task<IEnumerable<MeetingParticipantViewModel>> GetAllByMemberId(string memberId);
        Task<IEnumerable<MeetingParticipantViewModel>> GetAllByMeetingId(int meetId);
        Task<IEnumerable<GetEventParticipation>> GetAllByMemberIdInclude(string memberId);
        Task<int> Create(string memId, int metId);
        Task<int> GetCurrentParticipantAmounts(int metId);
        Task<int> GetParticipationNo(string memId, int metId);
        Task<bool> Delete(string memId, int metId);
        Task<MeetingParticipantViewModel?> GetById(string memId, int metId);
        void Update(MeetingParticipantViewModel entity);
        Task<bool> UpdateAllMeetingParticipantStatus(List<MeetingParticipantViewModel> listPart);
    }
}
