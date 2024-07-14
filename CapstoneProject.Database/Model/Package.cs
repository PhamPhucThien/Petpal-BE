using CapstoneProject.Database.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Database.Model
{
    public class Package : BaseModel
    {
        public Guid? CareCenterId { get; set; }
        public Guid? Description { get; set; }
        public string? Duration { get; set; }
        public string? Type { get; set; }

        public CareCenter? CareCenter { get; set; }
    }
}
