using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class ContestScore
    {
        public int ScoreId { get; set; }
        public int? ContestId { get; set; }
        public int? BirdId { get; set; }
        public int? MemberId { get; set; }
        public decimal? Score { get; set; }
        public DateTime? ScoreDate { get; set; }
        public string? Comment { get; set; }
    }
}
