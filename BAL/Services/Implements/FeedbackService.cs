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
    public class FeedbackService : IFeedbackService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public FeedbackService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FeedbackViewModel>> GetAllFeedbacks()
        {
            return _mapper.Map<IEnumerable<FeedbackViewModel>>(await _unitOfWork.FeedbackRepository.GetAllFeedbacks());
        }

        public void Create(FeedbackViewModel feedback)
        {
            var feed = _mapper.Map<Feedback>(feedback);
            _unitOfWork.FeedbackRepository.Create(feed);
            _unitOfWork.Save();
        }
    }
}
