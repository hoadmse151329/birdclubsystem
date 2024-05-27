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
        Task<News?> GetNewsByIdNoTracking(int newsId);
        Task<int> CountNews();
        Task <IEnumerable<News>?> GetSortedNews(
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
