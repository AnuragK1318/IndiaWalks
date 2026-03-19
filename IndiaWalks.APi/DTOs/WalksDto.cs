using IndiaWalks.APi.Domain;
using System.ComponentModel.DataAnnotations;

namespace IndiaWalks.APi.DTOs
{
    public class WalksDto
    {
        [Required]
        public string Name { get; set; }
        public double Length { get; set; }
        [Required]
        public int RegionId { get; set; }
        [Required]
        public int DifficultyId { get; set; }

        //Navigation Properties
        public RegionDto Region { get; set; }
        public DifficultyDto Difficulty { get; set; }
    }
}
