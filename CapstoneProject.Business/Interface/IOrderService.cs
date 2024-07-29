using CapstoneProject.DTO;
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
        Task<ResponseObject<ApproveOrderResponse>> ApproveRequest(Guid orderId);
        Task<ResponseObject<CreateOrderResponse>> CreateOrderRequest(CreateOrderRequest request);
        Task<ResponseObject<string>> GetTransactionStatusVNPay();
        Task<ResponseObject<string>> PerformTransaction(Guid orderId);
        Task<ResponseObject<RejectOrderResponse>> RejectRequest(Guid orderId);
/*        Task<ResponseObject> GetByCareCenterId();
*/        Task<ResponseObject<GetListOrderResponse>> GetByUserId(Guid userId, GetListOrderById request);
    }
}