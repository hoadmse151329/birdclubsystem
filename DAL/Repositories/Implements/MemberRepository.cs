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

        public async Task<IEnumerable<Member>> GetAllByRole(string role)
        {
            return await _context.Members.AsNoTracking().Where(x => x.Role == role).OrderBy(m => m.Status).ToListAsync();
        }

        public async Task<IEnumerable<Member>> GetSortedMembers(
            string? memberId = null,
            string? memberUserName = null,
            string? memberFullName = null,
            DateTime? expiryDateTime = null,
            DateTime? registerDateTime = null,
            DateTime? joinDateTime = null,
            List<string>? roles = null, 
            List<string>? statuses = null, 
            string? orderBy = null, 
            bool isManagerGetMemberList = false,
            bool isManagerGetStaffList = false,
            bool isAdmin = false)
        {
            /*var roadLocationIds = roles != null && roles.Any() ? GetLocationIdListByLocationName(roads).ToList() : new List<int>();
            var districtLocationIds = statuses != null && statuses.Any() ? GetLocationIdListByLocationName(districts).ToList() : new List<int>();*/

            var members = _context.Members.AsNoTracking().AsQueryable();

            List<string> statusListDefault = new List<string> { "Inactive", "Active", "Expired", "Denied", "Suspended" };
            List<string> roleListDefault = new List<string> { "Member", "Manager", "Staff" };

            if (isManagerGetMemberList)
            {
                roles = new List<string>() { "Member" };
            }
            if (isManagerGetStaffList)
            {
                roles = new List<string>() { "Staff" };
            }

            if (isAdmin)
            {
                roles = new List<string>() { "Manager", "Staff" };
            }

            if (!string.IsNullOrEmpty(memberId))
            {
                members = members.AsNoTracking().Where(m => m.MemberId.Equals(memberId));
            }

            if (!string.IsNullOrEmpty(memberUserName))
            {
                members = members.AsNoTracking().Where(m => m.UserName.Contains(memberUserName));
            }

            if (expiryDateTime.HasValue)
            {
                members = members.AsNoTracking().Where(m => m.ExpiryDate == expiryDateTime.Value);
            }
            if (statuses != null && statuses.Any())
            {
                members = members.AsNoTracking().Where(m => statuses.Contains(m.Status));
            }
            if (roles != null && roles.Any())
            {
                members = members.AsNoTracking().Where(m => roles.Contains(m.Role));
            }
            members = orderBy switch
            {
                "memberid_asc" => members.OrderBy(m => m.MemberId),
                "memberid_desc" => members.OrderByDescending(m => m.MemberId),
                "memberusername_asc" => members.OrderBy(m => m.UserName),
                "memberusername_desc" => members.OrderByDescending(m => m.UserName),
                "memberfullname_asc" => members.OrderBy(m => m.FullName),
                "memberfullname_desc" => members.OrderByDescending(m => m.FullName),
                "expirydate_asc" => members.OrderBy(m => m.ExpiryDate),
                "expirydate_desc" => members.OrderByDescending(m => m.ExpiryDate),
                "role_asc" => members.OrderBy(m => roleListDefault.IndexOf(m.Role)),
                "role_desc" => members.OrderByDescending(m => roleListDefault.IndexOf(m.Role)),
                "status_asc" => members.OrderBy(m => statusListDefault.IndexOf(m.Status)),
                "status_desc" => members.OrderByDescending(m => statusListDefault.IndexOf(m.Status)),
                _ => members.OrderBy(m => m.MemberId)
            };

            return members.ToList();
        }

        public async Task<Member?> GetByEmail(string email)
        {
            return await _context.Members.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(mem => mem.Email == email);
        }

        public async Task<Member?> GetByIdNoTracking(string id)
        {
            return await _context.Members.AsNoTrackingWithIdentityResolution().Include(mem => mem.UserDetails).SingleOrDefaultAsync(mem => mem.MemberId == id);
        }

        public async Task<Member?> GetByIdTracking(string id)
        {
            return await _context.Members.Include(mem => mem.UserDetails).SingleOrDefaultAsync(mem => mem.MemberId == id);
        }

        public async Task<string?> GetMemberNameById(string id)
        {
            return (await _context.Members.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(mem => mem.MemberId == id)).FullName;
        }

        public async Task<IEnumerable<Member>> GetAllMemberOnly()
        {
            return await _context.Members.Where(mem => mem.Role == "Member").OrderBy(mem => mem.MemberId).ToListAsync();
        }

        public async Task<IEnumerable<Member>> UpdateAllMemberStatus(List<Member> members)
        {
            foreach (var memberViewModel in members)
            {
                var mem = await _context.Members.SingleOrDefaultAsync(mem => mem.MemberId == memberViewModel.MemberId);
                if (mem != null)
                {
                    if (mem.Status != memberViewModel.Status)
                    {
                        if ((mem.Status == "Expired" || mem.Status == "Inactive") && memberViewModel.Status == "Active")
                        {
                            if (mem.JoinDate == null && mem.Status == "Inactive")
                            {
                                mem.JoinDate = DateTime.Now;
                            }
                            if (DateTime.Now.Day >= DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))
                            {
                                mem.ExpiryDate = DateTime.UtcNow.AddDays(30);
                            }
                            else
                            {
                                mem.ExpiryDate = DateTime.UtcNow.AddMonths(1);
                            }
                        }
                        else
                        if (mem.Status == "Inactive" && memberViewModel.Status == "Denied")
                        {
                            mem.ExpiryDate = null;
                        }
                        mem.Status = memberViewModel.Status;
                        _context.Update(mem);
                    }
                }
            }
            return members;
        }

        public async Task<IEnumerable<Member>> UpdateAllMemberStatusAndRole(List<Member> members)
        {
            foreach (var memberViewModel in members)
            {
                var mem = await _context.Members.SingleOrDefaultAsync(mem => mem.MemberId == memberViewModel.MemberId);
                if (mem != null)
                {
                    if (mem.Status != memberViewModel.Status)
                    {
                        if ((mem.Status == "Expired" || mem.Status == "Inactive") && memberViewModel.Status == "Active")
                        {
                            if (mem.JoinDate == null && mem.Status == "Inactive")
                            {
                                mem.JoinDate = DateTime.Now;
                            }
                            if (DateTime.Now.Day >= DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))
                            {
                                mem.ExpiryDate = DateTime.UtcNow.AddDays(30);
                            }
                            else
                            {
                                mem.ExpiryDate = DateTime.UtcNow.AddMonths(1);
                            }
                        }
                        else
                        if (mem.Status == "Inactive" && memberViewModel.Status == "Denied")
                        {
                            mem.ExpiryDate = null;
                        }
                        mem.Status = memberViewModel.Status;
                        _context.Update(mem);
                    }
                    if (mem.Role != memberViewModel.Role)
                    {
                        mem = await _context.Members.Include(m => m.UserDetails).SingleOrDefaultAsync(mem => mem.MemberId == memberViewModel.MemberId);
                        mem.Role = memberViewModel.Role;
                        mem.UserDetails.Role = memberViewModel.Role;
                        _context.Update(mem);
                    }
                }
            }
            return members;
        }

        public string GenerateNewMemberId()
        {
            var lastMember = _context.Members.OrderByDescending(m => Convert.ToInt32(m.MemberId)).FirstOrDefault();
            int newId = 1;
            if (lastMember != null)
            {
                newId = int.Parse(lastMember.MemberId) + 1;
            }
            return newId.ToString();
        }
    }
}