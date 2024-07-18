using CapstoneProject.Business.Interface;
using CapstoneProject.Database.Model;
using CapstoneProject.DTO.Request.Order;
using CapstoneProject.DTO.Response.Orders;
using CapstoneProject.Repository.Interface;
using CapstoneProject.Database.Model.Meta;


namespace CapstoneProject.Business.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<bool> ApproveRequest(Guid orderId)
        {
            Order? order = await _orderRepository.GetByIdAsync(orderId);
            
            if (order != null) {
                order.Status = OrderStatus.APPROVED;
                return await _orderRepository.EditAsync(order);
            }
            return false;
        }

        public async Task<CreateOrderResponse> CreateOrderRequest(CreateOrderRequest request)
        {
            CreateOrderResponse response = new CreateOrderResponse();
            return null;
        }

        public Task<string> GetTransactionStatusVNPay()
        {
            throw new NotImplementedException();
        }

        public Task<string> PerformTransaction(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RejectRequest(Guid orderId)
        {
            Order? order = await _orderRepository.GetByIdAsync(orderId);

            if (order != null)
            {
                order.Status = OrderStatus.REJECTED;
                return await _orderRepository.EditAsync(order);
            }
            return false;
        }
    }
}