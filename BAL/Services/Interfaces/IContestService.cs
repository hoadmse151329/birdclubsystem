﻿using BAL.ViewModels;
using BAL.ViewModels.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IContestService
    {
        Task<ContestViewModel?> GetById(int id);
        Task<ContestViewModel?> GetByIdCheckIncharge(
            int id, 
            string accToken
            );
        Task<IEnumerable<ContestViewModel>> GetAllContests(
            string? role,
            string? accToken = null
            );
        Task<IEnumerable<ContestViewModel>?> GetSortedContests(
            int? tripId,
            string? tripName,
            DateTime? openRegistration,
            DateTime? registrationDeadline,
            DateTime? startDate,
            DateTime? endDate,
            int? numberOfParticipants,
            int? reqMinElo,
            int? reqMaxElo,
            List<string>? roads = null,
            List<string>? districts = null,
            List<string>? cities = null,
            List<string>? statuses = null,
            string? orderBy = null,
            bool isMemberOrGuest = false,
            string? accToken = null
            );
        void Create(ContestViewModel entity);
        void Create(CreateNewContestVM entity);
        void Update(ContestViewModel entity);
        void Update(UpdateContestDetailsVM entity);
        Task<bool> UpdateStatus(UpdateContestStatusVM entity);
        Task<bool> GetBoolContestId(int id);
        Task<int> CountContest();
    }
}
