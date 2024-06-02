using DAL.Infrastructure;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IMeetingAssignmentRepository : IRepositoryBase<MeetingAssignment>
    {
        Task<MeetingAssignment> GetMeetingAssignmentByIdNoTracking(int meetingId, int memberId);
        Task<IEnumerable<MeetingAssignment>> GetMeetingAssignmentsByMeetingIdNoTracking(int meetingId);
        Task<IEnumerable<MeetingAssignment>> GetSortedMeetingAssignmentsNoTracking(
            int? meetingId = null,
            int? memberId = null,
            DateTime? assignedDate = null,
            string? role = null,
            string? orderBy = null
            );
        Task<IEnumerable<MeetingAssignment>> GetMeetingAssignmentsByMemberIdNoTracking(int memberId);
    }
}
