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

        public async Task<IEnumerable<Feedback>> GetAllFeedbacks()
        {
            return _context.Feedbacks.AsNoTracking().ToList();
        }

        public async Task<Feedback?> GetFeedbackById(int id)
        {
            return _context.Feedbacks.AsNoTracking().SingleOrDefault(f => f.FeedbackId == id);
        }
    }
}
