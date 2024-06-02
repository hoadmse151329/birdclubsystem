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
    public class MeetingAssignmentRepository : RepositoryBase<MeetingAssignment>, IMeetingAssignmentRepository
    {
        private readonly BirdClubContext _context;
        public MeetingAssignmentRepository(BirdClubContext context) : base(context)
        {
            _context = context;
        }

        public Task<MeetingAssignment> GetMeetingAssignmentByIdNoTracking(int meetingId, int memberId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MeetingAssignment>> GetMeetingAssignmentsByMeetingIdNoTracking(int meetingId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MeetingAssignment>> GetMeetingAssignmentsByMemberIdNoTracking(int memberId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MeetingAssignment>> GetSortedMeetingAssignmentsNoTracking(
            int? meetingId, 
            int? memberId, 
            DateTime? assignedDate, 
            string? role, 
            string? orderBy = null
            )
        {
            throw new NotImplementedException();
        }
    }
}
