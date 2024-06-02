using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    [Table("FieldtripAssignment")]
    public partial class FieldtripAssignment
    {
        [Key]
        [Column("memberId")]
        [StringLength(255)]
        public string MemberId { get; set; } = null!;
        [Key]
        [Column("tripId")]
        public int TripId { get; set; }
        [Column("assignedDate", TypeName = "datetime")]
        public DateTime? AssignedDate { get; set; }
        [Column("role")]
        [StringLength(50)]
        public string? Role { get; set; }

        [ForeignKey(nameof(MemberId))]
        [InverseProperty(nameof(Member.FieldtripAssignments))]
        public virtual Member MemberDetails { get; set; } = null!;
        [ForeignKey(nameof(TripId))]
        [InverseProperty(nameof(FieldTrip.FieldtripAssignments))]
        public virtual FieldTrip Trip { get; set; } = null!;
    }
}
