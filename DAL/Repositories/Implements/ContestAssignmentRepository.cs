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
    public class ContestAssignmentRepository : RepositoryBase<ContestAssignment>, IContestAssignmentRepository
    {
        private readonly BirdClubContext _context;
        public ContestAssignmentRepository(BirdClubContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ContestAssignment> GetContestAssignmentByIdNoTracking(int contestId, string memberId)
        {
            return await _context.ContestAssignments.AsNoTracking().SingleOrDefaultAsync(ca => ca.ContestId.Equals(contestId) && ca.MemberId.Equals(memberId));
        }

        public async Task<bool> IsContestAssignmentExistByIdNoTracking(int contestId, string memberId)
        {
            var isOccupied = await _context.ContestAssignments.AsNoTracking().AnyAsync(ca => ca.ContestId.Equals(contestId) && ca.MemberId.Equals(memberId));
            return isOccupied;
        }

        public async Task<IEnumerable<ContestAssignment>> GetContestAssignmentsByContestIdNoTracking(int contestId)
        {
            return await _context.ContestAssignments.AsNoTracking().Where(ca => ca.ContestId.Equals(contestId)).ToListAsync();
        }

        public async Task<IEnumerable<ContestAssignment>> GetContestAssignmentsByMemberIdNoTracking(string memberId)
        {
            return await _context.ContestAssignments.AsNoTracking().Where(ca => ca.MemberId.Equals(memberId)).ToListAsync();
        }

        public async Task<IEnumerable<ContestAssignment>> GetSortedContestAssignmentsNoTracking(
            int? contestId = null, 
            string? memberId = null, 
            DateTime? assignedDate = null, 
            List<string>? roles = null, 
            string? orderBy = null
            )
        {
            var conAss = _context.ContestAssignments.AsNoTracking().AsQueryable();
            List<string> roleListDefault = new List<string> {
                "Organizer",
                "Judge",
                "Timer"
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

            if (contestId.HasValue && contestId.Value > 0)
            {
                conAss = conAss.Where(m => m.ContestId.Equals(contestId.Value));
            }

            if (!string.IsNullOrEmpty(memberId) && !string.IsNullOrWhiteSpace(memberId))
            {
                conAss = conAss.Where(m => m.MemberId.Contains(memberId));
            }

            if (roles != null && roles.Any())
            {
                conAss = conAss.Where(m => roles.Contains(m.Role));
            }

            if (assignedDate.HasValue)
            {
                conAss = conAss.Where(m => m.AssignedDate.Value.Date.Equals(assignedDate.Value.Date));
            }
            conAss = orderBy switch
            {
                "contestId_asc" => conAss.OrderBy(m => m.ContestId),
                "contestId_desc" => conAss.OrderByDescending(m => m.ContestId),
                "memberId_asc" => conAss.OrderBy(m => m.MemberId),
                "memberId_desc" => conAss.OrderByDescending(m => m.MemberId),
                "assigneddate_asc" => conAss.OrderBy(m => m.AssignedDate.Value.Date),
                "assigneddate_desc" => conAss.OrderByDescending(m => m.AssignedDate.Value.Date),
                "role_asc" => conAss.OrderBy(m => roleListDefault.IndexOf(m.Role)),
                "role_desc" => conAss.OrderByDescending(m => roleListDefault.IndexOf(m.Role)),
                _ => conAss.OrderBy(m => m.ContestId)
            };

            return await conAss.AsNoTracking().ToListAsync();
        }
    }
}
