using BAL.ViewModels;

namespace WebAppMVC.Models.Transaction
{
	public class GetTransactionResponse : DefaultResponseViewModel
	{
		public TransactionViewModel? Data { get; set; }
	}
}
