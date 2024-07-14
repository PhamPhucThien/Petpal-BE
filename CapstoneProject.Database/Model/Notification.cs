using CapstoneProject.Database.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Database.Model
{
    public class Notification : BaseModel
    {
        public Guid? UserId { get; set; }
        public string? Content { get; set; }
        public bool? IsRead { get; set; }

        public User? User { get; set; }
    }
}
