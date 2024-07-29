using CapstoneProject.Database.Model.Base;
using CapstoneProject.Database.Model.Meta;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Database.Model
{
    [Table("User")]
    public class User : BaseModel<UserStatus>
    {
        [Column("username")]
        public string? Username { get; set; }
        [Column("password")]    
        public string? Password { get; set; }
        [Column("full_name")]
        public required string FullName { get; set; }
        [Column("address")]
        public string? Address { get; set; }
        [Column("room_id")]
        public string? RoomId { get; set; }
        [Column("phone_number")]
        public string? PhoneNumber { get; set; }
        [Column("profile_image")]
        public string? ProfileImage { get; set; }
        [Column("email")]
        public string? Email { get; set; }
        [Column("role", TypeName = "nvarchar(30)")]
        public UserRole Role { get; set; }

        public virtual List<Pet>? Pets { get; set; }
    }
}
