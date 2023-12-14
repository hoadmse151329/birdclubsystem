using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels
{
    public class BirdMediaViewModel
    {
        public int? PictureId { get; set; }
        public int? BirdId { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
    }
}
