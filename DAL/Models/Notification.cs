using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    [Table("Notification")]
    public partial class Notification
    {
        [Key]
        [Column("notificationId")]
        [StringLength(255)]
        public string NotificationId { get; set; } = null!;
        [Column("title")]
        [StringLength(255)]
        public string? Title { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("date", TypeName = "datetime")]
        public DateTime Date { get; set; }
        [Column("userId")]
        public int UserId { get; set; }
        [Column("status")]
        [StringLength(50)]
        public string? Status { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(User.Notifications))]
        public virtual User UserDetail { get; set; } = null!;
    }
}
