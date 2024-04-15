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
                string locationName = await _unitOfWork.LocationRepository.GetLocationNameById(con.LocationId.Value);
                if (locationName == null)
                {
                    return null;
                }
                int partAmount = await _unitOfWork.ContestParticipantRepository.GetCountContestParticipantsByContestId(con.ContestId);
                var contest = _mapper.Map<ContestViewModel>(con);
                contest.NumberOfParticipantsLimit = contest.NumberOfParticipants - partAmount;
                contest.Address = locationName;
                contest.AreaNumber = locationName[0];
                contest.Street = locationName.Split(",")[1];
                contest.District = locationName.Split(",")[2];
                contest.City = locationName.Split(",")[3];
                return contest;
            }
            return null;
        }
        public async Task<IEnumerable<ContestViewModel>> GetAll()
        {
            string locationName;
            var listcontest = _unitOfWork.ContestRepository.GetAll();
            var listcontestview = _mapper.Map<IEnumerable<ContestViewModel>>(listcontest);
            foreach (var itemview in listcontestview)
            {
                foreach(var item in listcontest)
                {
                    if (item.ContestId == itemview.ContestId)
                    {
                        locationName = await _unitOfWork.LocationRepository.GetLocationNameById(item.LocationId.Value);
                        itemview.AreaNumber = locationName[0];
                        itemview.Street = locationName.Split(",")[1];
                        itemview.District = locationName.Split(",")[2];
                        itemview.City = locationName.Split(",")[3];
                    }
                }
            }
            
            return listcontestview;
        }
        public void Create(ContestViewModel entity)
        {
            var loc = _unitOfWork.LocationRepository.GetLocationByName(entity.Address).Result;

            if (!loc.Equals(entity.Address.Trim()))
            {
                _unitOfWork.LocationRepository.Update(loc = new Location
                {
                    LocationName = entity.Address,
                    Description = loc.Description
                });
                _unitOfWork.Save();
                loc = _unitOfWork.LocationRepository.GetLocationByName(entity.Address).Result;
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
    }
}
