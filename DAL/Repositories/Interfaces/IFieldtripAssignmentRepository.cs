using DAL.Infrastructure;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IFieldtripAssignmentRepository : IRepositoryBase<FieldtripAssignment>
    {
        Task<FieldtripAssignment> GetFieldtripAssignmentByIdNoTracking(int tripId, int memberId);
        Task<IEnumerable<FieldtripAssignment>> GetFieldtripAssignmentsByFieldtripIdNoTracking(int tripId);
        Task<IEnumerable<FieldtripAssignment>> GetSortedFieldtripAssignmentsNoTracking(
            int? tripId = null,
            int? memberId = null,
            DateTime? assignedDate = null,
            string? role = null,
            string? orderBy = null
            );
        Task<IEnumerable<FieldtripAssignment>> GetFieldtripAssignmentsByMemberIdNoTracking(int memberId);
    }
}
