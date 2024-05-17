using BAL.ViewModels;

namespace WebAppMVC.Models.Transaction
{
	public class GetTransactionResponse : DefaultResponseViewModel<TransactionViewModel>
	{
        public GetTransactionResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
	}
}
