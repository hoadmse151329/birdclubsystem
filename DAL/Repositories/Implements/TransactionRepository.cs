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
    public class TransactionRepository : RepositoryBase<Transaction>, ITransactionRepository
    {
        private readonly BirdClubContext _context;
        public TransactionRepository(BirdClubContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Transaction?> GetTransactionById(int id)
        {
            return _context.Transactions.AsNoTracking().SingleOrDefault(trans => trans.TransactionId == id);
        }
        public async Task<IEnumerable<Transaction?>> GetAllTransactionsByUserId(int id)
        {
            return _context.Transactions.AsNoTracking()
                .Include(t => t.UserDetail)
                .Where(t => t.UserId == id).ToList();
        }

		public async Task<Transaction?> GetTransactionByVnPayId(string? vnPayid)
		{
			return _context.Transactions.AsNoTracking().SingleOrDefault(t => t.VnPayId == vnPayid);
		}

        public async Task<IEnumerable<Transaction?>> GetAllTransactionsByMemberId(string id)
        {
            return _context.Transactions.AsNoTracking()
                .Include(t => t.UserDetail)
                .Where(t => t.UserDetail.MemberId == id).ToList();
        }
	}
}
