﻿using DAL.Infrastructure;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implements
{
    public class MemberRepository : RepositoryBase<Member>, IMemberRepository
    {
        private readonly BirdClubContext _context;
        public MemberRepository(BirdClubContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Member?> GetByEmail(string email)
        {
            return _context.Members.AsNoTrackingWithIdentityResolution().SingleOrDefault(mem => mem.Email == email);
        }

        public async Task<Member?> GetByIdNoTracking(string id)
        {
            return await _context.Members.AsNoTrackingWithIdentityResolution().Include(mem => mem.MemberUser).SingleOrDefaultAsync(mem => mem.MemberId == id);
        }

        public async Task<string?> GetMemberNameById(string id)
        {
            return (await _context.Members.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(mem => mem.MemberId == id)).FullName;
        }

        public async Task<IEnumerable<Member>> UpdateAllMemberStatus(List<Member> members)
        {
            foreach(var memberViewModel in members)
            {
                var mem = _context.Members.SingleOrDefault(mem => mem.MemberId == memberViewModel.MemberId);
                if (mem != null)
                {
                    if (mem.Status != memberViewModel.Status)
                    {
                        mem.Status = memberViewModel.Status;
                        _context.Update(mem);
                    }

                }
            }
            return members;
        }
    }
}
