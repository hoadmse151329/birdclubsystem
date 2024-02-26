﻿using DAL.Infrastructure;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implements
{
    public class MeetingRepository : RepositoryBase<Meeting>, IMeetingRepository
    {
        private readonly BirdClubContext _context;
        public MeetingRepository(BirdClubContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Meeting> GetAllByRegistrationDeadline(DateTime registrationDeadline)
        {
            return _context.Meetings.AsNoTracking().Where(m => m.RegistrationDeadline == registrationDeadline).ToList();
        }

        public IEnumerable<string> GetAllMeetingName()
        {
            return _context.Meetings.AsNoTracking()
                .Select(m => m.MeetingName)
                .Distinct()
                .ToList();
        }

        public IEnumerable<Meeting> GetSortedMeetings(
            int? meetingId = null,
            string? meetingName = null,
            DateTime? registrationDeadline = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            int? numberOfParticipants = null,
            string? orderBy = null
            )
        {
            var meetings = _context.Meetings.AsNoTracking();
            if (meetingId != null)
            {
                meetings = meetings.AsNoTracking().Where(m => m.MeetingId.Equals(meetingId));
            }

            if (meetingName != null)
            {
                meetings = meetings.AsNoTracking().Where(m => m.MeetingName.Contains(meetingName));
            }

            if (registrationDeadline != null)
            {
                meetings = meetings.AsNoTracking().Where(m => m.RegistrationDeadline == registrationDeadline);
            }
            if (startDate != null)
            {
                meetings = meetings.AsNoTracking().Where(m => m.StartDate == startDate);
            }
            if (endDate != null)
            {
                meetings = meetings.AsNoTracking().Where(m => m.EndDate == endDate);
            }
            if (numberOfParticipants != null)
            {
                meetings = meetings.AsNoTracking().Where(m => m.NumberOfParticipants == numberOfParticipants);
            }

            if (!string.IsNullOrEmpty(orderBy))
            {
                switch (orderBy)
                {
                    case "meetingname_asc":
                        meetings = meetings.OrderBy(m => m.MeetingName);
                        break;
                    case "meetingname_desc":
                        meetings = meetings.OrderByDescending(m => m.MeetingName);
                        break;
                    case "registrationdeadline_asc":
                        meetings = meetings.OrderBy(m => m.RegistrationDeadline);
                        break;
                    case "registrationdeadline_desc":
                        meetings = meetings.OrderByDescending(m => m.RegistrationDeadline);
                        break;
                    case "startdate_asc":
                        meetings = meetings.OrderBy(m => m.StartDate);
                        break;
                    case "startdate_desc":
                        meetings = meetings.OrderByDescending(m => m.StartDate);
                        break;
                    case "enddate_asc":
                        meetings = meetings.OrderBy(m => m.EndDate);
                        break;
                    case "enddate_desc":
                        meetings = meetings.OrderByDescending(m => m.EndDate);
                        break;
                }
            }
            else
            {
                meetings = meetings.OrderBy(m => m.MeetingId);
            }
            return meetings.ToList();
        }

        public async Task<IEnumerable<Meeting>> GetMeetings()
        {
            return _context.Meetings.AsNoTracking().ToList();
        }

        public async Task<Meeting?> GetMeetingById(int id)
        {
            return _context.Meetings.AsNoTracking().SingleOrDefault(meet => meet.MeetingId == id);
        }
    }
}
