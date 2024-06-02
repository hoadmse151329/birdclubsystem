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
    public class FieldtripAssignmentRepository : RepositoryBase<FieldtripAssignment>, IFieldtripAssignmentRepository
    {
        private readonly BirdClubContext _context;
        public FieldtripAssignmentRepository(BirdClubContext context) : base(context)
        {
            _context = context;
        }

        public Task<FieldtripAssignment> GetFieldtripAssignmentByIdNoTracking(int tripId, int memberId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FieldtripAssignment>> GetFieldtripAssignmentsByFieldtripIdNoTracking(int tripId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FieldtripAssignment>> GetFieldtripAssignmentsByMemberIdNoTracking(int memberId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FieldtripAssignment>> GetSortedFieldtripAssignmentsNoTracking(
            int? tripId, 
            int? memberId, 
            DateTime? assignedDate, 
            string? role, 
            string? orderBy = null
            )
        {
            throw new NotImplementedException();
        }
    }
}
