using BAL.ViewModels.Manager;

namespace WebAppMVC.Models.Manager
{
    public class GetMeetingCreateResponse : DefaultResponseViewModel<CreateNewMeetingVM>
    {
        public GetMeetingCreateResponse()
        {
        }

        public GetMeetingCreateResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
