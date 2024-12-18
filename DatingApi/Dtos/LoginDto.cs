using System.ComponentModel.DataAnnotations;

namespace DatingApi.Dtos
{
    public class LoginDto
    {
        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public required string UserName { get; set; }
        [Required]
        [MaxLength(10)]
        [MinLength(7)]
        public required string Password { get; set; }
    }
}
