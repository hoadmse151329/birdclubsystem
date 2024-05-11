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
        public async Task<ContestViewModel?> GetById(int id)
        {
            var con = await _unitOfWork.ContestRepository.GetContestById(id);
            if (con != null)
            {
                var media = await _unitOfWork.ContestMediaRepository.GetContestMediasByContestId(con.ContestId);
                string locationName = await _unitOfWork.LocationRepository.GetLocationNameById(con.LocationId.Value);
                if (locationName == null)
                {
                    return null;
                }
                int partAmount = await _unitOfWork.ContestParticipantRepository.GetCountContestParticipantsByContestId(con.ContestId);

                var contest = _mapper.Map<ContestViewModel>(con);
                contest.NumberOfParticipants = contest.NumberOfParticipantsLimit - partAmount;
                contest.Address = locationName;

                contest.ContestPictures = (media != null) ? _mapper.Map<IEnumerable<ContestMediaViewModel>>(media).ToList() : null;

                string[] temp = locationName.Split(",");

                contest.AreaNumber = temp[0];
                contest.Street = temp[1];
                contest.District = temp[2];
                contest.City = temp[3];
                return contest;
            }
            return null;
        }
        public async Task<IEnumerable<ContestViewModel>> GetAllContests(string? role)
        {
            string locationName;
            var listcontest = await _unitOfWork.ContestRepository.GetAllContests(role);
            var listcontestview = _mapper.Map<IEnumerable<ContestViewModel>>(listcontest);
            foreach (var itemview in listcontestview)
            {
                foreach(var item in listcontest)
                {
                    if (item.ContestId == itemview.ContestId)
                    {
                        var media = await _unitOfWork.ContestMediaRepository.GetContestMediasByContestId(item.ContestId);
                        itemview.ContestPictures = (media != null) ? _mapper.Map<IEnumerable<ContestMediaViewModel>>(media).ToList() : null;

                        locationName = await _unitOfWork.LocationRepository.GetLocationNameById(item.LocationId.Value);

                        string[] temp = locationName.Split(',');
                        itemview.AreaNumber = temp[0];
                        itemview.Street = temp[1];
                        itemview.District = temp[2];
                        itemview.City = temp[3];
                    }
                }
            }
            
            return listcontestview;
        }
        public void Create(ContestViewModel entity)
        {
            var loc = _unitOfWork.LocationRepository.GetLocationByName(entity.Address.Trim()).Result;

            if (loc == null)
            {
                _unitOfWork.LocationRepository.Update(loc = new Location
                {
                    LocationName = entity.Address.Trim(),
                    Description = ""
                });
                _unitOfWork.Save();
                loc = _unitOfWork.LocationRepository.GetLocationByName(entity.Address.Trim()).Result;
            }
            var contest = _mapper.Map<Contest>(entity);
            contest.LocationId = loc.LocationId;
            _unitOfWork.ContestRepository.Create(contest);
            _unitOfWork.Save();
        }

        public void Update(ContestViewModel entity)
        {
            var loc = _unitOfWork.LocationRepository.GetLocationByContestId(entity.ContestId.Value).Result;

            if (!loc.Equals(entity.Address.Trim()))
            {
                _unitOfWork.LocationRepository.Update(loc = new Location
                {
                    LocationName = entity.Address,
                    Description = loc.Description
                });
                _unitOfWork.Save();
                loc = _unitOfWork.LocationRepository.GetLocationByContestId(entity.ContestId.Value).Result;
            }
            var contest = _mapper.Map<Contest>(entity);
            contest.LocationId = loc.LocationId;
            _unitOfWork.ContestRepository.Update(contest);
            _unitOfWork.Save();
        }

        public async Task<bool> GetBoolContestId(int id)
        {
            var con = await _unitOfWork.ContestRepository.GetBoolContestId(id);
            if (!con) return false;
            return true;
        }
    }
}
