using BAL.ViewModels;
using BAL.ViewModels.News;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface INewsService
    {
        Task<int> CountNews();
        Task<NewsViewModel?> GetNewsByIdNoTracking(int newsId);
        void Create(NewsViewModel entity);
        Task<bool> Create(CreateNewNews entity);
        void Update(NewsViewModel entity);
        Task<IEnumerable<NewsViewModel>> GetAllNews();
        Task<IEnumerable<NewsViewModel>?> GetSortedNews(
            string? title,
            List<string>? categories,
            DateTime? uploadDate,
            List<string>? statuses,
            string? orderBy,
            int? userId = null,
            bool isMemberOrGuest = false
            );
    }
}
