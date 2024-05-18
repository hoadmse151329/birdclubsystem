using BAL.ViewModels;

namespace WebAppMVC.Models.Bird
{
    public class GetListBirdByMemberResponse : DefaultResponseViewModel
    {
        public List<BirdViewModel> Data { get; set; }
    }
}
