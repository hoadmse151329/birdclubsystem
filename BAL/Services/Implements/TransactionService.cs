using AutoMapper;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using DAL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Implements
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TransactionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<TransactionViewModel?> GetTransactionById(int id)
        {
            var trans = await _unitOfWork.TransactionRepository.GetTransactionById(id);
            if (trans != null)
            {
                var transaction = _mapper.Map<TransactionViewModel>(trans);
                return transaction;
            }
            return null;
        }

        public async Task<IEnumerable<TransactionViewModel?>> GetAllTransactionsByUserId(int id)
        {
            return _mapper.Map<IEnumerable<TransactionViewModel>>(await
                _unitOfWork.TransactionRepository.GetAllTransactionsByUserId(id));
        }
    }
}
