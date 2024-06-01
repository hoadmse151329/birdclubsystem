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
    public class NewsRepository : RepositoryBase<News>, INewsRepository
    {
        private readonly BirdClubContext _context;
        public NewsRepository(BirdClubContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> CountNews()
        {
            return _context.News.AsNoTracking().Count();
        }

        public async Task<IEnumerable<News>> GetAllNews()
        {
            return await _context.News.AsNoTracking().ToListAsync();
        }

        public async Task<News?> GetNewsByIdNoTracking(int newsId)
        {
            return await _context.News.AsNoTracking().FirstOrDefaultAsync(n => n.NewsId.Equals(newsId));
        }

        public async Task<News?> GetNewsByIdNoTrackingGuestOrMember(int newsId)
        {
            return await _context.News.AsNoTracking().FirstOrDefaultAsync(n => n.NewsId.Equals(newsId) && n.Status.Equals("Active"));
        }

        public async Task<IEnumerable<News>?> GetSortedNews(
            string? title, 
            List<string>? categories, 
            DateTime? uploadDate, 
            List<string>? statuses, 
            string? orderBy, 
            int? userId = null, 
            bool isMemberOrGuest = false
            )
        {

            var newsfeeds = _context.News.AsNoTracking().AsQueryable();
            /*  Draft: The post has been created but not yet published.
                Archived: The post is no longer visible to the public but is kept for record-keeping.
                Hidden: The post is temporarily hidden from the public view.
                Reported: The post has been flagged by users for review.
                Active: The post is currently visible and engaging with users.
                Disabled: The post is restricted due to a violation of platform policies.
             */
            List<string> statusListDefault = new List<string> { "Draft", "Active", "Hidden", "Archived", "Reported", "Disabled" };
            List<string> categoryListDefault = new List<string> { "Announcement", "Meeting", "Fieldtrip", "Contest", "Others" };

            if (isMemberOrGuest)
            {
                statuses = new List<string>() { "Active" };
            }

            if (userId.HasValue && userId.Value > 0)
            {
                newsfeeds = newsfeeds.Where(m => m.UserId.Equals(userId.Value));
            }

            if (!string.IsNullOrEmpty(title))
            {
                newsfeeds = newsfeeds.Where(m => m.Title.Contains(title));
            }

            if (categories != null && categories.Any())
            {
                newsfeeds = newsfeeds.Where(m => categories.Contains(m.Category));
            }

            if (uploadDate.HasValue)
            {
                newsfeeds = newsfeeds.Where(m => m.UploadDate.Value.Date.Equals(uploadDate.Value.Date));
            }
            if (statuses != null && statuses.Any())
            {
                newsfeeds = newsfeeds.Where(m => statuses.Contains(m.Status));
            }
            newsfeeds = orderBy switch
            {
                "newstitle_asc" => newsfeeds.OrderBy(m => m.Title),
                "newstitle_desc" => newsfeeds.OrderByDescending(m => m.Title),
                "category_asc" => newsfeeds.OrderBy(m => categoryListDefault.IndexOf(m.Category)),
                "category_desc" => newsfeeds.OrderByDescending(m => categoryListDefault.IndexOf(m.Category)),
                "uploaddate_asc" => newsfeeds.OrderBy(m => m.UploadDate.Value.Date),
                "uploaddate_desc" => newsfeeds.OrderByDescending(m => m.UploadDate.Value.Date),
                "status_asc" => newsfeeds.OrderBy(m => statusListDefault.IndexOf(m.Status)),
                "status_desc" => newsfeeds.OrderByDescending(m => statusListDefault.IndexOf(m.Status)),
                _ => newsfeeds.OrderBy(m => m.NewsId)
            };

            return await newsfeeds.AsNoTracking().ToListAsync();
        }
    }
}
