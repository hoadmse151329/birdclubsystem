using AutoMapper;
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
    }
}
