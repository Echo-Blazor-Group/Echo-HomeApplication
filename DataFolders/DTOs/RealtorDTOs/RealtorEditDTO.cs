using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.RealtorDTOs
{
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
    }
}
