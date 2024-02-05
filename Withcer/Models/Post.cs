using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Withcer.Models
{
    public partial class Post
    {
        [Key]
        public int PostId { get; set; }
        public string PostName { get; set; }
        public int? UserId { get; set; }
        public string Text { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }


    }
}
