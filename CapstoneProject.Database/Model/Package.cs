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
    [Table("Package")]
    public class Package : BaseModel<BaseStatus>
    {
        [Column("carecenter_id")]
        public Guid? CareCenterId { get; set; }
        [Column("pet_type_id")]
        public Guid? PetTypeId { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("duration")]
        public string? Duration { get; set; }
        [Column("type")]
        public string? Type { get; set; }
        [Column("total_price")]
        public double? TotalPrice { get; set; }
        [Column("title")]
        public string? Title { get; set; }
        [Column("image")]
        public string? Image { get; set; }

        [ForeignKey("CareCenterId")]
        public CareCenter? CareCenter { get; set; }
        [ForeignKey("PetTypeId")]
        public PetType? PetType { get; set; }
        public virtual List<PackageItem> PackageItems { get; set; } = [];
        public virtual List<OrderDetail> OrderDetails { get; set; } = []; 
    }
}
