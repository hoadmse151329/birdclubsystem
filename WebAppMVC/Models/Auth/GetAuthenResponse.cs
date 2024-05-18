using BAL.ViewModels.Authenticates;

namespace WebAppMVC.Models.Auth
{
	public class GetAuthenResponse : DefaultResponseViewModel<AuthenResponse>
	{
        public GetAuthenResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }

	}
}
