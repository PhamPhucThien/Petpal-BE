using CapstoneProject.Database.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Database.Model
{
    public class OrderDetail : BaseModel
    {
        public Guid? OrderId { get; set; }
        public Guid? PetId { get; set; }
        public Guid? PackageId { get; set; }
        public DateTimeOffset FromDate { get; set; }
        public DateTimeOffset ToDate { get; set; }
        public string? Detail { get; set; }
        public DateTimeOffset? EndedAt { get; set; }
        public string? AttendanceList { get; set; }
        public TimeSpan? ReceiveTime { get; set; }
        public TimeSpan? ReturnTime { get; set; }

        public Order? Order { get; set; }
        public Pet? Pet { get; set; }
        public Package? Package { get; set; }
    }
}
