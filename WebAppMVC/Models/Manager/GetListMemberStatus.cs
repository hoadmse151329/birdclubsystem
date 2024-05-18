using BAL.ViewModels.Manager;

namespace WebAppMVC.Models.Manager
{
    public class GetListMemberStatus : DefaultResponseViewModel
    {
        public IEnumerable<GetMemberStatus> Data { get; set; }
    }
}
