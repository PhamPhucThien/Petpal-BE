using CapstoneProject.Database.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Database.Model
{
    [Table("Service")]
    public class Service : BaseModel
    {
        [Column("name")]
        public string? Name { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("base_price")]
        public double? BasePrice { get; set; }
        [Column("is_required")]
        public bool? IsRequired { get; set; }
    }
}
