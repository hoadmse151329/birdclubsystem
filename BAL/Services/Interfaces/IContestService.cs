﻿using BAL.ViewModels;
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
        Task<IEnumerable<ContestViewModel>> GetAllContests(string? role);
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
            List<string>? roads,
            List<string>? districts,
            List<string>? cities,
            List<string>? statuses,
            string? orderBy,
            bool isMemberOrGuest = false
            );
        void Create(ContestViewModel entity);
        void Update(ContestViewModel entity);
        Task<bool> GetBoolContestId(int id);
    }
}
