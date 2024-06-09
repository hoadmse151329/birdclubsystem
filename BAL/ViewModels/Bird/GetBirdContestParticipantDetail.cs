using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels.Bird
{
    public class GetBirdContestParticipantDetail
    {
        public GetBirdContestParticipantDetail()
        {
            Score = 0;
        }
        [DisplayName("Contest Id")]
        public int? ContestId { get; set; }
        [DisplayName("Contest Name")]
        public string? ContestName { get; set; }
        [Required(ErrorMessage = "Participant Full Name is required")]
        [DisplayName("Full Name")]
        public string? MemberName { get; set; }
        [Required(ErrorMessage = "Bird Id is required")]
        [DisplayName("Bird Id")]
        public int? BirdId { get; set; }
        [DisplayName("Bird Name")]
        public int? BirdName { get; set; }
        [DisplayName("Participant Recent Elo")]
        public int ParticipantElo { get; set; }
        [DisplayName("Participant Scored Elo")]
        public int? ContestElo { get; set; }
        [Range(0, 100, ErrorMessage = "Participant Score must be within range from 0 to 100 points")]
        [DisplayName("Participant Score")]
        public int? Score { get; set; }
        [DisplayName("Participant No")]
        public string? ParticipantNo { get; set; }
    }
}
