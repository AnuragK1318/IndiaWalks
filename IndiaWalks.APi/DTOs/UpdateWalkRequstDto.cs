namespace IndiaWalks.APi.DTOs
{
    public class UpdateWalkRequstDto
    {
        public string Name { get; set; }
        public double Length { get; set; }
        public int RegionId { get; set; }
        public int DifficultyId { get; set; }
    }
}
