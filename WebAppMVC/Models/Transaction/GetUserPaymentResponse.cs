using BAL.ViewModels;

namespace WebAppMVC.Models.Transaction
{
    public class GetUserPaymentResponse : DefaultResponseViewModel<IEnumerable<TransactionViewModel>>
    {
        public GetUserPaymentResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
