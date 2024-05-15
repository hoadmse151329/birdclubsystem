using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels
{
    public class MeetingMediaViewModel
    {
        public int? PictureId { get; set; }
        public int? MeetingId { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [DisplayName("Description")]
        public string? Description { get; set; }
        [DisplayName("Image")]
        public string? Image { get; set; }
        [Required(ErrorMessage = "Type is required")]
        [DisplayName("Type")]
        public string? Type { get; set; }
    }
}
