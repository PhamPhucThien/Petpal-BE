using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Request.Order
{
    public class VNPAYRequest
    {
        public string? VnpSecureHash { get; set; }
        public string? VnpOrderInfo { get; set; }
        public string? VnpAmount { get; set; }
        public string? VnpTransactionNo { get; set; }
        public string? VnpCardType { get; set; }
        public string? VnpTransactionStatus { get; set; }
    }
}
