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
    [Table("PetType")]
    public class PetType : BaseModel<BaseStatus>
    {
        [Column("type")]
        public string? Type { get; set; }
        [Column("category")]
        public string? Category { get; set; }
        [Column("description")]
        public string? Description { get; set; }
    }
}
