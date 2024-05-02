﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    [Keyless]
    [Table("Notification")]
    public partial class Notification
    {
        [Column("notificationId")]
        public int NotificationId { get; set; }
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
    }
}
