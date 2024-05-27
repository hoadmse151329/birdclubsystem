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
            return _context.News.AsNoTracking().ToList();
        }

        public IEnumerable<News> GetSortedNews(
            int? newsId, 
            string? title, 
            string? category, 
            DateTime? uploadDate, 
            List<string>? statuses, 
            string? orderBy, 
            int? userId = null, 
            bool isMemberOrGuest = false
            )
        {

            var newsfeeds = _context.News.AsNoTracking().AsQueryable();

            List<string> statusListDefault = new List<string> { "Draft", "Published" };

            if (isMemberOrGuest)
            {
                statuses = new List<string>() { "Published" };
            }

            if (newsId.HasValue)
            {
                newsfeeds = newsfeeds.AsNoTracking().Where(m => m.NewsId.Equals(newsId.Value));
            }

            if (userId.HasValue)
            {
                newsfeeds = newsfeeds.AsNoTracking().Where(m => m.UserId.Equals(userId.Value));
            }

            if (!string.IsNullOrEmpty(title))
            {
                newsfeeds = newsfeeds.AsNoTracking().Where(m => m.Title.Contains(title));
            }

            if (!string.IsNullOrEmpty(category))
            {
                newsfeeds = newsfeeds.AsNoTracking().Where(m => m.Category.Contains(category));
            }

            if (uploadDate.HasValue)
            {
                newsfeeds = newsfeeds.AsNoTracking().Where(m => m.UploadDate == uploadDate.Value);
            }
            if (statuses != null && statuses.Any())
            {
                newsfeeds = newsfeeds.AsNoTracking().Where(m => statuses.Contains(m.Status));
            }
            newsfeeds = orderBy switch
            {
                "newstitle_asc" => newsfeeds.OrderBy(m => m.Title),
                "newstitle_desc" => newsfeeds.OrderByDescending(m => m.Title),
                "uploaddate_asc" => newsfeeds.OrderBy(m => m.UploadDate),
                "uploaddate_desc" => newsfeeds.OrderByDescending(m => m.UploadDate),
                "status_asc" => newsfeeds.OrderBy(m => statusListDefault.IndexOf(m.Status)),
                "status_desc" => newsfeeds.OrderByDescending(m => statusListDefault.IndexOf(m.Status)),
                _ => newsfeeds.OrderBy(m => m.NewsId)
            };

            return newsfeeds.ToList();
        }
    }
}
