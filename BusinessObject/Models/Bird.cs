using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public partial class Bird
    {
        public int birdId { get; set; }
        public int memberId {  get; set; }
        public string birdName { get; set; } = null!;
        public int ELO { get; set; }
        public int? age { get; set; }
        public string? description { get; set; }
        public string? color { get; set; }
        public DateTime? addDate { get; set; }
        public string? profilePic { get; set; }
        public string status { get; set; } = null!;
        public string? origin { get; set; }
    }
}
