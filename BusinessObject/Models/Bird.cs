using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Bird
    {
        public Bird()
        {
            BirdMedia = new HashSet<BirdMedium>();
        }

        public int BirdId { get; set; }
        public int MemberId { get; set; }
        public string BirdName { get; set; } = null!;
        public int Elo { get; set; }
        public int? Age { get; set; }
        public string? Description { get; set; }
        public string? Color { get; set; }
        public DateTime? AddDate { get; set; }
        public string? ProfilePic { get; set; }
        public string Status { get; set; } = null!;
        public string? Origin { get; set; }

        public virtual Member Member { get; set; } = null!;
        public virtual ICollection<BirdMedium> BirdMedia { get; set; }
    }
}
