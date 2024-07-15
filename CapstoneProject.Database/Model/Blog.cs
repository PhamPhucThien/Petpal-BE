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
    [Table("Blog")]
    public class Blog : BaseModel<UserStatus>
    {
        [Column("user_id")]
        public Guid? UserId { get; set; }
        [Column("title")]
        public string? Title { get; set; }
        [Column("content")]
        public string? Content { get; set; }
        [Column("tags")]
        public string? Tags { get; set; }
        [Column("view_number")]
        public int ViewNumber { get; set; }
        [Column("like_number")]
        public int LikeNumber { get; set; }
        [Column("list_images")]
        public string? ListImages { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
