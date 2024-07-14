using CapstoneProject.Database.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Database.Model
{
    public class CareCenterStaff : BaseModel
    {
        public Guid CareCenterId { get; set; }
        public Guid UserId { get; set; }

        public CareCenter? CareCenter { get; set; }
        public User? User { get; set; }
    }
}
