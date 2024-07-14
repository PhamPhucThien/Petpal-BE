using CapstoneProject.Database.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Database.Model
{
    [Table("Order")]
    public class Order : BaseModel
    {
        [Column("user_id")]
        public Guid? UserId { get; set; }
        [Column("current_price")]
        public double CurrentPrice { get; set; }
        [Column("detail")]
        public string? Detail { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
