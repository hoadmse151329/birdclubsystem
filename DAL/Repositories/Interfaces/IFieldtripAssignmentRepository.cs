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
        Task<FieldtripAssignment> GetFieldtripAssignmentByIdNoTracking(int tripId, string memberId);
        Task<bool> IsFieldtripAssignmentExistByIdNoTracking(int tripId, string memberId);
        Task<IEnumerable<FieldtripAssignment>> GetFieldtripAssignmentsByFieldtripIdNoTracking(int tripId);
        Task<IEnumerable<FieldtripAssignment>> GetSortedFieldtripAssignmentsNoTracking(
            int? tripId = null,
            string? memberId = null,
            DateTime? assignedDate = null,
            List<string>? roles = null,
            string? orderBy = null
            );
        Task<IEnumerable<FieldtripAssignment>> GetFieldtripAssignmentsByMemberIdNoTracking(string memberId);
    }
}
