using System.ComponentModel.DataAnnotations;

namespace Withcer.Models
{
    public class UserRegistrationRequest
    {
        [Key]
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = null!;
        public string Role{ get; set; }
    }
}
