using System.ComponentModel.DataAnnotations;

namespace Withcer.Models
{
    public class CommentEditRequest
    {

        [Required]
        public string Text { get; set; }
    }
}
