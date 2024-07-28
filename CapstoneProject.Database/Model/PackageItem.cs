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
    [Table("PackageItem")]
    public class PackageItem : BaseModel<BaseStatus>
    {
        [Column("package_id")]
        public Guid? PackageId { get; set; }
        [Column("service_id")]
        public Guid? ServiceId { get; set; }
        [Column("current_price")]
        public double CurrentPrice { get; set; }
        [Column("detail")]
        public string? Detail { get; set; } 

        [ForeignKey("PackageId")]
        public Package? Package { get; set; }
        [ForeignKey("ServiceId")]
        public Service? Service { get; set; }
    }
}
