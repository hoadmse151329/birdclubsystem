using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public partial class ContestScore
    {
        public int scoreId { get; set; }
        public int? contestId { get; set; }
        public int? birdId { get; set; }
        public int? memberId { get; set; }
        public double? score { get; set; }
        public DateTime? scoreDate { get; set; }
        public string? comment { get; set; }
    }
}
