using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class User
    {
        public User()
        {
            Feedbacks = new HashSet<Feedback>();
        }

        public int UserId { get; set; }
        public int? ClubId { get; set; }
        public string? MemberId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }

        public virtual Member? Member { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
