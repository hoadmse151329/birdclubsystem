using BAL.ViewModels;

namespace WebAppMVC.Models.Feedback
{
    public class GetFeedbackPostResponse : DefaultResponseViewModel<FeedbackViewModel>
    {
        public GetFeedbackPostResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {

        }
    }
}
