using BAL.ViewModels;

namespace WebAppMVC.Models.Bird
{
    public class GetBirdResponse : DefaultResponseViewModel<BirdViewModel>
    {
        public GetBirdResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
