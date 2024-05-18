using BAL.ViewModels;

namespace WebAppMVC.Models.Bird
{
    public class GetListBirdByMemberResponse : DefaultResponseViewModel<List<BirdViewModel>>
    {
        public GetListBirdByMemberResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
