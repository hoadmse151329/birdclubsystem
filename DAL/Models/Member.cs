using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Member
    {
        public Member()
        {
            Birds = new HashSet<Bird>();
            MemberUser = new User();
        }

        public string MemberId { get; set; } = null!;
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public string? Role { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Description { get; set; }
        public int Status { get; set; }
        public int? ClubId { get; set; }

        public virtual ICollection<Bird> Birds { get; set; }
        public virtual User MemberUser { get; set; }
    }
}
