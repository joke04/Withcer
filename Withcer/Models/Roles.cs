using System.ComponentModel.DataAnnotations;

namespace Withcer.Models
{
    public class Roles
    {
        [Key]
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        
    }
}
