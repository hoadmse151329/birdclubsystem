using DAL.Infrastructure;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface ITransactionRepository : IRepositoryBase<Transaction>
    {
        Task<Transaction?> GetTransactionById(int id);
		Task<Transaction?> GetTransactionByVnPayId(string? vnPayid);
		Task<IEnumerable<Transaction>> GetAllTransactionsByUserId(int id);
        Task<IEnumerable<Transaction>> GetAllTransactionsByMemberId(string id);
    }
}
