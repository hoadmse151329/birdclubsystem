namespace BAL.ViewModels
{
    public class CommentViewModel
    {
        public CommentViewModel()
        {
            Date = DateTime.Now;
        }
        public int? CommentId { get; set; }
        public int? BlogId { get; set; }
        public int? Vote { get; set; }
        public string? Description { get; set; }
        public DateTime? Date { get; set; }
        public int? UserId { get; set; }
        public string? UserFullName { get; set; }
    }
}
