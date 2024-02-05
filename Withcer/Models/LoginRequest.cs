using System.ComponentModel.DataAnnotations;

namespace Withcer.Models
{
    public class LoginRequest
    {
        [Key]
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
