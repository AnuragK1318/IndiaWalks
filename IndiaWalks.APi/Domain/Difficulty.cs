using System.ComponentModel.DataAnnotations.Schema;

namespace IndiaWalks.APi.Domain
{
    public class Difficulty
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Code { get; set; }
    }


}
