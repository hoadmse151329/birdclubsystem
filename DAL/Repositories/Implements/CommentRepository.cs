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
    public class CommentRepository : RepositoryBase<Comment>, ICommentRepository
    {
        private readonly BirdClubContext _context;
        public CommentRepository(BirdClubContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comment>> GetAllByBlogId(int blogId)
        {
            return await _context.Comments.AsNoTracking().Include(c => c.UserDetail.MemberDetail).Where(c => c.BlogId.Value.Equals(blogId)).ToListAsync();
        }
    }
}
