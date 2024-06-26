﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.RealtorDTOs
{
    //Author: Seb
    public class RealtorEditDTO
    {
       
        public string FirstName { get; set; } = string.Empty;

        
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
        public string ProfilePicture { get; set; } = "https://shorturl.at/CJOR3";
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
        [Required]
        [DisplayName("Active")]
        public bool IsActive { get; set; } = false;
    }
}
