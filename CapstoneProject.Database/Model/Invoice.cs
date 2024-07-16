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
    [Table("Invoice")]
    public class Invoice : BaseModel<UserStatus>
    {
        [Column("order_id")]
        public Guid? OrderId { get; set; }
        [Column("payment_method")]
        public string? PaymentMethod { get; set; }
        [Column("amount")]
        public double Amount { get; set; }
        [Column("detail")]
        public string? Detail { get; set; }
        [Column("response_message")]
        public string? ResponseMessage { get; set; }

        [ForeignKey("OrderId")]
        public Order? Order { get; set; }
    }
}
