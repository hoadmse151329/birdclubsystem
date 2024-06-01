using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    [Table("ContestAssignment")]
    public partial class ContestAssignment
    {
        [Key]
        [Column("memberId")]
        [StringLength(255)]
        public string MemberId { get; set; } = null!;
        [Key]
        [Column("contestId")]
        public int ContestId { get; set; }
        [Column("assignedDate", TypeName = "datetime")]
        public DateTime? AssignedDate { get; set; }
        [Column("role")]
        [StringLength(50)]
        public string? Role { get; set; }

        [ForeignKey(nameof(ContestId))]
        [InverseProperty(nameof(Contest.ContestAssignments))]
        public virtual Contest ContestDetails { get; set; } = null!;
        [ForeignKey(nameof(MemberId))]
        [InverseProperty(nameof(Member.ContestAssignments))]
        public virtual Member MemberDetails { get; set; } = null!;
    }
}
