using DAL.Infrastructure;
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
            return _context.Members.AsNoTracking().Where(x => x.Role == role).ToList();
        }

        public async Task<Member?> GetByEmail(string email)
        {
            return _context.Members.AsNoTrackingWithIdentityResolution().SingleOrDefault(mem => mem.Email == email);
        }

        public async Task<Member?> GetByIdNoTracking(string id)
        {
            return await _context.Members.AsNoTrackingWithIdentityResolution().Include(mem => mem.UserDetail).SingleOrDefaultAsync(mem => mem.MemberId == id);
        }

        public async Task<Member?> GetByIdTracking(string id)
        {
            return await _context.Members.Include(mem => mem.UserDetail).SingleOrDefaultAsync(mem => mem.MemberId == id);
        }

        public async Task<string?> GetMemberNameById(string id)
        {
            return (await _context.Members.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(mem => mem.MemberId == id)).FullName;
        }

        public async Task<IEnumerable<Member>> UpdateAllMemberStatus(List<Member> members)
        {
            foreach(var memberViewModel in members)
            {
                var mem = await _context.Members.SingleOrDefaultAsync(mem => mem.MemberId == memberViewModel.MemberId);
                if (mem != null)
                {
                    if (mem.Status != memberViewModel.Status)
                    {
                        mem.Status = memberViewModel.Status;
                        if(mem.ExpiryDate == null && mem.Status == "Active")
                        {
                            if(DateTime.Now.Day >= DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))
                            {
                                mem.ExpiryDate = DateTime.UtcNow.AddDays(30);
                            }
                            else
                            {
                                mem.ExpiryDate = DateTime.UtcNow.AddMonths(1);
                            }
                        }
                        else if(mem.ExpiryDate != null && mem.Status != "Active")
                        {
                            mem.ExpiryDate = null;
                        }
                        _context.Update(mem);
                    }

                }
            }
            return members;
        }
    }
}
