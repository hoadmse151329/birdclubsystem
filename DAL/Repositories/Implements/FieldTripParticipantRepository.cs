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
            var mempart = _context.FieldTripParticipants.Find(tripId, memberId);
            if (mempart != null) return true;
            return false;
        }

        public async Task<int> GetCountFieldTripParticipantsByTripId(int tripId)
        {
            return _context.FieldTripParticipants.Count(trip => trip.TripId == tripId);
        }

        public async Task<int> GetCountFieldTripParticipantsByMemberId(string memId)
        {
            return _context.FieldTripParticipants.Count(m => m.MemberId == memId);
        }

        public async Task<FieldTripParticipant> GetFieldTripParticipantById(int tripId, string memberId)
        {
            return _context.FieldTripParticipants.Find(tripId, memberId);
        }

        public async Task<IEnumerable<FieldTripParticipant>> GetFieldTripParticipantsByTripId(int tripId)
        {
            return _context.FieldTripParticipants.Where(trip => trip.TripId == tripId).Include(f => f.Member).ToList();
        }

        public async Task<IEnumerable<FieldTripParticipant>> GetFieldTripParticipantsByMemberId(string memId)
        {
            return _context.FieldTripParticipants.Where(m => m.MemberId == memId).ToList();
        }
        public async Task<IEnumerable<FieldTripParticipant>> GetFieldTripParticipantsByMemberIdInclude(string memId)
        {
            return _context.FieldTripParticipants.Where(m => m.MemberId == memId).Include(f => f.Trip).ToList();
        }

        public async Task<int> GetParticipantNoFieldTripParticipantById(int tripId, string memberId)
        {
            var mempart = _context.FieldTripParticipants.Find(tripId, memberId);
            if (mempart != null) return Int32.Parse(mempart.ParticipantNo);
            return 0;
        }
    }
}
