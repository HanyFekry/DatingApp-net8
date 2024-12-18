using System.ComponentModel.DataAnnotations;

namespace DatingApi.Dtos
{
    public class RegisterDto
    {
        [Required]
        [StringLength(10, MinimumLength = 3)]
        public string UserName { get; set; } = default!;
        [Required]
        [StringLength(10, MinimumLength = 8)]
        public string Password { get; set; } = default!;
    }
}
