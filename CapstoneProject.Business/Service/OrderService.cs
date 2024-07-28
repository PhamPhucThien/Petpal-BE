using CapstoneProject.Business.Interface;
using CapstoneProject.Database.Model;
using CapstoneProject.DTO.Request.Order;
using CapstoneProject.DTO.Response.Orders;
using CapstoneProject.Repository.Interface;
using CapstoneProject.Database.Model.Meta;
using System.Transactions;
using CapstoneProject.DTO;


namespace CapstoneProject.Business.Service
{
    public class OrderService(IOrderRepository orderRepository, IUserRepository userRepository, IPetRepository petRepository, IPackageRepository packageRepository, IOrderDetailRepository orderDetailRepository) : IOrderService
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPetRepository _petRepository = petRepository;
        private readonly IPackageRepository _packageRepository = packageRepository;
        private readonly IOrderDetailRepository _orderDetailRepository = orderDetailRepository;
        public StatusCode StatusCode { get; set; } = new();
        public async Task<ResponseObject<ApproveOrderResponse>> ApproveRequest(Guid orderId)
        {
            ResponseObject<ApproveOrderResponse> response = new();
            ApproveOrderResponse data = new();
            Order? order = await _orderRepository.GetByIdAsync(orderId);
            
            if (order != null) {
                order.Status = OrderStatus.APPROVED;
                bool check = await _orderRepository.EditAsync(order);
                data.IsSucceed = check;
                response.Payload.Data = data;
                if (data.IsSucceed)
                {
                    response.Status = StatusCode.OK;
                    response.Payload.Message = "Đơn hàng đã được xác nhận";
                } else
                {
                    response.Status = StatusCode.BadRequest;
                    response.Payload.Message = "Hiện tại không thể xác nhận đơn hàng";
                }
            }

            return response;
        }

        public async Task<ResponseObject<CreateOrderResponse>> CreateOrderRequest(CreateOrderRequest request)
        {
            ResponseObject<CreateOrderResponse> response = new();
            CreateOrderResponse isSucceed = new() { 
                IsSucceed = false
            };
            response.Payload.Data = isSucceed;

            User? user = await _userRepository.GetByIdAsync(request.UserId);
            Pet? pet = await _petRepository.GetByIdAsync(request.PetId);
            Package? package = await _packageRepository.GetByIdAsync(request.PackageId);

            if (user == null) 
            {
                response.Status = StatusCode.NotFound;
                response.Payload.Message = "Không tìm thấy người dùng";
                return response;
            }
            if (pet == null)
            {
                response.Status = StatusCode.NotFound;
                response.Payload.Message = "Không tìm thấy thú cưng";
                return response;
            }
            if (package == null)
            {
                response.Status = StatusCode.NotFound;
                response.Payload.Message = "Không tìm thấy gói dịch vụ";
                return response;
            }

            Order order = new()
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                CurrentPrice = package.TotalPrice,
                Detail = request.Detail,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = user.Username
            };

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

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
                    isSucceed.IsSucceed = true;
                    response.Payload.Data = isSucceed;
                }
            }

            scope.Complete();

            return response;
        }

        public Task<ResponseObject<string>> GetTransactionStatusVNPay()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseObject<string>> PerformTransaction(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseObject<RejectOrderResponse>> RejectRequest(Guid orderId)
        {
            ResponseObject<RejectOrderResponse> response = new();
            RejectOrderResponse data = new();
            Order? order = await _orderRepository.GetByIdAsync(orderId);

            if (order != null)
            {
                order.Status = OrderStatus.REJECTED;
                bool check = await _orderRepository.EditAsync(order);
                data.IsSucceed = check;
                response.Payload.Data = data;
                if (data.IsSucceed)
                {
                    response.Status = StatusCode.OK;
                    response.Payload.Message = "Đơn hàng đã được từ chối";
                }
                else
                {
                    response.Status = StatusCode.BadRequest;
                    response.Payload.Message = "Hiện tại không thể từ chối đơn hàng";
                }
            }

            return response;
        }
    }
}