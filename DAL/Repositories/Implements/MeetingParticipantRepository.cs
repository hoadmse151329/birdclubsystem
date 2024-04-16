using DAL.Infrastructure;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> GetBoolMeetingParticipantById(int meetingId, string memberId)
        {
            var mempart = _context.MeetingParticipants.FirstOrDefault(m => m.MeetingId == meetingId && m.MemberId == memberId);
            if (mempart != null) return true;
            return false;
        }

        public async Task<int> GetCountMeetingParticipantsByMeetId(int meetingId)
        {
            return _context.MeetingParticipants.Count(m => m.MeetingId == meetingId);
        }

        public async Task<int> GetCountMeetingParticipantsByMemberId(string memId)
        {
            return _context.MeetingParticipants.Count(m => m.MemberId == memId);
        }

        public async Task<MeetingParticipant> GetMeetingParticipantById(int meetingId, string memberId)
        {
            return _context.MeetingParticipants.FirstOrDefault(m => m.MeetingId == meetingId && m.MemberId == memberId);
        }

        public async Task<IEnumerable<MeetingParticipant>> GetMeetingParticipantsByMeetId(int meetingId)
        {
            return _context.MeetingParticipants
                .Where(m => m.MeetingId == meetingId).Include(m => m.Member)
                /*.Select(x => new MeetingParticipant
                {
                    MeetingId= x.MeetingId,
                    MemberId= x.MemberId,
                    ParticipantNo = x.ParticipantNo,
                    Member = x.Member
                })*/
                .ToList();
        }

        public async Task<IEnumerable<MeetingParticipant>> GetMeetingParticipantsByMemberId(string memId)
        {
            return _context.MeetingParticipants.Where(m => m.MemberId == memId).ToList();
        }

        public async Task<IEnumerable<MeetingParticipant>> GetMeetingParticipantsByMemberIdInclude(string memId)
        {
            return _context.MeetingParticipants.Where(m => m.MemberId == memId).Include(m => m.Meeting).ToList();
        }

        public async Task<int> GetParticipationNoMeetingParticipantById(int meetingId, string memberId)
        {
            var mempart = _context.MeetingParticipants.SingleOrDefault(m => m.MeetingId.Equals(meetingId) && m.MemberId.Equals(memberId));
            if (mempart != null) return Int32.Parse(mempart.ParticipantNo);
            return 0;
        }
    }
}
