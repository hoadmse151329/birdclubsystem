﻿using DAL.Infrastructure;
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
            int meetingId,
            string? meetingName,
            DateTime? registrationDeadline,
            DateTime? startDate,
            DateTime? endDate,
            int? numberOfParticipants,
            string? orderBy
            );
        IEnumerable<string> GetAllMeetingName();
        Task<IEnumerable<Meeting>> GetMeetings();
    }
}
