using System.ComponentModel.DataAnnotations;

namespace IndiaWalks.APi.DTOs
{
    public class UpdateRegionRequestDto
    {
        [Required]
        [MinLength(1,ErrorMessage ="Please provide a valid Code ")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Please provide a valid Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please provide a valid Area")]
        public double Area { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public long Population { get; set; }
    }
}
