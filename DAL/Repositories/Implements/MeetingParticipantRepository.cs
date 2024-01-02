using DAL.Infrastructure;
using DAL.Models;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implements
{
    public class MeetingParticipantRepository : RepositoryBase<MeetingParticipant>, IMeetingParticipantRepository
    {
        private readonly BirdClubContext _context;
        public MeetingParticipantRepository(BirdClubContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> GetBoolMeetingParticipantById(int meetingId, int memberId)
        {
            var mempart = _context.MeetingParticipants.Find(meetingId, memberId);
            if (mempart != null) return true;
            return false;
        }

        public async Task<int> GetCountMeetingParticipantsByMeetId(int meetingId)
        {
            return _context.MeetingParticipants.Count(m => m.MeetingId == meetingId);
        }

        public async Task<int> GetCountMeetingParticipantsByMemberId(int memId)
        {
            return _context.MeetingParticipants.Count(m => m.MemberId == memId);
        }

        public async Task<MeetingParticipant> GetMeetingParticipantById(int meetingId, int memberId)
        {
            return _context.MeetingParticipants.Find(meetingId, memberId);
        }

        public async Task<IEnumerable<MeetingParticipant>> GetMeetingParticipantsByMeetId(int meetingId)
        {
            return _context.MeetingParticipants.Where(m => m.MeetingId == meetingId).ToList();
        }

        public async Task<IEnumerable<MeetingParticipant>> GetMeetingParticipantsByMemberId(int memId)
        {
            return _context.MeetingParticipants.Where(m => m.MemberId == memId).ToList();
        }

        public async Task<int> GetParticipationNoMeetingParticipantById(int meetingId, int memberId)
        {
            var mempart = _context.MeetingParticipants.Find(meetingId, memberId);
            if (mempart != null) return Int32.Parse(mempart.ParticipantNo);
            return 0;
        }
    }
}
