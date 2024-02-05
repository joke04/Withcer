using System.ComponentModel.DataAnnotations;

namespace Withcer.Models
{
    public class Comments
    {
        [Key]
        public int CommentID { get; set; }
        public int PostID { get; set; }
        public int UserID { get; set; } 
        public string Text { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
