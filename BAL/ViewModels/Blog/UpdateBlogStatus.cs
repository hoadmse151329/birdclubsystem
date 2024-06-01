using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BAL.ViewModels.Blog
{
    public class UpdateBlogStatus
    {
        public UpdateBlogStatus()
        {
            Status = "Draft";
            DefaultBlogStatusSelectList = new List<SelectListItem>() {
                new SelectListItem() { Text = "Status", Value = ""},
                new SelectListItem { Text = "Draft", Value = "Draft" },
                new SelectListItem { Text = "Active", Value = "Active" },
                new SelectListItem { Text = "Hidden", Value = "Hidden" },
                new SelectListItem { Text = "Archived", Value = "Archived" },
                new SelectListItem { Text = "Reported", Value = "Reported" },
                new SelectListItem { Text = "Disabled", Value = "Disabled" },
            };
        }

        public int? BlogId { get; set; }
        [Required(ErrorMessage = "Status is required")]
        [DisplayName("Status")]
        public string? Status { get; set; }
        public List<SelectListItem> DefaultBlogStatusSelectList { get; set; }
    }
}
