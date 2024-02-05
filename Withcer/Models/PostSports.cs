using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Withcer.Models
{
    public partial class PostSports
    {
        [Key]
        public int PostID { get; set; }
        public int SportID { get; set; }
    }
}
