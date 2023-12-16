using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DAL.Models
{
    public partial class BirdMedia
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PictureId { get; set; }
        public int? BirdId { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        [JsonIgnore]
        public virtual Bird? Bird { get; set; }
    }
}
