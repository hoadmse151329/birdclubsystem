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
    public class FieldTripParticipantRepository : RepositoryBase<FieldTripParticipant>, IFieldTripParticipantRepository
    {
        private readonly BirdClubContext _context;
        public FieldTripParticipantRepository(BirdClubContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> GetBoolFieldTripParticipantById(int tripId, string memberId)
        {
            var mempart = _context.FieldTripParticipants.AsNoTracking().FirstOrDefault(fp => fp.TripId == tripId && fp.MemberId == memberId);
            if (mempart != null) return true;
            return false;
        }

        public async Task<int> GetCountFieldTripParticipantsByTripId(int tripId)
        {
            return _context.FieldTripParticipants.AsNoTracking().Count(trip => trip.TripId == tripId);
        }

        public async Task<int> GetCountFieldTripParticipantsByMemberId(string memId)
        {
            return _context.FieldTripParticipants.AsNoTracking().Count(m => m.MemberId == memId);
        }

        public async Task<FieldTripParticipant> GetFieldTripParticipantById(int tripId, string memberId)
        {
            return _context.FieldTripParticipants.AsNoTracking().FirstOrDefault(fp => fp.TripId == tripId && fp.MemberId == memberId);
        }

        public async Task<IEnumerable<FieldTripParticipant>> GetFieldTripParticipantsByTripId(int tripId)
        {
            return _context.FieldTripParticipants.AsNoTracking().Where(trip => trip.TripId == tripId).Include(f => f.MemberDetail).OrderBy(p => p.ParticipantNo).ToList();
        }

        public async Task<IEnumerable<FieldTripParticipant>> GetFieldTripParticipantsByMemberId(string memberId)
        {
            return _context.FieldTripParticipants.AsNoTracking().Where(m => m.MemberId == memberId).ToList();
        }
        public async Task<IEnumerable<FieldTripParticipant>> GetFieldTripParticipantsByMemberIdInclude(string memberId)
        {
            return _context.FieldTripParticipants.AsNoTracking().Where(m => m.MemberId == memberId).Include(f => f.Trip).ToList();
        }

        public async Task<int> GetParticipationNoFieldTripParticipantById(int tripId, string memberId)
        {
            var mempart = _context.FieldTripParticipants.AsNoTracking().SingleOrDefault(f => f.TripId.Equals(tripId) && f.MemberId.Equals(memberId));
            if (mempart != null) return Int32.Parse(mempart.ParticipantNo);
            return 0;
        }

        public async Task<IEnumerable<FieldTripParticipant>> UpdateAllFieldTripParticipantStatus(List<FieldTripParticipant> part)
        {
            foreach (var participant in part)
            {
                var trippart = _context.FieldTripParticipants
                    .SingleOrDefault(p => p.TripId == participant.TripId && p.MemberId == participant.MemberId);
                if (trippart != null)
                {
                    if (trippart.CheckInStatus != participant.CheckInStatus)
                    {
                        trippart.CheckInStatus = participant.CheckInStatus;
                        _context.Update(trippart);
                    }
                }
            }
            return part;
        }
    }
}
