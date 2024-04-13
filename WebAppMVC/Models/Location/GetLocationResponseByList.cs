using BAL.ViewModels;

namespace WebAppMVC.Models.Location
{
    public class GetLocationResponseByList : DefaultResponseViewModel
    {
        public List<string> Data { get; set; }
    }
}
