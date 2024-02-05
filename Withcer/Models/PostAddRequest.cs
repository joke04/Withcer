using System.ComponentModel.DataAnnotations;

namespace Withcer.Models
{
    public class PostAddRequest
    {
        [Key]
        public string PostName { get; set; }
        public string Content { get; set; } = null!;
        
    }
}
