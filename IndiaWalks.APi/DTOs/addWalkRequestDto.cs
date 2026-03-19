using System.ComponentModel.DataAnnotations;

namespace IndiaWalks.APi.DTOs
{
    public class addWalkRequestDto
    {
        [Required]
        public string Name { get; set; }
        public double Length { get; set; }
        [Required]
        public int RegionId { get; set; }
        [Required]
        public int DifficultyId { get; set; }
    }
}
