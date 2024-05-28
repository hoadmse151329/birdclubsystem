using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels.Event
{
    public class GetLeaderboardResponse
    {
        public int? Rank { get; set; }
        public int? BirdId { get; set; }
        public string? BirdName { get; set; }
        public int? Elo { get; set; }
    }
}
