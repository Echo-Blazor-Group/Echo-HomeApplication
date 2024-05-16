namespace Echo_HemAPI.Data.Models.DTOs.PictureDtos
{

    //Author Gustaf
    //For calling and creating new pictures
    public class PictureDto
    {
        public int Id { get; set; }
        public string PictureUrl { get; set; } = string.Empty;
        public int? EstateId { get; set; }
    }
    public class InsertPictureDto
    {
        public string PictureUrl { get; set; } = string.Empty;
        public int? EstateId { get; set; }
    }
    //For updating pictures via Dto mapping.
    public class UpdatePictureDto
    {
        public int Id { get; set; }
        public string PictureUrl { get; set; } = string.Empty;
        public int? EstateId { get; set; }
    }
}
