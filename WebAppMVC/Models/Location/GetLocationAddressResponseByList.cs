namespace WebAppMVC.Models.Location
{
    public class GetLocationAddressResponseByList : DefaultResponseViewModel<List<string>>
    {
        public GetLocationAddressResponseByList(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }

        //public List<string>? Data { get; set; }
    }
}
