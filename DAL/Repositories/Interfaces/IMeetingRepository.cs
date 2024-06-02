using DAL.Infrastructure;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IMeetingRepository : IRepositoryBase<Meeting>
    {
        IEnumerable<Meeting> GetAllByRegistrationDeadline(DateTime registrationDeadline);
        IEnumerable<Meeting> GetSortedMeetings(
            int? meetingId,
            string? meetingName,
            DateTime? openRegistration,
            DateTime? registrationDeadline,
            DateTime? startDate,
            DateTime? endDate,
            int? numberOfParticipants,
            List<string>? roads,
            List<string>? districts,
            List<string>? cities,
            List<string>? statuses,
            string? orderBy,
            bool isMemberOrGuest = false
            );
        IEnumerable<string> GetAllMeetingName();
        Task<IEnumerable<Meeting>> GetAllMeetings(string? role);
        Task<Meeting?> GetMeetingById(int id);
        Task<bool> GetBoolMeetingId(int id);
        Task<int> CountMeeting();
        Task<int> CountMeetingByStatus(string status);
    }
}
