using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Withcer.Models
{
    public partial class PostRatings
    {
        [Key]
        public int RatingID { get; set; }
        public int PostID { get; set; }
        public int UserID { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public User User1 { get; set; }
        public User User2 { get; set; }
        public User User1ID { get; set;}
        public User Userb2ID { get;  set; }
    }
}
