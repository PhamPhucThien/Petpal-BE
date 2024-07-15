using CapstoneProject.Database.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Database.Model
{
    [Table("Comment")]
    public class Comment : BaseModel
    {
        [Column("related_id")]
        public Guid? RelatedId { get; set; }
        [Column("comment_id")]
        public Guid? CommentId { get; set; }
        [Column("user_id")]
        public Guid? UserId { get; set; }
        [Column("content")]
        public string? Content { get; set; }
        [Column("like_number")]
        public int LikeNumber { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
        [ForeignKey("RelatedId")]
        public Comment? ParentComment { get; set; }
    }
}
