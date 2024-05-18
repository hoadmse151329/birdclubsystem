namespace WebAppMVC.Models.Contest
{
    public class GetContestPostDeRegister : DefaultResponseViewModel<object>
    {
        public GetContestPostDeRegister(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
