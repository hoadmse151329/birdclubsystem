using BAL.ViewModels;
using BAL.ViewModels.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IFieldTripService
    {
        Task<FieldTripViewModel?> GetById(int id);
        Task<FieldTripViewModel?> GetByIdCheckIncharge(
            int id, 
            string accToken
            );
        Task<FieldTripViewModel?> GetByIdWithoutInclude(int id);
        Task<IEnumerable<FieldTripViewModel>> GetAllFieldTrips(
            string? role, 
            string? accToken = null
            );
        Task<IEnumerable<FieldTripViewModel>?> GetSortedFieldTrips(
            int? tripId,
            string? tripName,
            DateTime? openRegistration,
            DateTime? registrationDeadline,
            DateTime? startDate,
            DateTime? endDate,
            int? numberOfParticipants,
            List<string>? roads = null,
            List<string>? districts = null,
            List<string>? cities = null,
            List<string>? statuses = null,
            string? orderBy = null,
            bool isMemberOrGuest = false,
            string? accToken = null
            );
        void Create(FieldTripViewModel entity);
        void Create(CreateNewFieldtripVM entity);
        void Update(FieldTripViewModel entity);
        void Update(UpdateFieldtripDetailsVM entity);
        Task<bool> UpdateStatus(UpdateFieldtripStatusVM entity);
        bool UpdateGettingThere(FieldtripGettingThereViewModel entity);
        Task<bool> GetBoolFieldTripId(int id);
        bool UpdateMedia(FieldtripMediaViewModel entity);
        Task<int> CountFieldTrip();
    }
}
