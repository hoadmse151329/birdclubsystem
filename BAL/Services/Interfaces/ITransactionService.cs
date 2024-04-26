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
		Task<TransactionViewModel?> GetTransactionByVnPayId(string? vnPayId);
		Task<IEnumerable<TransactionViewModel>> GetAllTransactionsByUserId(int userId);
		Task<IEnumerable<TransactionViewModel>> GetAllTransactionsByMemberId(string memberId);
        void Create(TransactionViewModel transaction);
		void Update(TransactionViewModel transaction);
		Task<bool> UpdateUserId(int id, int userId);
	}
}
