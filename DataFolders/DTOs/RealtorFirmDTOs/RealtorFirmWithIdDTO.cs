using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataFolders.DTOs.RealtorFirmDTOs
{
    // Get class has id
    public class RealtorFirmWithIdDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required, DisplayName("About this firm")]
        public string RealtorFirmPresentation { get; set; } = string.Empty;
        public string? Logotype { get; set; } = string.Empty;
        public bool Active { get; set; }
    }
}
