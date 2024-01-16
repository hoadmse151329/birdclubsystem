using AutoMapper;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using DAL.Infrastructure;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Implements
{
    public class ContestService : IContestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ContestService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ContestViewModel?> GetContestById(int id)
        {
            var con = await _unitOfWork.ContestRepository.GetContestById(id);
            if (con != null)
            {
                var contest = _mapper.Map<ContestViewModel>(con);
                return contest;
            }
            return null;
        }
        public async Task<IEnumerable<ContestViewModel>> GetAllContests()
        {
            var con = await _unitOfWork.ContestRepository.GetAllContests();
            if (con != null)
            {
                var contest = _mapper.Map<IEnumerable<ContestViewModel>>(con);
                return contest;
            }
            return null;
        }
        public void Create(ContestViewModel entity)
        {
            var contest = _mapper.Map<Contest>(entity);
            _unitOfWork.ContestRepository.Create(contest);
            _unitOfWork.Save();
        }

        public void Update(ContestViewModel entity)
        {
            var contest = _mapper.Map<Contest>(entity);
            _unitOfWork.ContestRepository.Update(contest);
            _unitOfWork.Save();
        }
    }
}
