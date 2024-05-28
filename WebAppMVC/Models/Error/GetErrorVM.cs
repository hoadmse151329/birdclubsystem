namespace WebAppMVC.Models.Error
{
    public class GetErrorVM 
    {
        public GetErrorVM() 
        {
        }
        public string Type { get; set; }
        public int? Status { get; set; }
        public string? Title { get; set; }
        public Dictionary<string, string[]> Errors { get; set; }
    }
}
