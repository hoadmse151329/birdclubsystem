using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    [Table("Feedback")]
    public partial class Feedback
    {
        [Key]
        [Column("feedbackId")]
        public int FeedbackId { get; set; }
        [Column("userId")]
        public int UserId { get; set; }
        [Column("eventId")]
        [StringLength(5)]
        public string EventId { get; set; } = null!;
        [Column("title")]
        [StringLength(255)]
        public string Title { get; set; } = null!;
        [Column("details")]
        public string? Details { get; set; }
        [Column("date", TypeName = "datetime")]
        public DateTime? Date { get; set; }
        [Column("category")]
        [StringLength(50)]
        public string? Category { get; set; }
        [Column("rating", TypeName = "decimal(5, 2)")]
        public decimal? Rating { get; set; }
        [Column("status")]
        [StringLength(20)]
        public string? Status { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(User.Feedbacks))]
        public virtual User UserDetail { get; set; } = null!;
    }
}
