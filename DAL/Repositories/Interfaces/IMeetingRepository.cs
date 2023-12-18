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
        IEnumerable<Meeting> GetSortedMeetings(
            int meetingId,
            string? meetingName,
            string? description,
            DateTime? registrationDeadline,
            DateTime? startDate,
            DateTime? endDate,
            int numberOfParticipants,
            string? host,
            string? incharge,
            string? note,
            string? image
            );
        Meeting? GetMeetingById(int id);
    }
}
