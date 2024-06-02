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
            return await _context.Transactions.AsNoTracking().SingleOrDefaultAsync(trans => trans.TransactionId == id);
        }
        public async Task<IEnumerable<Transaction?>> GetAllTransactionsByUserId(int id)
        {
            return await _context.Transactions.AsNoTracking()
                .Include(t => t.UserDetail)
                .Where(t => t.UserId == id).ToListAsync();
        }

		public async Task<Transaction?> GetTransactionByVnPayId(string? vnPayid)
		{
			return await _context.Transactions.AsNoTracking().SingleOrDefaultAsync(t => t.VnPayId == vnPayid);
		}

        public async Task<IEnumerable<Transaction?>> GetAllTransactionsByMemberId(string id)
        {
            return await _context.Transactions.AsNoTracking()
                .Include(t => t.UserDetail)
                .Where(t => t.UserDetail.MemberId == id).ToListAsync();
        }

        public async Task<int> CalculateTotalValue()
        {
            return await _context.Transactions.AsNoTracking().Where(t => t.Status != "Refund").SumAsync(t => t.Value.Value);
        }

        public async Task<int> CalculateTotalRefund()
        {
            return await _context.Transactions.AsNoTracking().Where(t => t.Status == "Refund").SumAsync(t => t.Value.Value);
        }
	}
}
