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
        Task<MeetingAssignment> GetMeetingAssignmentByIdNoTracking(int meetingId, string memberId);
        Task<IEnumerable<MeetingAssignment>> GetMeetingAssignmentsByMeetingIdNoTracking(int meetingId);
        Task<bool> IsMeetingAssignmentExistByIdNoTracking(int meetingId, string memberId);
        Task<IEnumerable<MeetingAssignment>> GetSortedMeetingAssignmentsNoTracking(
            int? meetingId = null,
            string? memberId = null,
            DateTime? assignedDate = null,
            List<string>? roles = null,
            string? orderBy = null
            );
        Task<IEnumerable<MeetingAssignment>> GetMeetingAssignmentsByMemberIdNoTracking(string memberId);
    }
}
