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
    public class BirdMediaRepository : RepositoryBase<BirdMedia>, IBirdMediaRepository
    {
        private readonly BirdClubContext _context;
        public BirdMediaRepository(BirdClubContext context) : base(context)
        {
            _context = context;
        }   
    }
}
