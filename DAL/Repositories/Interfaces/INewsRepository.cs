using DAL.Infrastructure;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface INewsRepository : IRepositoryBase<News>
    {
        Task<IEnumerable<News>> GetAllNews();
        Task<int> CountNews();
        IEnumerable<News> GetSortedNews(
            int? newsId,
            string? title,
            string? category,
            DateTime? uploadDate,
            List<string>? statuses,
            string? orderBy,
            int? userId = null,
            bool isMemberOrGuest = false
            );
    }
}
