using CapstoneProject.Database.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Database.Model
{
    public class Blog : BaseModel
    {
        public Guid? UserId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Tags { get; set; }
        public int ViewNumber { get; set; }
        public int LikeNumber { get; set; }
        public string? ListImages { get; set; }

        public User? User { get; set; }
    }
}
