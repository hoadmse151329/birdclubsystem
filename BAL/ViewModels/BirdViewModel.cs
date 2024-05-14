﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels
{
    public class BirdViewModel
    {
        public BirdViewModel()
        {
            AddDate = DateTime.Now;
            Status = "Active";
            Elo = 1500;
        }
        public int? BirdId { get; set; }
        public int? MemberId { get; set; }
        [Required(ErrorMessage = "Bird Name is required")]
        [DisplayName("Bird Name")]
        public string? BirdName { get; set; }
        [Required(ErrorMessage = "ELO is required")]
        [DisplayName("ELO")]
        public int? Elo { get; set; }
        [Required(ErrorMessage = "Age is required")]
        [DisplayName("Age")]
        public int? Age { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [DisplayName("Description")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Color is required")]
        [DisplayName("Color")]
        public string? Color { get; set; }
        [Required(ErrorMessage = "Date Added is required")]
        [DisplayName("Date Added")]
        public DateTime AddDate { get; set; }
        [DisplayName("Profile Picture")]
        public BirdMediaViewModel? ProfilePic { get; set; }
        [DisplayName("Status")]
        public string? Status { get; set; }
        [Required(ErrorMessage = "Origin is required")]
        [DisplayName("Origin")]
        public string? Origin { get; set; }


    }
}
