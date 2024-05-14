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
    public class BirdService : IBirdService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BirdService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BirdViewModel>> GetBirdsByMemberId(string memberId)
        {
            return _mapper.Map<IEnumerable<BirdViewModel>>(await
                _unitOfWork.BirdRepository.GetBirdsByMemberId(memberId));
        }

        public async Task<bool> Create(string memberId, BirdViewModel birdModel)
        {
            var mem = await _unitOfWork.MemberRepository.GetByIdNoTracking(memberId);
            if (mem == null) return false;
            var bird = _mapper.Map<Bird>(birdModel);
            bird.MemberId = memberId;
            _unitOfWork.BirdRepository.Create(bird);
            _unitOfWork.Save();
            return true;
        }

        public async Task<bool> Update(string memberId, BirdViewModel birdModel)
        {
            var mem = await _unitOfWork.MemberRepository.GetByIdNoTracking(memberId);
            if (mem == null) return false;
            if (birdModel == null || birdModel.BirdId == null) return false;
            var birdCheck = await _unitOfWork.BirdRepository.GetBirdById(birdModel.BirdId.Value);
            if (birdCheck == null) return false;

            var bird = _mapper.Map<Bird>(birdModel);

            bird.MemberId = memberId;
            _unitOfWork.Save();
            return true;
        }

        public async Task<bool> Delete(string memberId, int birdId)
        {
            var mem = await _unitOfWork.MemberRepository.GetByIdNoTracking(memberId);
            if (mem == null) return false;
            var bird = await _unitOfWork.BirdRepository.GetBirdByIdTracking(birdId);
            if (bird == null) return false;
            _unitOfWork.BirdRepository.Delete(bird);
            _unitOfWork.Save();
            return true;
        }

        public async Task<BirdViewModel> GetById(int birdId)
        {
            return _mapper.Map<BirdViewModel>(await _unitOfWork.BirdRepository.GetBirdById(birdId));
        }

        public async Task<BirdViewModel> GetByBirdName(string birdName)
        {
            return _mapper.Map<BirdViewModel>(await _unitOfWork.BirdRepository.GetBirdByName(birdName));
        }
    }
}
