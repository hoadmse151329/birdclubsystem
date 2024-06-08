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
            var mempart = await _context.MeetingParticipants.AsNoTracking().FirstOrDefaultAsync(m => m.MeetingId == meetingId && m.MemberId == memberId);
            if (mempart != null) return true;
            return false;
        }

        public async Task<int> GetCountMeetingParticipantsByMeetId(int meetingId)
        {
            return await _context.MeetingParticipants.AsNoTracking().CountAsync(m => m.MeetingId == meetingId);
        }

        public async Task<int> GetCountMeetingParticipantsByMemberId(string memId)
        {
            return await _context.MeetingParticipants.AsNoTracking().CountAsync(m => m.MemberId == memId);
        }

        public async Task<MeetingParticipant> GetMeetingParticipantById(int meetingId, string memberId)
        {
            return await _context.MeetingParticipants.AsNoTracking()
                .Where(m => m.MeetingId == meetingId && m.MemberId == memberId)
                .Include(m => m.MemberDetails)
                .Include(m => m.MeetingDetails)
                .FirstOrDefaultAsync();
        }
        public async Task<MeetingParticipant> GetMeetingParticipantByIdTracking(int meetingId, string memberId)
        {
            return await _context.MeetingParticipants
                .Where(m => m.MeetingId == meetingId && m.MemberId == memberId)
                .Include(m => m.MemberDetails)
                .Include(m => m.MeetingDetails)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<MeetingParticipant>> GetMeetingParticipantsByMeetId(int meetingId)
        {
            return await _context.MeetingParticipants
                .AsNoTracking()
                .Where(m => m.MeetingId == meetingId)
                .Include(m => m.MemberDetails)
                .OrderBy(p => p.ParticipantNo)
                .ToListAsync();
        }

        public async Task<IEnumerable<MeetingParticipant>> GetMeetingParticipantsByMemberId(string memId)
        {
            return await _context.MeetingParticipants.AsNoTracking().Where(m => m.MemberId == memId).ToListAsync();
        }

        public async Task<IEnumerable<MeetingParticipant>> GetMeetingParticipantsByMemberIdInclude(string memId)
        {
            return await _context.MeetingParticipants.AsNoTracking().Where(m => m.MemberId == memId).Include(m => m.MeetingDetails).ToListAsync();
        }

        public async Task<int> GetParticipationNoMeetingParticipantById(int meetingId, string memberId)
        {
            var mempart = await _context.MeetingParticipants.AsNoTracking().SingleOrDefaultAsync(m => m.MeetingId.Equals(meetingId) && m.MemberId.Equals(memberId));
            if (mempart != null) return mempart.ParticipantNo.Value;
            return 0;
        }

        public async Task<IEnumerable<MeetingParticipant>> UpdateAllMeetingParticipantStatus(List<MeetingParticipant> part)
        {
            foreach (var participant in part)
            {
                var meetpart = await _context.MeetingParticipants
                    .SingleOrDefaultAsync(p => p.MeetingId == participant.MeetingId && p.MemberId == participant.MemberId);
                if (meetpart != null)
                {
                    if (meetpart.CheckInStatus != participant.CheckInStatus)
                    {
                        meetpart.CheckInStatus = participant.CheckInStatus;
                        _context.Update(meetpart);
                    }
                }
            }
            return part;
        }
    }
}