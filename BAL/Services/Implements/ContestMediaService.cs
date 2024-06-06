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
    public class ContestMediaService : IContestMediaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ContestMediaService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> Create(int contestId, ContestMediaViewModel media)
        {
            var contest = await _unitOfWork.ContestRepository.GetContestById(contestId);
            if (contest == null) return false;
            var pic = _mapper.Map<ContestMedia>(media);
            pic.ContestId = contestId;
            if (media.Image == null)
            {
                pic.Image = "https://edwinbirdclubstorage.blob.core.windows.net/images/contest/contest_image_1.png";
            }
            _unitOfWork.ContestMediaRepository.Create(pic);
            _unitOfWork.Save();
            return true;
        }

        public async Task<bool> Delete(int contestId, int pictureId)
        {
            var contest = await _unitOfWork.ContestRepository.GetContestById(contestId);
            if (contest == null) return false;
            var pic = await _unitOfWork.ContestMediaRepository.GetContestMediaById(pictureId);
            if (pic == null) return false;
            _unitOfWork.ContestMediaRepository.Delete(pic);
            _unitOfWork.Save();
            return true;
        }

        public Task<IEnumerable<ContestMediaViewModel>> GetAllByContestId(int contestId)
        {
            throw new NotImplementedException();
        }

        public async Task<ContestMediaViewModel> GetById(int pictureId)
        {
            return _mapper.Map<ContestMediaViewModel>(await _unitOfWork.ContestMediaRepository.GetContestMediaById(pictureId));
        }

        public async Task<bool> Update(int contestId, ContestMediaViewModel media)
        {
            var contest = await _unitOfWork.ContestRepository.GetContestById(contestId);
            if (contest == null) return false;
            if (media == null || media.PictureId == null) return false;
            var contestmedia = await _unitOfWork.ContestMediaRepository.GetContestMediaById(media.PictureId.Value);
            if (contestmedia == null) return false;
            var pic = _mapper.Map<ContestMedia>(media);
            pic.ContestId = contestId;
            if (media.Image == null)
            {
                pic.Image = "https://edwinbirdclubstorage.blob.core.windows.net/images/contest/contest_image_1.png";
            }
            _unitOfWork.ContestMediaRepository.Update(pic);
            _unitOfWork.Save();
            return true;
        }
    }
}
