using AutoMapper;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using BAL.ViewModels.Manager;
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

        public async Task<List<GetFeedbackResponse>> GetAllFeedbacks()
        {
            return _mapper.Map<List<GetFeedbackResponse>>(await _unitOfWork.FeedbackRepository.GetAllFeedbacks());
        }

        public async Task<FeedbackViewModel?> GetFeedbackById(int id)
        {
            var feed = await _unitOfWork.FeedbackRepository.GetFeedbackById(id);
            if (feed != null)
            {
                var feedback = _mapper.Map<FeedbackViewModel>(feed);
                return feedback;
            }
            return null;
        }

        public void Create(FeedbackViewModel feedback)
        {
            var feed = _mapper.Map<Feedback>(feedback);
            _unitOfWork.FeedbackRepository.Create(feed);
            _unitOfWork.Save();
        }

        public async Task<int> CountFeedback()
        {
            return await _unitOfWork.FeedbackRepository.CountFeedback();
        }
    }
}
