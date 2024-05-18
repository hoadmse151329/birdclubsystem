using BAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IMeetingService
    {
        Task<MeetingViewModel?> GetById(int id);
        Task<IEnumerable<MeetingViewModel>> GetAllMeetings(string? role);
        IEnumerable<MeetingViewModel> GetAllByRegistrationDeadline(DateTime registrationDeadline);
        Task<IEnumerable<MeetingViewModel>?> GetSortedMeetings(
            int? meetingId,
            string? meetingName,
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
        List<string> GetAllMeetingName();
        void Create(MeetingViewModel entity);
        void Update(MeetingViewModel entity);
        Task<bool> GetBoolMeetingId(int id);
    }
}
