using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Range(1, 12, ErrorMessage = "Tổng số tuần đăng ký phải từ 1 đến 12 tuần.")]
        public int TotalWeek { get; set; }
        public TimeSpan ReceiveTime {  get; set; }
        public TimeSpan ReturnTime { get; set; }
    }
}