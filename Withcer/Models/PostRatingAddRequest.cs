using System.ComponentModel.DataAnnotations;

namespace Withcer.Models
{
    public class PostRatingAddRequest
    {
        [Required]
        public int PostID { get; set; }
        [Required]
        public int Rating { get; set; }
    }
}
