using BAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<TransactionViewModel?> GetTransactionById(int id);
        Task<IEnumerable<TransactionViewModel>> GetAllTransactionsByUserId(int id);
    }
}
