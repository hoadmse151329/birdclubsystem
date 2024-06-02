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
    public class FieldtripAssignmentRepository : RepositoryBase<FieldtripAssignment>, IFieldtripAssignmentRepository
    {
        private readonly BirdClubContext _context;
        public FieldtripAssignmentRepository(BirdClubContext context) : base(context)
        {
            _context = context;
        }

        public async Task<FieldtripAssignment> GetFieldtripAssignmentByIdNoTracking(int tripId, string memberId)
        {
            return await _context.FieldtripAssignments.AsNoTracking().SingleOrDefaultAsync(fa => fa.TripId.Equals(tripId) && fa.MemberId.Equals(memberId));
        }

        public async Task<IEnumerable<FieldtripAssignment>> GetFieldtripAssignmentsByFieldtripIdNoTracking(int tripId)
        {
            return await _context.FieldtripAssignments.AsNoTracking().Where(fa => fa.TripId.Equals(tripId)).ToListAsync();
        }

        public async Task<IEnumerable<FieldtripAssignment>> GetFieldtripAssignmentsByMemberIdNoTracking(string memberId)
        {
            return await _context.FieldtripAssignments.AsNoTracking().Where(fa => fa.MemberId.Equals(memberId)).ToListAsync();
        }

        public async Task<IEnumerable<FieldtripAssignment>> GetSortedFieldtripAssignmentsNoTracking(
            int? tripId = null, 
            string? memberId = null, 
            DateTime? assignedDate = null, 
            List<string>? roles = null, 
            string? orderBy = null
            )
        {
            var fieldAss = _context.FieldtripAssignments.AsNoTracking().AsQueryable();
            List<string> roleListDefault = new List<string> {
                "Organizer",
                "Guide",
                "Coordinator",
                "Transporter"
            };
            /*List<string> roleListDefault = new List<string> {
                "Registrar",
                "Master of Ceremonies",
                "Technical",
                "Bird Handler",
                "Scorekeeper",
                "Security Personnel",
                "Veterinarian",
                "Hospitality",
                "Public Relations Officer",
                "Awards Coordinator",
                "Photographer"
            };*/

            if (tripId.HasValue && tripId.Value > 0)
            {
                fieldAss = fieldAss.Where(m => m.TripId.Equals(tripId.Value));
            }

            if (!string.IsNullOrEmpty(memberId) && !string.IsNullOrWhiteSpace(memberId))
            {
                fieldAss = fieldAss.Where(m => m.MemberId.Contains(memberId));
            }

            if (roles != null && roles.Any())
            {
                fieldAss = fieldAss.Where(m => roles.Contains(m.Role));
            }

            if (assignedDate.HasValue)
            {
                fieldAss = fieldAss.Where(m => m.AssignedDate.Value.Date.Equals(assignedDate.Value.Date));
            }
            fieldAss = orderBy switch
            {
                "tripId_asc" => fieldAss.OrderBy(m => m.TripId),
                "tripId_desc" => fieldAss.OrderByDescending(m => m.TripId),
                "memberId_asc" => fieldAss.OrderBy(m => m.MemberId),
                "memberId_desc" => fieldAss.OrderByDescending(m => m.MemberId),
                "assigneddate_asc" => fieldAss.OrderBy(m => m.AssignedDate.Value.Date),
                "assigneddate_desc" => fieldAss.OrderByDescending(m => m.AssignedDate.Value.Date),
                "role_asc" => fieldAss.OrderBy(m => roleListDefault.IndexOf(m.Role)),
                "role_desc" => fieldAss.OrderByDescending(m => roleListDefault.IndexOf(m.Role)),
                _ => fieldAss.OrderBy(m => m.TripId)
            };

            return await fieldAss.AsNoTracking().ToListAsync();
        }

        public async Task<bool> IsFieldtripAssignmentExistByIdNoTracking(int tripId, string memberId)
        {
            var isOccupied = await _context.FieldtripAssignments.AsNoTracking().AnyAsync(fa => fa.TripId.Equals(tripId) && fa.MemberId.Equals(memberId));
            return isOccupied;
        }
    }
}
