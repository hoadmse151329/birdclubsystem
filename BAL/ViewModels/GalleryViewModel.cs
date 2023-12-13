using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels
{
    public class GalleryViewModel
    {
        public int? PictureId { get; set; }
        public string? Description { get; set; }
        public int? UserId { get; set; }
        public string? Image { get; set; }
    }
}
