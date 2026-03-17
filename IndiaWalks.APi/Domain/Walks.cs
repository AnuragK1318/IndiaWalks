using System.ComponentModel.DataAnnotations.Schema;

namespace IndiaWalks.APi.Domain
{
    public class Walks
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        public int RegionId { get; set; }
        public int DifficultyId { get; set; }

        //Navigation Properties
        public Region Region { get; set; }
        public Difficulty Difficulty { get; set; }

    }
}
