using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    [Table("MeetingAssignment")]
    public partial class MeetingAssignment
    {
        [Key]
        [Column("memberId")]
        [StringLength(255)]
        public string MemberId { get; set; } = null!;
        [Key]
        [Column("meetingId")]
        public int MeetingId { get; set; }
        [Column("assignedDate", TypeName = "datetime")]
        public DateTime? AssignedDate { get; set; }
        [Column("role")]
        [StringLength(50)]
        public string? Role { get; set; }

        [ForeignKey(nameof(MeetingId))]
        [InverseProperty(nameof(Meeting.MeetingAssignments))]
        public virtual Meeting MeetingDetails { get; set; } = null!;
        [ForeignKey(nameof(MemberId))]
        [InverseProperty(nameof(Member.MeetingAssignments))]
        public virtual Member MemberDetails { get; set; } = null!;
    }
}
