using CapstoneProject.Database.Model;
using CapstoneProject.Database.Model.Meta;
using CapstoneProject.DTO.Request;
using CapstoneProject.DTO.Response.Package;
using CapstoneProject.DTO.Response.Pet;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Response.Orders
{
    public class GetListOrderResponse
    {
        public List<OrderResponseModel> Orders { get; set; } = [];
        public Paging Paging { get; set; } = new();
    }

    public class OrderResponseModel
    {
        public Guid Id { get; set; }
        public PetModel? Pet { get; set; }
        public PackageResponseModel? Package { get; set; }
        public double? CurrentPrice { get; set; }
        public string? Detail { get; set; }
        public DateTimeOffset? FromDate { get; set; }
        public DateTimeOffset? ToDate { get; set; }
        public TimeSpan? ReceiveTime { get; set; }
        public TimeSpan? ReturnTime { get; set; }
        public OrderStatus Status { get; set; }
    }
}
