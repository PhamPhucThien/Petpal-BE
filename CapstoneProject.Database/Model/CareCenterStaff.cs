using CapstoneProject.Database.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Database.Model
{
    [Table("CareCenterStaff")]
    public class CareCenterStaff : BaseModel
    {
        [Column("carecenter_id")]
        public Guid CareCenterId { get; set; }
        [Column("user_ID")]
        public Guid UserId { get; set; }

        [ForeignKey("CareCenterId")]
        public CareCenter? CareCenter { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
