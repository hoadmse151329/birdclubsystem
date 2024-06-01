using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    [Table("Member")]
    public partial class Member
    {
        public Member()
        {
            Birds = new HashSet<Bird>();
            ContestAssignments = new HashSet<ContestAssignment>();
            ContestParticipants = new HashSet<ContestParticipant>();
            FieldTripParticipants = new HashSet<FieldTripParticipant>();
            FieldtripAssignments = new HashSet<FieldtripAssignment>();
            MeetingAssignments = new HashSet<MeetingAssignment>();
            MeetingParticipants = new HashSet<MeetingParticipant>();
            UserDetails = new User();
        }

        [Key]
        [Column("memberId")]
        [StringLength(255)]
        public string MemberId { get; set; } = null!;
        [Column("fullName")]
        [StringLength(255)]
        public string? FullName { get; set; }
        [Column("userName")]
        [StringLength(50)]
        public string? UserName { get; set; }
        [Column("email")]
        [StringLength(255)]
        public string? Email { get; set; }
        [Column("gender")]
        [StringLength(10)]
        public string? Gender { get; set; }
        [Column("role")]
        [StringLength(50)]
        public string? Role { get; set; }
        [Column("address")]
        public string? Address { get; set; }
        [Column("phone")]
        [StringLength(20)]
        public string? Phone { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("status")]
        [StringLength(50)]
        public string? Status { get; set; }
        [Column("registerDate", TypeName = "datetime")]
        public DateTime? RegisterDate { get; set; }
        [Column("joinDate", TypeName = "datetime")]
        public DateTime? JoinDate { get; set; }
        [Column("expiryDate", TypeName = "datetime")]
        public DateTime? ExpiryDate { get; set; }
        [Column("clubId")]
        public int? ClubId { get; set; }

        [InverseProperty(nameof(Bird.MemberDetails))]
        public virtual ICollection<Bird> Birds { get; set; }
        [InverseProperty(nameof(ContestAssignment.MemberDetails))]
        public virtual ICollection<ContestAssignment> ContestAssignments { get; set; }
        [InverseProperty(nameof(ContestParticipant.MemberDetails))]
        public virtual ICollection<ContestParticipant> ContestParticipants { get; set; }
        [InverseProperty(nameof(FieldTripParticipant.MemberDetails))]
        public virtual ICollection<FieldTripParticipant> FieldTripParticipants { get; set; }
        [InverseProperty(nameof(FieldtripAssignment.MemberDetails))]
        public virtual ICollection<FieldtripAssignment> FieldtripAssignments { get; set; }
        [InverseProperty(nameof(MeetingAssignment.MemberDetails))]
        public virtual ICollection<MeetingAssignment> MeetingAssignments { get; set; }
        [InverseProperty(nameof(MeetingParticipant.MemberDetails))]
        public virtual ICollection<MeetingParticipant> MeetingParticipants { get; set; }
        [InverseProperty(nameof(User.MemberDetails))]
        public virtual User? UserDetails { get; set; }
    }
}
