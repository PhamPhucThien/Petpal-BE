using CapstoneProject.Database.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Database.Model
{
    public class Comment : BaseModel
    {
        public Guid? RelatedId { get; set; }
        public Guid? CommentId { get; set; }
        public Guid? UserId { get; set; }
        public string? Content { get; set; }
        public int LikeNumber { get; set; }

        public User? User { get; set; }
        public Comment? ParentComment { get; set; }
    }
}
