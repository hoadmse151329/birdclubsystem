using BAL.ViewModels.Authenticates;

namespace WebAppMVC.Models.Auth
{
	public class GetAuthenResponse : DefaultResponseViewModel
	{
		public AuthenResponse? Data { get; set; }
	}
}
