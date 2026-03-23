using System.ComponentModel.DataAnnotations;

namespace IndiaWalks.APi.DTOs
{
    public class LoginReqDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
