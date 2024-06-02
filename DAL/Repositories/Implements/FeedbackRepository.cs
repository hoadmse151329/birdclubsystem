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
    public class FeedbackRepository : RepositoryBase<Feedback>, IFeedbackRepository
    {
        private readonly BirdClubContext _context;
        public FeedbackRepository(BirdClubContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Feedback>> GetAllFeedbacks()
        {
            return await _context.Feedbacks.AsNoTracking()
                .Include(f => f.UserDetail.MemberDetail)
                .ToListAsync();
        }

        public async Task<Feedback?> GetFeedbackById(int id)
        {
            return await _context.Feedbacks.AsNoTracking().SingleOrDefaultAsync(f => f.FeedbackId == id);
        }

        public async Task<int> CountFeedback()
        {
            return await _context.Feedbacks.AsNoTracking().CountAsync();
        }
    }
}
