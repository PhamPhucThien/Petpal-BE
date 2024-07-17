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
    [Table("Calendar")]
    public class Calendar : BaseModel<BaseStatus>
    {
        [Column("carecenter_id")]   
        public Guid? CareCenterId { get; set; }
        [Column("pet_amount_list")]
        public string? PetAmountList { get; set; }
        [Column("year")]
        public string? Year { get; set; }

        [ForeignKey("CareCenterId")]
        public CareCenter? CareCenter { get; set; }
    }
}
