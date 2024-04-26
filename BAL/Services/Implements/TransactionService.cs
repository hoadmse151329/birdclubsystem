using AutoMapper;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using DAL.Infrastructure;
using DAL.Models;
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

        public async Task<IEnumerable<TransactionViewModel?>> GetAllTransactionsByUserId(int userId)
        {
            return _mapper.Map<IEnumerable<TransactionViewModel>>(await
                _unitOfWork.TransactionRepository.GetAllTransactionsByUserId(userId));
        }

		public void Create(TransactionViewModel transaction)
		{
            var tran = _mapper.Map<Transaction>(transaction);
            _unitOfWork.TransactionRepository.Create(tran);
            _unitOfWork.Save();
		}

		public async Task<TransactionViewModel?> GetTransactionByVnPayId(string? vnPayId)
		{
			return _mapper.Map<TransactionViewModel>(await
				_unitOfWork.TransactionRepository.GetTransactionByVnPayId(vnPayId));
		}

        public async Task<IEnumerable<TransactionViewModel?>> GetAllTransactionsByMemberId(string memberId)
        {
            return _mapper.Map<IEnumerable<TransactionViewModel>>(await
                _unitOfWork.TransactionRepository.GetAllTransactionsByMemberId(memberId));
        }

		public void Update(TransactionViewModel transaction)
		{
			var tran = _mapper.Map<Transaction>(transaction);
			_unitOfWork.TransactionRepository.Update(tran);
			_unitOfWork.Save();
		}

		public async Task<bool> UpdateUserId(int id, int userId)
		{
			var trans = await _unitOfWork.TransactionRepository.GetTransactionById(id);
			if (trans != null)
			{
                trans.UserId = userId;
				_unitOfWork.TransactionRepository.Update(trans);
				_unitOfWork.Save();
                return true;
			}
            return false;
		}
	}
}
