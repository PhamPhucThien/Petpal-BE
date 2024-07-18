using CapstoneProject.DTO.Request.Order;
using CapstoneProject.DTO.Response.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Business.Interface
{
    public interface IOrderService
    {
        Task<CreateOrderResponse> CreateOrderRequest(CreateOrderRequest request);
    }
}