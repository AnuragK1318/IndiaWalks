using System.ComponentModel.DataAnnotations;

namespace IndiaWalks.UI.Models.Dto
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(1,ErrorMessage ="Code is required")]
        public string Code { get; set; }

        [Required(ErrorMessage ="Please Provide a valid Name")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Please provide a valid Area")]
        public double Area { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public long Population { get; set; }
    }
}
