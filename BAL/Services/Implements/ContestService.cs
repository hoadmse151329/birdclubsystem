﻿using AutoMapper;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using BAL.ViewModels.Manager;
using DAL.Infrastructure;
using DAL.Models;
using Microsoft.Extensions.Configuration;
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
        private readonly IJWTService _jwtService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public ContestService(IUnitOfWork unitOfWork, IMapper mapper, IJWTService jWTService, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtService = jWTService;
            _configuration = configuration;
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
                contest.NumberOfParticipants = partAmount;
                contest.Address = locationName;

                contest.ContestPictures = (media.Count() > 0) ? _mapper.Map<IEnumerable<ContestMediaViewModel>>(media).ToList() : contest.ContestPictures;

                string[] temp = locationName.Split(",");

                contest.AreaNumber = temp[0];
                contest.Street = temp[1];
                contest.District = temp[2];
                contest.City = temp[3];
                foreach (var picture in contest.ContestPictures.ToList())
                {
                    if (picture.Type == "Spotlight")
                    {
                        contest.SpotlightImage = picture;
                        contest.ContestPictures.Remove(picture);
                    }
                    else
                    if (picture.Type == "LocationMap")
                    {
                        contest.LocationMapImage = picture;
                        contest.ContestPictures.Remove(picture);
                    }
                }
                return contest;
            }
            return null;
        }
        public async Task<IEnumerable<ContestViewModel>> GetAllContests(
            string? role,
            string? accToken = null
            )
        {
            string locationName;
            var listcontest = await _unitOfWork.ContestRepository.GetAllContests(role);
            var listcontestview = _mapper.Map<IEnumerable<ContestViewModel>>(listcontest);
            string member = string.Empty;
            if (accToken != null)
            {
                member = await _unitOfWork.MemberRepository.GetMemberNameById(_jwtService.ExtractToken(accToken, _configuration).UserId);
            }
            foreach (var itemview in listcontestview)
            {
                foreach(var item in listcontest)
                {
                    if (item.ContestId == itemview.ContestId)
                    {
                        var media = await _unitOfWork.ContestMediaRepository.GetContestMediaByContestIdAndType(item.ContestId, "Spotlight");
                        itemview.SpotlightImage = (media != null) ? _mapper.Map<ContestMediaViewModel>(media) : itemview.SpotlightImage;
                        if (!string.IsNullOrEmpty(member) && !string.IsNullOrWhiteSpace(member))
                        {
                            itemview.isIncharge = member.Equals(itemview.Incharge);
                        }
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

        public async Task<IEnumerable<ContestViewModel>?> GetSortedContests(
            int? tripId, 
            string? tripName,
            DateTime? openRegistration,
            DateTime? registrationDeadline, 
            DateTime? startDate, 
            DateTime? endDate, 
            int? numberOfParticipants, 
            int? reqMinElo, 
            int? reqMaxElo, 
            List<string>? roads, 
            List<string>? districts, 
            List<string>? cities, 
            List<string>? statuses, 
            string? orderBy, 
            bool isMemberOrGuest = false,
            string? accToken = null
            )
        {
            var listcontest = _unitOfWork.ContestRepository.GetSortedContests(
                tripId,
                tripName,
                openRegistration,
                registrationDeadline,
                startDate,
                endDate,
                numberOfParticipants,
                reqMinElo,
                reqMaxElo,
                roads,
                districts,
                cities,
                statuses,
                orderBy,
                isMemberOrGuest
                );
            var listcontestview = _mapper.Map<IEnumerable<ContestViewModel>>(_unitOfWork.ContestRepository.GetSortedContests(
                tripId,
                tripName,
                openRegistration,
                registrationDeadline,
                startDate,
                endDate,
                numberOfParticipants,
                reqMinElo,
                reqMaxElo,
                roads,
                districts,
                cities,
                statuses,
                orderBy,
                isMemberOrGuest
                ));
            string member = string.Empty;
            if (accToken != null)
            {
                member = await _unitOfWork.MemberRepository.GetMemberNameById(_jwtService.ExtractToken(accToken, _configuration).UserId);
            }
            string locationName;
            foreach (var itemview in listcontestview)
            {
                foreach (var item in listcontest)
                {
                    if (item.ContestId == itemview.ContestId)
                    {
                        //int partAmount = await _unitOfWork.MeetingParticipantRepository.GetCountMeetingParticipantsByMeetId(meet.MeetingId);
                        var media = await _unitOfWork.ContestMediaRepository.GetContestMediaByContestIdAndType(item.ContestId, "SpotlightImage");
                        itemview.SpotlightImage = (media != null) ? _mapper.Map<ContestMediaViewModel>(media) : itemview.SpotlightImage;
                        if (!string.IsNullOrEmpty(member) && !string.IsNullOrWhiteSpace(member))
                        {
                            itemview.isIncharge = member.Equals(itemview.Incharge);
                        }
                        locationName = await _unitOfWork.LocationRepository.GetLocationNameById(item.LocationId.Value);

                        itemview.Address = locationName;

                        string[] temp = locationName.Split(",");
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
        public void Create(CreateNewContestVM entity)
        {
            var loc = _unitOfWork.LocationRepository.GetLocationByName(entity.Address.Trim()).Result;

            if (loc == null)
            {
                _unitOfWork.LocationRepository.Update(loc = new Location
                {
                    LocationName = entity.Address.Trim(),
                    Description = string.Empty
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
        public void Update(UpdateContestDetailsVM entity)
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
            var media = _unitOfWork.ContestMediaRepository.GetContestMediasByContestId(entity.ContestId.Value).Result;

            if (media == null)
            {
                _unitOfWork.ContestMediaRepository.Update(new ContestMedia
                {
                    ContestId = entity.ContestId.Value,
                });
                _unitOfWork.Save();
                media = _unitOfWork.ContestMediaRepository.GetContestMediasByContestId(entity.ContestId.Value).Result;
            }
            /*if (entity.Status.Equals("ClosedRegistration") && contest.NumberOfParticipants < 10)
            {
                return false;
            }*/
            var contest = _mapper.Map<Contest>(entity);
            contest.LocationId = loc.LocationId;
            contest.ContestPictures = (ICollection<ContestMedia>)media;
            _unitOfWork.ContestRepository.Update(contest);
            _unitOfWork.Save();
        }

        public async Task<bool> GetBoolContestId(int id)
        {
            var con = await _unitOfWork.ContestRepository.GetBoolContestId(id);
            if (!con) return false;
            return true;
        }

        public async Task<int> CountContest()
        {
            return await _unitOfWork.ContestRepository.CountContest();
        }

        public async Task<int> CountContestByStatus(string status)
        {
            return await _unitOfWork.ContestRepository.CountContestByStatus(status);
        }

        public async Task<bool> UpdateStatus(UpdateContestStatusVM entity)
        {
            var contest = await _unitOfWork.ContestRepository.GetContestById(entity.ContestId.Value);
            if (entity.Status.Equals("ClosedRegistration") && contest.NumberOfParticipants < 10)
            {
                return false;
            }
            contest.Status = entity.Status;
            _unitOfWork.ContestRepository.Update(contest);
            _unitOfWork.Save();
            return true;
        }

        public async Task<ContestViewModel?> GetByIdCheckIncharge(int id, string accToken)
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
                contest.NumberOfParticipants = partAmount;
                contest.Address = locationName;

                contest.ContestPictures = (media.Count() > 0) ? _mapper.Map<IEnumerable<ContestMediaViewModel>>(media).ToList() : contest.ContestPictures;

                string[] temp = locationName.Split(",");

                contest.AreaNumber = temp[0];
                contest.Street = temp[1];
                contest.District = temp[2];
                contest.City = temp[3];

                var member = await _unitOfWork.MemberRepository.GetMemberNameById(_jwtService.ExtractToken(accToken, _configuration).UserId);
                contest.isIncharge = member.Equals(contest.Incharge);

                foreach (var picture in contest.ContestPictures.ToList())
                {
                    if (picture.Type == "Spotlight")
                    {
                        contest.SpotlightImage = picture;
                        contest.ContestPictures.Remove(picture);
                    }
                    else
                    if (picture.Type == "LocationMap")
                    {
                        contest.LocationMapImage = picture;
                        contest.ContestPictures.Remove(picture);
                    }
                }
                return contest;
            }
            return null;
        }
    }
}
