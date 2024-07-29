using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Request.Order
{
    public class CreateOrderRequest
    {
        public Guid PetId { get; set; }
        public Guid PackageId { get; set; }
        public string? Detail {  get; set; }
        public DateTimeOffset FromDate { get; set; }
        public DateTimeOffset ToDate { get; set; }
        public TimeSpan ReceiveTime {  get; set; }
        public TimeSpan ReturnTime { get; set; }
    }
}