using IndiaWalks.APi.Domain;

namespace IndiaWalks.APi.DTOs
{
    public class WalksDto
    {
        public string Name { get; set; }
        public double Length { get; set; }
        public int RegionId { get; set; }
        public int DifficultyId { get; set; }

        //Navigation Properties
        public RegionDto Region { get; set; }
        public DifficultyDto Difficulty { get; set; }
    }
}
