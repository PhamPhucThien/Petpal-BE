using CapstoneProject.Business.Interface;
using CapstoneProject.Database.Model;
using CapstoneProject.Database.Model.Meta;
using CapstoneProject.DTO;
using CapstoneProject.DTO.Request;
using CapstoneProject.DTO.Request.Order;
using CapstoneProject.DTO.Response.Orders;
using CapstoneProject.DTO.Response.Package;
using CapstoneProject.DTO.Response.Pet;
using CapstoneProject.Repository.Interface;
using System.Transactions;


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

            if (order != null)
            {
                order.Status = OrderStatus.APPROVED;
                bool check = await _orderRepository.EditAsync(order);
                data.IsSucceed = check;
                response.Payload.Data = data;
                if (data.IsSucceed)
                {
                    response.Status = StatusCode.OK;
                    response.Payload.Message = "Đơn hàng đã được xác nhận";
                }
                else
                {
                    response.Status = StatusCode.BadRequest;
                    response.Payload.Message = "Hiện tại không thể xác nhận đơn hàng";
                }
            }

            return response;
        }

        public async Task<ResponseObject<CountMoneyResponse>> CountMoney()
        {
            ResponseObject<CountMoneyResponse> response = new();
            CountMoneyResponse data = new();

            double money = await _orderRepository.CountMoney();
            data.Total = money;

            response.Status = StatusCode.OK;
            response.Payload.Message = "Lấy dữ liệu thành công";
            response.Payload.Data = data;

            return response;
        }

        public async Task<ResponseObject<CountOrderResponse>> CountOrder()
        {
            ResponseObject<CountOrderResponse> response = new();
            CountOrderResponse data = new();

            int count = await _orderRepository.Count();
            data.Count = count;

            response.Status = StatusCode.OK;
            response.Payload.Message = "Lấy dữ liệu thành công";
            response.Payload.Data = data;

            return response;
        }

        public async Task<ResponseObject<CreateOrderResponse>> CreateOrderRequest(Guid userId, CreateOrderRequest request)
        {
            ResponseObject<CreateOrderResponse> response = new();
            CreateOrderResponse isSucceed = new()
            {
                IsSucceed = false
            };
            response.Payload.Data = isSucceed;

            User? user = await _userRepository.GetByIdAsync(userId);
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

            using TransactionScope scope = new(TransactionScopeAsyncFlowOption.Enabled);

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

        public async Task<ResponseObject<GetListOrderResponse>> GetByUserId(Guid userId, GetListOrderById request)
        {
            ResponseObject<GetListOrderResponse> response = new();
            GetListOrderResponse data = new();

            User? user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
            {
                response.Status = StatusCode.BadRequest;
                response.Payload.Message = "Không thể tìm thấy người dùng";
            }
            else
            {
                List<Order>? list = [];

                Paging paging = new()
                {
                    Page = request.Page,
                    Size = request.Size,
                    MaxPage = 1
                };

                if (user.Role == UserRole.CUSTOMER)
                {
                    list = await _orderRepository.GetByUserId(userId, paging);
                }
                else if (user.Role == UserRole.MANAGER)
                {
                    list = await _orderRepository.GetByManagerId(userId, paging);
                }
                else if (user.Role == UserRole.PARTNER)
                {
                    list = await _orderRepository.GetByPartnerId(userId, paging);
                }

                if (list == null)
                {
                    response.Status = StatusCode.OK;
                    response.Payload.Message = "Hiện không có đơn hàng nào";
                }
                else
                {
                    response.Status = StatusCode.OK;
                    response.Payload.Message = "Lấy danh sách đơn hàng thành công";
                    List<OrderResponseModel> orders = [];

                    foreach (Order item in list)
                    {
                        OrderResponseModel model = new()
                        {
                            CurrentPrice = item.CurrentPrice,
                            Detail = item.Detail,
                            FromDate = (item.OrderDetail?.FromDate),
                            ToDate = (item.OrderDetail?.ToDate),
                            ReceiveTime = (item.OrderDetail?.ReceiveTime),
                            ReturnTime = (item.OrderDetail?.ReturnTime),
                            Status = item.Status,
                            Pet = new PetModel
                            {
                                Id = item.OrderDetail?.Pet?.Id,
                                FullName = item.OrderDetail?.Pet?.FullName,
                                ProfileImage = item.OrderDetail?.Pet?.ProfileImage,
                                Description = item.OrderDetail?.Pet?.Description
                            },
                            Package = new PackageResponseModel
                            {
                                Id = item.OrderDetail?.Package?.Id,
                                Description = item.OrderDetail?.Package?.Description,
                                Duration = item.OrderDetail?.Package?.Duration,
                                Type = item.OrderDetail?.Package?.Type,
                                TotalPrice = item.OrderDetail?.Package?.TotalPrice
                            }
                        };

                        orders.Add(model);
                    }

                    data.Orders = orders;
                    data.Paging = paging;
                    response.Payload.Data = data;
                }
            }

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