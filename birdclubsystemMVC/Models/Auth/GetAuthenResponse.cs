using BAL.ViewModels.Authenticates;

namespace birdclubsystem.Models.Auth
{
	public class GetAuthenResponse
	{
		public bool Success { get; set; }
		public AuthenResponse Data { get; set; }
	}
}
