using AutoMapper;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using DAL.Infrastructure;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Implements
{
    public class NewsService : INewsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public NewsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> CountNews()
        {
            return await _unitOfWork.NewsRepository.CountNews();
        }

        public void Create(NewsViewModel entity)
        {
            var news = _mapper.Map<News>(entity);
            _unitOfWork.NewsRepository.Create(news);
            _unitOfWork.Save();
        }

        public async Task<IEnumerable<NewsViewModel>> GetAllNews()
        {
            return _mapper.Map<IEnumerable<NewsViewModel>>(await _unitOfWork.NewsRepository.GetAllNews());
        }

        public async Task<NewsViewModel?> GetNewsByIdNoTracking(int newsId)
        {
            return _mapper.Map<NewsViewModel>(await _unitOfWork.NewsRepository.GetNewsByIdNoTracking(newsId: newsId));
        }

        public async Task<IEnumerable<NewsViewModel>?> GetSortedNews(
            string? title, 
            string? category, 
            DateTime? uploadDate, 
            List<string>? statuses, 
            string? orderBy, 
            int? userId = null, 
            bool isMemberOrGuest = false)
        {
            return _mapper.Map<IEnumerable<NewsViewModel>>(await _unitOfWork.NewsRepository.GetSortedNews(
                title: title,
                category: category,
                uploadDate: uploadDate,
                statuses: statuses,
                orderBy: orderBy,
                userId: userId,
                isMemberOrGuest: isMemberOrGuest
                ));
        }

        public void Update(NewsViewModel entity)
        {
            var news = _mapper.Map<News>(entity);
            _unitOfWork.NewsRepository.Update(news);
            _unitOfWork.Save();
        }
    }
}
