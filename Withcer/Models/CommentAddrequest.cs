using System.ComponentModel.DataAnnotations;

namespace Withcer.Models
{
    public class CommentAddrequest
    {
        [Required]
        public int PostID { get; set; }
        [Required]
        public string Text { get; set; }
    
    }
}
