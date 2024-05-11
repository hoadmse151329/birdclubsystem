using BAL.ViewModels;

namespace WebAppMVC.Models.Bird
{
    public class GetBirdResponse : DefaultResponseViewModel
    {
        public BirdViewModel Data { get; set; }
    }
}
