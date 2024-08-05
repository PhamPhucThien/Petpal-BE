﻿using CapstoneProject.Business.Interfaces;
using CapstoneProject.Database.Model;
using CapstoneProject.Database.Model.Meta;
using CapstoneProject.DTO;
using CapstoneProject.DTO.Request;
using CapstoneProject.DTO.Request.Order;
using CapstoneProject.DTO.Response.Orders;
using CapstoneProject.DTO.Response.Package;
using CapstoneProject.DTO.Response.Pet;
using CapstoneProject.Repository.Interface;
using Microsoft.IdentityModel.Protocols;
using System.Globalization;
using System.Text;
using System.Transactions;
using System.Web;
using static Google.Apis.Requests.BatchRequest;


namespace CapstoneProject.Business.Services
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

            double? money = await _orderRepository.CountMoney();
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
                            Id = item.Id,
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

        public async Task<ResponseObject<string>> GetTransactionStatusVNPay(Guid orderId, Guid userId, String urlReturn)
        {
            ResponseObject<string> response = new();
            Order? order = await _orderRepository.GetByIdAsync(orderId);

            if (order == null || order.UserId != userId)
            {
                response.Status = StatusCode.BadRequest;
                response.Payload.Message = "Không thể tìm thấy đơn hàng";
                return response;
            }

            string vnp_PayUrl = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
            string vnp_ReturnUrl = "/api/v1/vnpay/vnpay-payment";
            string vnp_TmnCode = "NCLDLDTA";
            string vnp_HashSecret = "J4VTRXS61APKTX5M834JUSMXX3DR501C";
            string vnp_apiUrl = "https://sandbox.vnpayment.vn/merchant_webapi/api/transaction";
            string vnp_Version = "2.1.0";
            string vnp_Command = "pay";
            string vnp_TxnRef = GetRandomNumber(8);
            string vnp_IpAddr = "127.0.0.1";
            string orderType = "250000";
            double? money = order.CurrentPrice * 100;

            string totalPrice = money.ToString();
            

            Dictionary<String, String> vnp_Params = new();
            vnp_Params.Add("vnp_Version", vnp_Version);
            vnp_Params.Add("vnp_Command", vnp_Command);
            vnp_Params.Add("vnp_TmnCode", vnp_TmnCode);
            vnp_Params.Add("vnp_Amount", totalPrice);
            vnp_Params.Add("vnp_CurrCode", "VND");

            vnp_Params.Add("vnp_TxnRef", vnp_TxnRef);
            vnp_Params.Add("vnp_OrderInfo", order.Id.ToString());
            vnp_Params.Add("vnp_OrderType", orderType);

            String locate = "vn";
            vnp_Params.Add("vnp_Locale", locate);

            urlReturn += vnp_ReturnUrl;
            vnp_Params.Add("vnp_ReturnUrl", urlReturn);
            vnp_Params.Add("vnp_IpAddr", vnp_IpAddr);


            var formatter = "yyyyMMddHHmmss";
            var now = DateTime.UtcNow.AddHours(7); // GMT+7
            var vnp_CreateDate = now.ToString(formatter, CultureInfo.InvariantCulture);
            vnp_Params["vnp_CreateDate"] = vnp_CreateDate;

            var expireTime = now.AddMinutes(15);
            var vnp_ExpireDate = expireTime.ToString(formatter, CultureInfo.InvariantCulture);
            vnp_Params["vnp_ExpireDate"] = vnp_ExpireDate;

            var fieldNames = vnp_Params.Keys.ToList();
            fieldNames.Sort();

            var hashData = new StringBuilder();
            var query = new StringBuilder();

            foreach (var fieldName in fieldNames)
            {
                var fieldValue = vnp_Params[fieldName];
                if (!string.IsNullOrEmpty(fieldValue))
                {
                    hashData.Append(fieldName)
                            .Append('=')
                            .Append(HttpUtility.UrlEncode(fieldValue, Encoding.ASCII));
                    query.Append(HttpUtility.UrlEncode(fieldName, Encoding.ASCII))
                         .Append('=')
                         .Append(HttpUtility.UrlEncode(fieldValue, Encoding.ASCII));
                    if (fieldNames.IndexOf(fieldName) != fieldNames.Count - 1)
                    {
                        query.Append('&');
                        hashData.Append('&');
                    }
                }
            }

            var queryUrl = query.ToString();
            var vnp_SecureHash = HmacSHA512(vnp_HashSecret, hashData.ToString());
            queryUrl += "&vnp_SecureHash=" + vnp_SecureHash;

            response.Status = StatusCode.OK;
            
            response.Payload.Message = "Tạo giao dịch thành công";
            response.Payload.Data = vnp_PayUrl + "?" + queryUrl;

            return response;
        }


        public static string HmacSHA512(string key, string data)
        {
            if (key == null || data == null)
            {
                throw new ArgumentNullException();
            }

            using (var hmac512 = new System.Security.Cryptography.HMACSHA512(Encoding.UTF8.GetBytes(key)))
            {
                byte[] hashValue = hmac512.ComputeHash(Encoding.UTF8.GetBytes(data));
                StringBuilder sb = new StringBuilder(2 * hashValue.Length);
                foreach (byte b in hashValue)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        /*public static string GetIpAddress(HttpRequest request)
        {
            string ipAddress = request.Headers["X-FORWARDED-FOR"];
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = request.HttpContext.Connection.LocalIpAddress.ToString();
            }
            return ipAddress;
        }*/

        public string GetRandomNumber(int len)
        {
            Random rnd = new Random();
            const string chars = "0123456789";
            StringBuilder sb = new StringBuilder(len);
            for (int i = 0; i < len; i++)
            {
                sb.Append(chars[rnd.Next(chars.Length)]);
            }
            return sb.ToString();
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