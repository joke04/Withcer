using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Withcer.Models
{
    public partial class User
    {
   

        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; } = null!;
        public string? Role { get; set; }

        public User User1 { get; set; }
        public User User2 { get; set; }
        public  ICollection<PostRatings> PostRatingsAsUser1 { get; set; }
        public  ICollection<PostRatings> PostRatingsAsUser2 { get; set; }

        
    }
}
