using DAL.Infrastructure;
using DAL.Models;
using DAL.Repositories.Interfaces;
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
			throw new NotImplementedException();
		}

		public IEnumerable<Meeting> GetSortedMeetings(int meetingId, string? meetingName, string? meetingDescription, DateTime? registrationDeadline, DateTime? startDate, DateTime? endDate, int numberOfParticipants, string? host, string? incharge, string? note)
		{
			throw new NotImplementedException();
		}
	}
}
