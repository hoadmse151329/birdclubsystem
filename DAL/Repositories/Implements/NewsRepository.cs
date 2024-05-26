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
    public class NewsRepository : RepositoryBase<News>, INewsRepository
    {
        private readonly BirdClubContext _context;
        public NewsRepository(BirdClubContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> CountNews()
        {
            return _context.News.AsNoTracking().Count();
        }
    }
}
