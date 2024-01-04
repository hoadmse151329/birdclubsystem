﻿using BAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IMeetingService
    {
        public Task<MeetingViewModel?> GetById(int id);
        Task<IEnumerable<MeetingViewModel>> GetAll();
        IEnumerable<MeetingViewModel> GetAllByRegistrationDeadline(DateTime registrationDeadline);
        IEnumerable<MeetingViewModel> GetSortedMeetings(
            int meetingId,
            string? meetingName,
            DateTime? registrationDeadline,
            DateTime? startDate,
            DateTime? endDate,
            int? numberOfParticipants,
            string? orderBy
            );
        List<string> GetAllMeetingName();
        void Create(MeetingViewModel entity);
        void Update(MeetingViewModel entity);
    }
}
