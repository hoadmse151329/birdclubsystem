using DAL.Infrastructure;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IMeetingParticipantRepository : IRepositoryBase<MeetingParticipant>
    {
        Task<IEnumerable<MeetingParticipant>> GetMeetingParticipantsByMeetId(int meetingId);
        Task<int> GetCountMeetingParticipantsByMeetId(int meetingId);
        Task<bool> GetBoolMeetingParticipantById(int meetingId, string memberId);
        Task<int> GetParticipationNoMeetingParticipantById(int meetingId, string memberId);
        Task<MeetingParticipant> GetMeetingParticipantById(int meetingId, string memberId);
        Task<MeetingParticipant> GetMeetingParticipantByIdTracking(int meetingId, string memberId);
        Task<IEnumerable<MeetingParticipant>> GetMeetingParticipantsByMemberId(string memId);
        Task<IEnumerable<MeetingParticipant>> GetMeetingParticipantsByMemberIdInclude(string memId);
        Task<int> GetCountMeetingParticipantsByMemberId(string memId);
        Task<IEnumerable<MeetingParticipant>> UpdateAllMeetingParticipantStatus(List<MeetingParticipant> part);
    }
}
