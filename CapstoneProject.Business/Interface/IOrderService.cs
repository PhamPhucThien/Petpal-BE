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
        Task<bool> ApproveRequest(Guid orderId);
        Task<CreateOrderResponse> CreateOrderRequest(CreateOrderRequest request);
        Task<string> GetTransactionStatusVNPay();
        Task<string> PerformTransaction(Guid orderId);
        Task<bool> RejectRequest(Guid orderId);
    }
}