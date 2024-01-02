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
        Task<bool> GetBoolMeetingParticipantById(int meetingId, int memberId);
        Task<int> GetParticipationNoMeetingParticipantById(int meetingId, int memberId);
        Task<MeetingParticipant> GetMeetingParticipantById(int meetingId, int memberId);
        Task<IEnumerable<MeetingParticipant>> GetMeetingParticipantsByMemberId(int memId);
        Task<int> GetCountMeetingParticipantsByMemberId(int memId);
    }
}
