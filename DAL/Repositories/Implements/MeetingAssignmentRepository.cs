using DAL.Infrastructure;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implements
{
    public class MeetingAssignmentRepository : RepositoryBase<MeetingAssignment>, IMeetingAssignmentRepository
    {
        private readonly BirdClubContext _context;
        public MeetingAssignmentRepository(BirdClubContext context) : base(context)
        {
            _context = context;
        }

        public async Task<MeetingAssignment> GetMeetingAssignmentByIdNoTracking(int meetingId, string memberId)
        {
            return await _context.MeetingAssignments.AsNoTracking().SingleOrDefaultAsync(ma => ma.MeetingId.Equals(meetingId) && ma.MemberId.Equals(memberId));
        }

        public async Task<IEnumerable<MeetingAssignment>> GetMeetingAssignmentsByMeetingIdNoTracking(int meetingId)
        {
            return await _context.MeetingAssignments.AsNoTracking().Where(ma => ma.MeetingId.Equals(meetingId)).ToListAsync();
        }

        public async Task<IEnumerable<MeetingAssignment>> GetMeetingAssignmentsByMemberIdNoTracking(string memberId)
        {
            return await _context.MeetingAssignments.AsNoTracking().Where(ma => ma.MemberId.Equals(memberId)).ToListAsync();
        }

        public async Task<IEnumerable<MeetingAssignment>> GetSortedMeetingAssignmentsNoTracking(
            int? meetingId = null, 
            string? memberId = null, 
            DateTime? assignedDate = null, 
            List<string>? roles = null, 
            string? orderBy = null
            )
        {
            var meeAsss = _context.MeetingAssignments.AsNoTracking().AsQueryable();
            List<string> roleListDefault = new List<string> {
                "Organizer",
                "Recorder",
                "Facilitator"
            };

            if (meetingId.HasValue && meetingId.Value > 0)
            {
                meeAsss = meeAsss.Where(m => m.MeetingId.Equals(meetingId.Value));
            }

            if (!string.IsNullOrEmpty(memberId) && !string.IsNullOrWhiteSpace(memberId))
            {
                meeAsss = meeAsss.Where(m => m.MemberId.Contains(memberId));
            }

            if (roles != null && roles.Any())
            {
                meeAsss = meeAsss.Where(m => roles.Contains(m.Role));
            }

            if (assignedDate.HasValue)
            {
                meeAsss = meeAsss.Where(m => m.AssignedDate.Value.Date.Equals(assignedDate.Value.Date));
            }
            meeAsss = orderBy switch
            {
                "meetingId_asc" => meeAsss.OrderBy(m => m.MeetingId),
                "meetingId_desc" => meeAsss.OrderByDescending(m => m.MeetingId),
                "memberId_asc" => meeAsss.OrderBy(m => m.MemberId),
                "memberId_desc" => meeAsss.OrderByDescending(m => m.MemberId),
                "assigneddate_asc" => meeAsss.OrderBy(m => m.AssignedDate.Value.Date),
                "assigneddate_desc" => meeAsss.OrderByDescending(m => m.AssignedDate.Value.Date),
                "role_asc" => meeAsss.OrderBy(m => roleListDefault.IndexOf(m.Role)),
                "role_desc" => meeAsss.OrderByDescending(m => roleListDefault.IndexOf(m.Role)),
                _ => meeAsss.OrderBy(m => m.MeetingId)
            };

            return await meeAsss.AsNoTracking().ToListAsync();
        }

        public async Task<bool> IsMeetingAssignmentExistByIdNoTracking(int meetingId, string memberId)
        {
            var isOccupied = await _context.MeetingAssignments.AsNoTracking().AnyAsync(ma => ma.MeetingId.Equals(meetingId) && ma.MemberId.Equals(memberId));
            return isOccupied;
        }
    }
}
