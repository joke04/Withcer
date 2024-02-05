using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Withcer.Models
{
    public partial class Sports
    {
        [Key]
        public int PostID { get; set; }
        public int SportsID { get; set; }   
    }
}
