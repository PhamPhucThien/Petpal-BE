using CapstoneProject.DTO;
using CapstoneProject.DTO.Request.Order;
using CapstoneProject.DTO.Response.Orders;

namespace CapstoneProject.Business.Interfaces
{
    public interface IOrderService
    {
        Task<ResponseObject<ApproveOrderResponse>> ApproveRequest(Guid orderId);
        Task<ResponseObject<CreateOrderResponse>> CreateOrderRequest(Guid userId, CreateOrderRequest request);
        Task<ResponseObject<string>> GetTransactionStatusVNPay(Guid orderId, Guid userId, String urlReturn);
        Task<ResponseObject<string>> PerformTransaction(Guid orderId);
        Task<ResponseObject<RejectOrderResponse>> RejectRequest(Guid orderId);
        /*        Task<ResponseObject> GetByCareCenterId();
        */
        Task<ResponseObject<GetListOrderResponse>> GetByUserId(Guid userId, GetListOrderById request);
        Task<ResponseObject<CountOrderResponse>> CountOrder();
        Task<ResponseObject<CountMoneyResponse>> CountMoney();
    }
}