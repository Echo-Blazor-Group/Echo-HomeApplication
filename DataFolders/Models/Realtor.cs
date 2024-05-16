using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    //Author Seb
    public class Realtor
    {
        public Realtor()
        {

        }
        public string Id { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [DisplayName("Realtor Firm")]
        public RealtorFirm? RealtorFirm { get; set; }

        [DisplayName("Profile Picture")]
        public string? ProfilePicture { get; set; }



    }
}
