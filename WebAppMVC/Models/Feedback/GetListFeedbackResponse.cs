using BAL.ViewModels.Manager;

namespace WebAppMVC.Models.Feedback
{
    public class GetListFeedbackResponse : DefaultResponseViewModel<List<GetFeedbackResponse>>
    {
        public GetListFeedbackResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        { 
        }
    }
}
