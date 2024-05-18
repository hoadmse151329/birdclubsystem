
namespace WebAppMVC.Models
{
    public class DefaultResponseViewModel<T> where T : class
    {
        public DefaultResponseViewModel(bool status, string? errorMessage, string? successMessage)
        {
            Status = status;
            ErrorMessage = errorMessage;
            SuccessMessage = successMessage;
        }
        public DefaultResponseViewModel()
        {
            Status = false;
        }
        public T? Data { get; set; }
        public bool? BoolData { get; set; }
        public int? IntData { get; set; }
        public bool Status { get; set; }
        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }

    }
}
