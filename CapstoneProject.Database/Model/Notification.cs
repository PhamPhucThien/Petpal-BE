using CapstoneProject.Database.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Database.Model
{
    [Table("Notification")]
    public class Notification : BaseModel
    {
        [Column("user_id")]
        public Guid? UserId { get; set; }
        [Column("content")]
        public string? Content { get; set; }
        [Column("is_read")]
        public bool? IsRead { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
