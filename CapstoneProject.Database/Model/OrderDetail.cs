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
    [Table("OrderDetail")]
    public class OrderDetail : BaseModel<BaseStatus>
    {
        [Column("order_id")]
        public Guid? OrderId { get; set; }
        [Column("pet_id")]
        public Guid? PetId { get; set; }
        [Column("package_id")]
        public Guid? PackageId { get; set; }
        [Column("from_date")]
        public DateTimeOffset FromDate { get; set; }
        [Column("to_date")]
        public DateTimeOffset ToDate { get; set; }
        [Column("detail")]
        public string? Detail { get; set; }
        [Column("ended_at")]
        public DateTimeOffset? EndedAt { get; set; }
        [Column("attendance_list")]
        public string? AttendanceList { get; set; }
        [Column("receive_time")]
        public TimeSpan? ReceiveTime { get; set; }
        [Column("return_time")]
        public TimeSpan? ReturnTime { get; set; }

        [ForeignKey("OrderId")]
        public Order? Order { get; set; }
        [ForeignKey("PetId")]
        public Pet? Pet { get; set; }
        [ForeignKey("PackageId")]
        public Package? Package { get; set; }
    }
}
