namespace WebAppMVC.Models.Error
{
    public class GetErrorVM 
    {
        public GetErrorVM() 
        {
        }
        public int? Status { get; set; }
        public string? Title { get; set; }
        public List<List<string>> Errors { get; set; }
    }
}
