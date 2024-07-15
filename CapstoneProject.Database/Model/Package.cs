using CapstoneProject.Database.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Database.Model
{
    [Table("Package")]
    public class Package : BaseModel
    {
        [Column("carecenter_id")]
        public Guid? CareCenterId { get; set; }
        [Column("description")]
        public Guid? Description { get; set; }
        [Column("duration")]
        public string? Duration { get; set; }
        [Column("type")]
        public string? Type { get; set; }

        [ForeignKey("CareCenterId")]
        public CareCenter? CareCenter { get; set; }
    }
}
