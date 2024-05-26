﻿using AutoMapper;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using DAL.Infrastructure;
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

        public async Task<IEnumerable<NewsViewModel>> GetAllNews()
        {
            return _mapper.Map<IEnumerable<NewsViewModel>>(await _unitOfWork.NewsRepository.GetAllNews());
        }
    }
}