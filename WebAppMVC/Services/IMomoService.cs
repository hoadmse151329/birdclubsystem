using BAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppMVC.Models.Momo;

namespace BAL.Services.Interfaces
{
    public interface IMomoService
    {
        Task<MomoCreatePaymentResponseModel> CreatePaymentAsync(TransactionViewModel transactionViewModel);
        MomoExecuteResponseModel PaymentExecuteAsync(IQueryCollection collection);
    }
}
