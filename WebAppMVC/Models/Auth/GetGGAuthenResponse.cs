using BAL.ViewModels.Authenticates;

namespace WebAppMVC.Models.Auth
{
	public class GetGGAuthenResponse : DefaultResponseViewModel
	{
		public GGAuthenResponse? Data { get; set; }
	}
}
