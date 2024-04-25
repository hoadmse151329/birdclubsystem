using BAL.ViewModels;

namespace WebAppMVC.Models.Transaction
{
    public class GetUserPaymentResponse : DefaultResponseViewModel
    {
        public IEnumerable<TransactionViewModel>? Data { get; set; }
    }
}
