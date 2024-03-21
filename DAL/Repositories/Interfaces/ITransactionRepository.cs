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
        public Task<Transaction?> GetTransactionById(int id);
        public Task<IEnumerable<Transaction>> GetAllTransactionsByUserId(int id);
    }
}
