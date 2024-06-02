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
            var mempart = await _context.FieldTripParticipants.AsNoTracking().FirstOrDefaultAsync(fp => fp.TripId == tripId && fp.MemberId == memberId);
            if (mempart != null) return true;
            return false;
        }

        public async Task<int> GetCountFieldTripParticipantsByTripId(int tripId)
        {
            return await _context.FieldTripParticipants.AsNoTracking().CountAsync(trip => trip.TripId == tripId);
        }

        public async Task<int> GetCountFieldTripParticipantsByMemberId(string memId)
        {
            return await _context.FieldTripParticipants.AsNoTracking().CountAsync(m => m.MemberId == memId);
        }

        public async Task<FieldTripParticipant> GetFieldTripParticipantById(int tripId, string memberId)
        {
            return await _context.FieldTripParticipants.AsNoTracking().FirstOrDefaultAsync(fp => fp.TripId == tripId && fp.MemberId == memberId);
        }

        public async Task<IEnumerable<FieldTripParticipant>> GetFieldTripParticipantsByTripId(int tripId)
        {
            return await _context.FieldTripParticipants
                .AsNoTracking()
                .Where(trip => trip.TripId == tripId)
                .Include(f => f.MemberDetails)
                .OrderBy(p => p.ParticipantNo)
                .ToListAsync();
        }

        public async Task<IEnumerable<FieldTripParticipant>> GetFieldTripParticipantsByMemberId(string memberId)
        {
            return await _context.FieldTripParticipants.AsNoTracking().Where(m => m.MemberId == memberId).ToListAsync();
        }
        public async Task<IEnumerable<FieldTripParticipant>> GetFieldTripParticipantsByMemberIdInclude(string memberId)
        {
            return await _context.FieldTripParticipants.AsNoTracking().Where(m => m.MemberId == memberId).Include(f => f.Trip).ToListAsync();
        }

        public async Task<int> GetParticipationNoFieldTripParticipantById(int tripId, string memberId)
        {
            var mempart = await _context.FieldTripParticipants.AsNoTracking().SingleOrDefaultAsync(f => f.TripId.Equals(tripId) && f.MemberId.Equals(memberId));
            if (mempart != null) return mempart.ParticipantNo.Value;
            return 0;
        }

        public async Task<IEnumerable<FieldTripParticipant>> UpdateAllFieldTripParticipantStatus(List<FieldTripParticipant> part)
        {
            foreach (var participant in part)
            {
                var trippart = await _context.FieldTripParticipants
                    .SingleOrDefaultAsync(p => p.TripId == participant.TripId && p.MemberId == participant.MemberId);
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
