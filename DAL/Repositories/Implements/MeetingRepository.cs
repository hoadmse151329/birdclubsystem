using DAL.Infrastructure;
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

        public Meeting? GetMeetingById(int id)
        {
            var meetimage = _context.MeetingMedia
                .SingleOrDefault(m => m.MeetingId.Equals(id));
            var meet = _context.Meetings
                .Where(m => m.MeetingId.Equals(meetimage.MeetingId))
                .Select(m => new Meeting
                {
                    MeetingId = m.MeetingId,
                    MeetingName = m.MeetingName,
                    Description = m.Description,
                    RegistrationDeadline = m.RegistrationDeadline,
                    StartDate = m.StartDate,
                    EndDate = m.EndDate,
                    NumberOfParticipants = m.NumberOfParticipants,
                    Host = m.Host,
                    Incharge = m.Incharge,
                    Note = m.Note,
                }).First();
            meetimage.Meeting = meet;
            return meet;
        }

        public IEnumerable<Meeting> GetSortedMeetings(
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
            )
        {
            var meetings = _context.Meetings.Where(m => m.MeetingId == meetingId);

            if (meetingName != null)
            {
                meetings = meetings.Where(m => m.MeetingName.Contains(meetingName));
            }
            if (description != null)
            {
                meetings = meetings.Where(m => m.Description.Contains(description));
            }
            if (registrationDeadline != null)
            {
                meetings = meetings.Where(m => m.RegistrationDeadline == registrationDeadline);
            }
            if (startDate != null)
            {
                meetings = meetings.Where(m => m.StartDate == startDate);
            }
            if (endDate != null)
            {
                meetings = meetings.Where(m => m.EndDate == endDate);
            }
            if (numberOfParticipants != null)
            {
                meetings = meetings.Where(m => m.NumberOfParticipants == numberOfParticipants);
            }
            if (host != null)
            {
                meetings = meetings.Where(m => m.Host.Contains(host));
            }
            if (incharge != null)
            {
                meetings = meetings.Where(m => m.Incharge == incharge);
            }
            if (note != null)
            {
                meetings = meetings.Where(m => m.Note == note);
            }
            if (image != null)
            {
                meetings = meetings.Where(m => m.MeetingMedia.Any(mm => mm.Image == image));
            }

            foreach (var meet in meetings)
            {

            }
        }
    }
}
