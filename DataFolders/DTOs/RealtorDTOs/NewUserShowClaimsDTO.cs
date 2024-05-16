using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.RealtorDTOs
{
    public class NewUserShowClaimsDTO
    {
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public int RealtorFirmId { get; set; }
        public string? Role { get; set; }
        public string? Token { get; set; }
    }

}
