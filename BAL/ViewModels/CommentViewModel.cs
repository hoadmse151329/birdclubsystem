using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels
{
    public class CommentViewModel
    {
        public int? CommentId { get; set; }
        public int? BlogId { get; set; }
        public int? Vote { get; set; }
        public string? Description { get; set; }
        public DateTime? Date { get; set; }
        public int? UserId { get; set; }
    }
}
