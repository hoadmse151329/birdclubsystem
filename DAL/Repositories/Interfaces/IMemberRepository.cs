﻿using DAL.Infrastructure;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IMemberRepository : IRepositoryBase<Member>
    {
        Task<IEnumerable<Member>> GetAllByRole(string role);
        Task<IEnumerable<Member>> GetSortedMembers(
            string? memberId,
            string? memberUserName,
            string? memberFullName,
            DateTime? expiryDateTime,
            List<string>? roles,
            List<string>? statuses,
            string? orderBy,
            bool isManager = false,
            bool isAdmin = false
            );
        Task<Member?> GetByIdNoTracking(string id);
        Task<Member?> GetByIdTracking(string id);
        Task<string?> GetMemberNameById(string id);
        Task<Member?> GetByEmail(string email);
        Task<IEnumerable<Member>> UpdateAllMemberStatus(List<Member> members);

    }
}
