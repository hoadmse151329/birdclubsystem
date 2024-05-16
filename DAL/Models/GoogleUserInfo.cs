using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    // GoogleUserInfo.cs
    public class GoogleUserInfo
    {
        [Key]
        [Column("userId")]
        public int UserId { get; set; }
        [Column("clubId")]
        public int? ClubId { get; set; }
        [Column("imagePath")]
        [StringLength(255)]
        public string? ImagePath { get; set; }
        [Column("memberId")]
        [StringLength(50)]
        public string? MemberId { get; set; }
        [Column("userName")]
        [StringLength(255)]
        public string? UserName { get; set; }
        [Column("password")]
        [StringLength(255)]
        public string? Password { get; set; }
        [Column("role")]
        [StringLength(50)]
        public string? Role { get; set; }
    }

}
