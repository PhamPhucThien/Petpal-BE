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
    [Table("Pet")]
    public class Pet : BaseModel<BaseStatus>
    {
        [Column("user_id")]
        public Guid? UserId { get; set; }
        [Column("pet_type_id")]
        public Guid? PetTypeId { get; set; }
        [Column("full_name")]
        public string? FullName { get; set; }
        [Column("profile_image")]
        public string? ProfileImage { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        
        [ForeignKey("UserId")]
        public User? User { get; set; }
        [ForeignKey("PetTypeId")]
        public PetType? PetType { get; set; }
    }
}
