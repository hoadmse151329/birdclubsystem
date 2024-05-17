using BAL.ViewModels.Authenticates;

namespace WebAppMVC.Models.Auth
{
	public class GetGGAuthenResponse : DefaultResponseViewModel<GGAuthenResponse>
	{
        public GetGGAuthenResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
	}
}
