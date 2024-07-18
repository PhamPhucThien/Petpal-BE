using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Request.Order
{
    public class CreateOrderRequest
    {
        public Guid UserId { get; set; }
        public Guid PetId { get; set; }
        public Guid PackageId { get; set; }
    }
}