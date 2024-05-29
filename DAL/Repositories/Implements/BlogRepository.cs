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
    public class BlogRepository : RepositoryBase<Blog>, IBlogRepository
    {
        private readonly BirdClubContext _context;
        public BlogRepository(BirdClubContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Blog>> GetAllBlogs()
        {
            return _context.Blogs.AsNoTracking().Include(b => b.UserDetail.MemberDetail).ToList();
        }

        public async Task<IEnumerable<Blog>?> GetAllBlogsByUserId(int usrId)
        {
            return _context.Blogs.AsNoTracking().Where(b => b.UserId == usrId).Include(b => b.UserDetail.MemberDetail).ToList();
        }

        public async Task<int> CountBlog()
        {
            return _context.Blogs.AsNoTracking().Count();
        }

        public async Task<IEnumerable<Blog>?> GetSortedBlogs(
            string? description, 
            string? category, 
            DateTime? uploadDate, 
            int? vote, 
            List<string>? statuses, 
            string? orderBy, 
            int? userId = null, 
            bool isMemberOrGuest = false)
        {
            var newsfeeds = _context.Blogs.AsNoTracking().Include(b => b.UserDetail.MemberDetail).AsQueryable();
            /*  Draft: The post has been created but not yet published.
                Archived: The post is no longer visible to the public but is kept for record-keeping.
                Hidden: The post is temporarily hidden from the public view.
                Reported: The post has been flagged by users for review.
                Active: The post is currently visible and engaging with users.
                Disabled: The post is restricted due to a violation of platform policies.
             */
            List<string> statusListDefault = new List<string> { "Draft", "Active", "Hidden", "Archived", "Reported", "Disabled" };
            //List<string> categoryListDefault = new List<string> { "Announcement", "Meeting", "Fieldtrip", "Contest", "Others" };

            if (isMemberOrGuest)
            {
                statuses = new List<string>() { "Active" };
            }

            if (userId.HasValue && userId > 0)
            {
                newsfeeds = newsfeeds.Where(m => m.UserId.Equals(userId.Value));
            }

            if (vote.HasValue)
            {
                newsfeeds = newsfeeds.Where(m => m.Vote.Equals(vote.Value));
            }

            if (!string.IsNullOrEmpty(description))
            {
                newsfeeds = newsfeeds.Where(m => m.Description.Contains(description));
            }

            if (!string.IsNullOrEmpty(category))
            {
                newsfeeds = newsfeeds.Where(m => m.Category.Equals(category));
            }

            if (uploadDate.HasValue)
            {
                newsfeeds = newsfeeds.Where(m => m.UploadDate.Date.Equals(uploadDate.Value.Date));
            }
            if (statuses != null && statuses.Any())
            {
                newsfeeds = newsfeeds.Where(m => statuses.Contains(m.Status));
            }
            newsfeeds = orderBy switch
            {
                "vote_asc" => newsfeeds.OrderBy(m => m.Vote),
                "vote_desc" => newsfeeds.OrderByDescending(m => m.Vote),
                "category_asc" => newsfeeds.OrderBy(m => m.Category),
                "category_desc" => newsfeeds.OrderByDescending(m => m.Category),
                "uploaddate_asc" => newsfeeds.OrderBy(m => m.UploadDate.Date),
                "uploaddate_desc" => newsfeeds.OrderByDescending(m => m.UploadDate.Date),
                "status_asc" => newsfeeds.OrderBy(m => statusListDefault.IndexOf(m.Status)),
                "status_desc" => newsfeeds.OrderByDescending(m => statusListDefault.IndexOf(m.Status)),
                _ => newsfeeds.OrderBy(m => m.BlogId)
            };

            return await newsfeeds.AsNoTracking().ToListAsync();
        }

        public async Task<Blog?> GetBlogByIdNoTracking(int blogId)
        {
            return await _context.Blogs.AsNoTracking().Include(b => b.UserDetail.MemberDetail).FirstOrDefaultAsync(b => b.BlogId.Equals(blogId));
        }
    }
}
