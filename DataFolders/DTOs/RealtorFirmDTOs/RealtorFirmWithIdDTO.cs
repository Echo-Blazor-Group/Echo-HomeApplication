using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataFolders.DTOs.RealtorFirmDTOs
{
    // Get class has id
    public class RealtorFirmWithIdDTO : RealtorFirmPostDTO
    {
        [Required]
        public int Id { get; set; }
    }
}
