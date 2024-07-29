using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Request.Order
{
    public class GetListOrderRequestByCareCenterId
    {
        [Range(1, Int64.MaxValue)]
        public int Page { get; set; }
        [Range(1, 20)]
        public int Size { get; set; }
        [Required]
        public Guid CareCenterId { get; set; }
    }
}
