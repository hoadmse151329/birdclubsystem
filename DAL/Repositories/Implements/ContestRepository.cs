using DAL.Infrastructure;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implements
{
    public class ContestRepository : RepositoryBase<Contest>, IContestRepository
    {
        private readonly BirdClubContext _context;
        public ContestRepository(BirdClubContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Contest>> GetAllContests(string? role)
        {
            if (role == "Manager" || role == "Staff")
            {
                return _context.Contests.AsNoTracking().ToList();
            }
            return _context.Contests.AsNoTracking().Where(c => c.Status == "OpenRegistration").ToList();
        }
        public async Task<Contest?> GetContestById(int id)
        {
            return _context.Contests.AsNoTracking().SingleOrDefault(c => c.ContestId == id);
        }

        public IEnumerable<Contest> GetSortedContests(
            int? contestId = null, 
            string? contestName = null,
            DateTime? openRegistration = null, 
            DateTime? registrationDeadline = null, 
            DateTime? startDate = null, 
            DateTime? endDate = null, 
            int? numberOfParticipants = null, 
            int? reqMinElo = null, 
            int? reqMaxElo = null, 
            List<string>? roads = null, 
            List<string>? districts = null, 
            List<string>? cities = null, 
            List<string>? statuses = null, 
            string? orderBy = null, 
            bool isMemberOrGuest = false)
        {
            var roadLocationIds = roads != null && roads.Any() ? GetLocationIdListByLocationName(roads).ToList() : new List<int>();
            var districtLocationIds = districts != null && districts.Any() ? GetLocationIdListByLocationName(districts).ToList() : new List<int>();
            var cityLocationIds = cities != null && cities.Any() ? GetLocationIdListByLocationName(cities).ToList() : new List<int>();

            var contests = _context.Contests.AsNoTracking().AsQueryable();

            List<string> statusListDefault = new List<string> { "OnHold", "OpenRegistration", "ClosedRegistration", "CheckingIn", "Ongoing", "Ended", "Postponed", "Cancelled" };

            if (isMemberOrGuest)
            {
                statuses = new List<string>() { "OpenRegistration" };
            }

            if (contestId.HasValue)
            {
                contests = contests.AsNoTracking().Where(c => c.ContestId.Equals(contestId.Value));
            }

            if (!string.IsNullOrEmpty(contestName))
            {
                contests = contests.AsNoTracking().Where(c => c.ContestName.Contains(contestName));
            }

            if (openRegistration.HasValue)
            {
                contests = contests.AsNoTracking().Where(c => c.OpenRegistration.Value.Date.Equals(openRegistration.Value.Date));
            }

            if (registrationDeadline.HasValue)
            {
                contests = contests.AsNoTracking().Where(c => c.RegistrationDeadline.Value.Date.Equals(registrationDeadline.Value.Date));
            }
            if (startDate.HasValue)
            {
                contests = contests.AsNoTracking().Where(c => c.StartDate.Value.Date.Equals(startDate.Value.Date));
            }
            if (endDate.HasValue)
            {
                contests = contests.AsNoTracking().Where(c => c.EndDate.Value.Date.Equals(endDate.Value.Date));
            }
            if (numberOfParticipants.HasValue)
            {
                contests = contests.AsNoTracking().Where(c => c.NumberOfParticipants == numberOfParticipants.Value);
            }
            if(reqMinElo.HasValue && reqMaxElo.HasValue)
            {
                contests = contests.AsNoTracking().Where(c => c.ReqMinELO >= reqMinElo.Value && c.ReqMaxELO <= reqMaxElo.Value);
            }
            if (roadLocationIds.Any())
            {
                contests = contests.AsNoTracking().Where(c => roadLocationIds.Contains(c.LocationId.Value));
            }
            if (districtLocationIds.Any())
            {
                contests = contests.AsNoTracking().Where(c => districtLocationIds.Contains(c.LocationId.Value));
            }

            if (cityLocationIds.Any())
            {
                contests = contests.AsNoTracking().Where(c => cityLocationIds.Contains(c.LocationId.Value));
            }
            if (statuses != null && statuses.Any())
            {
                contests = contests.AsNoTracking().Where(c => statuses.Contains(c.Status));
            }
            contests = orderBy switch
            {
                "contestname_asc" => contests.OrderBy(c => c.ContestName),
                "contestname_desc" => contests.OrderByDescending(c => c.ContestName),
                "openregistration_asc" => contests.OrderBy(c => c.OpenRegistration),
                "openregistration_desc" => contests.OrderByDescending(c => c.OpenRegistration),
                "registrationdeadline_asc" => contests.OrderBy(c => c.RegistrationDeadline),
                "registrationdeadline_desc" => contests.OrderByDescending(c => c.RegistrationDeadline),
                "startdate_asc" => contests.OrderBy(c => c.StartDate),
                "startdate_desc" => contests.OrderByDescending(c => c.StartDate),
                "enddate_asc" => contests.OrderBy(c => c.EndDate),
                "enddate_desc" => contests.OrderByDescending(c => c.EndDate),
                "reqminelo_asc" => contests.OrderBy(c => c.ReqMinELO),
                "reqminelo_desc" => contests.OrderByDescending(c => c.ReqMinELO),
                "reqmaxelo_asc" => contests.OrderBy(c => c.ReqMaxELO),
                "reqmaxelo_desc" => contests.OrderByDescending(c => c.ReqMaxELO),
                "status_asc" => contests.OrderBy(c => statusListDefault.IndexOf(c.Status)),
                "status_desc" => contests.OrderByDescending(c => statusListDefault.IndexOf(c.Status)),
                _ => contests.OrderBy(c => c.ContestId)
            };

            return contests.ToList();
        }

        public async Task<bool> GetBoolContestId(int id)
        {
            var con = _context.Contests.SingleOrDefault(c => c.ContestId == id);
            if (con != null) return true;
            else return false;
        }

        public async Task<Contest?> GetContestByIdTracking(int id)
        {
            return _context.Contests.SingleOrDefault(c => c.ContestId == id);
        }

        public async Task<Contest?> GetContestByIdWithoutInclude(int id)
        {
            return _context.Contests.AsNoTracking().SingleOrDefault(c => c.ContestId == id);
        }
        private List<int> GetLocationIdListByLocationName(List<string>? locationNames)
        {
            var nameLocationList = new List<Location>();
            foreach (var locationName in locationNames)
            {
                var list = _context.Locations.AsNoTracking().Where(l => l.LocationName.Contains(locationName)).ToList();
                nameLocationList.AddRange(list);
            }
            return nameLocationList.Select(l => l.LocationId).ToList();
        }

        public async Task<int> CountContest()
        {
            return _context.Contests.AsNoTracking().Count();
        }

        public async Task<int> CountContestByStatus(string status)
        {
            return _context.Contests.AsNoTracking().Where(c => c.Status == status).Count();
        }
    }
}
