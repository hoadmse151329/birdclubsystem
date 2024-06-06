using BAL.ViewModels;
using BAL.ViewModels.Manager;
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
        Task<MeetingViewModel?> GetByIdCheckIncharge(int id, string? accToken);
        Task<IEnumerable<MeetingViewModel>> GetAllMeetings(string? role);
        Task<IEnumerable<MeetingViewModel>?> GetSortedMeetings(
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
        List<string> GetAllMeetingName();
        void Create(MeetingViewModel entity);
        void Create(CreateNewMeetingVM entity);
        void Update(MeetingViewModel entity);
        void Update(UpdateMeetingDetailsVM entity);
        Task<bool> UpdateStatus(UpdateMeetingStatusVM entity);
        Task<bool> GetBoolMeetingId(int id);
        Task<int> CountMeeting();
    }
}
