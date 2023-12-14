using DAL.Infrastructure;
using DAL.Models;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implements
{
    public class ClubInformationRepository : RepositoryBase<ClubInformation>, IClubInformationRepository
    {
        private readonly BirdClubContext _context;
        public ClubInformationRepository(BirdClubContext context) : base(context)
        {
            _context = context;
        }
    }
}
