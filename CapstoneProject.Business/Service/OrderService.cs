using CapstoneProject.Business.Interface;
using CapstoneProject.Database.Model;
using CapstoneProject.DTO.Request.Order;
using CapstoneProject.DTO.Response.Orders;
using CapstoneProject.Repository.Interface;
using CapstoneProject.Database.Model.Meta;


namespace CapstoneProject.Business.Service
{
    public class OrderService(IOrderRepository orderRepository, IUserRepository userRepository, IPetRepository petRepository, IPackageRepository packageRepository, IOrderDetailRepository orderDetailRepository) : IOrderService
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPetRepository _petRepository = petRepository;
        private readonly IPackageRepository _packageRepository = packageRepository;
        private readonly IOrderDetailRepository _orderDetailRepository = orderDetailRepository;

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
            CreateOrderResponse response = new() { 
                IsSucceed = false
            };

            User? user = await _userRepository.GetByIdAsync(request.UserId);
            Pet? pet = await _petRepository.GetByIdAsync(request.PetId);
            Package? package = await _packageRepository.GetByIdAsync(request.PackageId);

            if (user == null) return response;
            if (pet == null) return response;   
            if (package == null) return response;

            Order order = new()
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                CurrentPrice = package.TotalPrice,
                Detail = request.Detail,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = user.Username
            };

            Order? existedOrder = await _orderRepository.AddAsync(order);

            if (existedOrder != null)
            {
                OrderDetail detail = new()
                {
                    Id = Guid.NewGuid(),
                    OrderId = existedOrder.Id,
                    PetId = request.PetId,
                    FromDate = request.FromDate,
                    ToDate = request.ToDate,
                    ReceiveTime = request.ReceiveTime,
                    ReturnTime = request.ReturnTime,
                    PackageId = request.PackageId,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = user.Username
                };

                OrderDetail? existedOrderDetail = await _orderDetailRepository.AddAsync(detail);

                if (existedOrderDetail != null)
                {
                    response.IsSucceed = true;
                }
            }

            return response;
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