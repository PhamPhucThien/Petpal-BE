using CapstoneProject.Business;
using CapstoneProject.Business.Interfaces;
using CapstoneProject.Business.Services;
using CapstoneProject.Database.Model;
using CapstoneProject.DTO;
using CapstoneProject.DTO.Request.Order;
using CapstoneProject.Infrastructure.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public new StatusCode StatusCode { get; set; } = new();

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("create-order-request")]
        public async Task<IActionResult> CreateOrderRequest(CreateOrderRequest request)
        {
            try
            {
                Guid userId = Guid.Parse(HttpContext.GetName());
                var response = await _orderService.CreateOrderRequest(userId, request);
                return Ok(response);
            }
            catch (FormatException)
            {
                return Unauthorized(new ResponseObject<string>()
                {
                    Payload = new Payload<string>(string.Empty, "Bạn chưa đăng nhập"),
                    Status = StatusCode.Unauthorized
                });
            }
            catch (Exception)
            {
                return BadRequest(new ResponseObject<string>()
                {
                    Payload = new Payload<string>(string.Empty, "Lỗi hệ thống"),
                    Status = StatusCode.BadRequest
                });
            }
        }

        [HttpPost("approve-request")]
        public async Task<IActionResult> ApproveRequest(Guid orderId)
        {
            try
            {
                var response = await _orderService.ApproveRequest(orderId);
                return Ok(response);
            }
            catch (FormatException)
            {
                return Unauthorized(new ResponseObject<string>()
                {
                    Payload = new Payload<string>(string.Empty, "Bạn chưa đăng nhập"),
                    Status = StatusCode.Unauthorized
                });
            }
            catch (Exception)
            {
                return BadRequest(new ResponseObject<string>()
                {
                    Payload = new Payload<string>(string.Empty, "Lỗi hệ thống"),
                    Status = StatusCode.BadRequest
                });
            }
        }

        [HttpPost("reject-request")]
        public async Task<IActionResult> RejectRequest(Guid orderId)
        {
            try
            {
                var response = await _orderService.RejectRequest(orderId);
                return Ok(response);
            }
            catch (FormatException)
            {
                return Unauthorized(new ResponseObject<string>()
                {
                    Payload = new Payload<string>(string.Empty, "Bạn chưa đăng nhập"),
                    Status = StatusCode.Unauthorized
                });
            }
            catch (Exception)
            {
                return BadRequest(new ResponseObject<string>()
                {
                    Payload = new Payload<string>(string.Empty, "Lỗi hệ thống"),
                    Status = StatusCode.BadRequest
                });
            }
        }

        [HttpPost("perform-transaction")]
        public async Task<IActionResult> PerformTransaction(Guid orderId)
        {
            try
            {
                var response = await _orderService.PerformTransaction(orderId);
                return Ok(response);
            }
            catch (FormatException)
            {
                return Unauthorized(new ResponseObject<string>()
                {
                    Payload = new Payload<string>(string.Empty, "Bạn chưa đăng nhập"),
                    Status = StatusCode.Unauthorized
                });
            }
            catch (Exception)
            {
                return BadRequest(new ResponseObject<string>()
                {
                    Payload = new Payload<string>(string.Empty, "Lỗi hệ thống"),
                    Status = StatusCode.BadRequest
                });
            }
        }

        [HttpGet("get-transaction-status-vnpay")]
        [Authorize(Roles = "CUSTOMER")]
        public async Task<IActionResult> GetTransactionStatusVNPay([FromQuery] Guid orderId)
        {
            try
            {
                Guid userId = Guid.Parse(HttpContext.GetName());
                var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
                var response = await _orderService.GetTransactionStatusVNPay(orderId, userId, baseUrl);
                return Ok(response);
            }
            catch (FormatException)
            {
                return Unauthorized(new ResponseObject<string>()
                {
                    Payload = new Payload<string>(string.Empty, "Bạn chưa đăng nhập"),
                    Status = StatusCode.Unauthorized
                });
            }
            catch (Exception)
            {
                return BadRequest(new ResponseObject<string>()
                {
                    Payload = new Payload<string>(string.Empty, "Lỗi hệ thống"),
                    Status = StatusCode.BadRequest
                });
            }
        }

        [HttpGet("get-order/{orderId}")]
        public async Task<IActionResult> GetOrderById(Guid orderId)
        {
            try
            {
                Guid userId = Guid.Parse(HttpContext.GetName());
                var response = await _orderService.GetById(orderId, userId);
                return Ok(response);
            }
            catch (FormatException)
            {
                return Unauthorized(new ResponseObject<string>()
                {
                    Payload = new Payload<string>(string.Empty, "Bạn chưa đăng nhập"),
                    Status = StatusCode.Unauthorized
                });
            }
            catch (Exception)
            {
                return BadRequest(new ResponseObject<string>()
                {
                    Payload = new Payload<string>(string.Empty, "Lỗi hệ thống"),
                    Status = StatusCode.BadRequest
                });
            }
        }

        [HttpGet("vnpay-payment")]
        public async Task<IActionResult> VNPAYPayment()
        {
            try
            {
                VNPAYRequest request = new()
                {
                    VnpSecureHash = Request.Query["vnp_SecureHash"],
                    VnpOrderInfo = Request.Query["vnp_OrderInfo"],
                    VnpAmount = Request.Query["vnp_Amount"],
                    VnpTransactionNo = Request.Query["vnp_TransactionNo"],
                    VnpCardType = Request.Query["vnp_CardType"],
                    VnpTransactionStatus = Request.Query["vnp_TransactionStatus"],
                    VnpBankCode = Request.Query["vnp_BankCode"],
                    VnpBankTranNo = Request.Query["vnp_BankTranNo"],
                    VnpTxnRef = Request.Query["vnp_TxnRef"],
                    VnpPayDate = Request.Query["vnp_PayDate"],
                    VnpResponseCode = Request.Query["vnp_ResponseCode"],
                    VnpTmnCode = Request.Query["vnp_TmnCode"]
                };

                var paymentStatus = await _orderService.VNPAYPayment(request);

                if (paymentStatus.IsSucceed && paymentStatus.Text != null)
                {
                    Redirect(paymentStatus.Text);
                }

                return Ok(paymentStatus);
            }
            catch (FormatException)
            {
                return Unauthorized(new ResponseObject<string>()
                {
                    Payload = new Payload<string>(string.Empty, "Bạn chưa đăng nhập"),
                    Status = StatusCode.Unauthorized
                });
            }
            catch (Exception)
            {
                return BadRequest(new ResponseObject<string>()
                {
                    Payload = new Payload<string>(string.Empty, "Lỗi hệ thống"),
                    Status = StatusCode.BadRequest
                });
            }
        }

        [HttpPost("get-order-request")]
        [Authorize(Roles = "PARTNER,MANAGER,CUSTOMER")]
        public async Task<IActionResult> GetOrderRequest(GetListOrderById request)
        {
            try
            {
                Guid userId = Guid.Parse(HttpContext.GetName());
                var response = await _orderService.GetByUserId(userId, request);
                return Ok(response);
            }
            catch (FormatException)
            {
                return Unauthorized(new ResponseObject<string>()
                {
                    Payload = new Payload<string>(string.Empty, "Bạn chưa đăng nhập"),
                    Status = StatusCode.Unauthorized
                });
            }
            catch (Exception)
            {
                return BadRequest(new ResponseObject<string>()
                {
                    Payload = new Payload<string>(string.Empty, "Lỗi hệ thống"),
                    Status = StatusCode.BadRequest
                });
            }
        }

        [HttpPost("get-pending-request")]
        [Authorize(Roles = "MANAGER")]
        public async Task<IActionResult> GetPedingRequest(GetListOrderById request)
        {
            try
            {
                Guid userId = Guid.Parse(HttpContext.GetName());
                var response = await _orderService.GetPendingByUserId(userId, request);
                return Ok(response);
            }
            catch (FormatException)
            {
                return Unauthorized(new ResponseObject<string>()
                {
                    Payload = new Payload<string>(string.Empty, "Bạn chưa đăng nhập"),
                    Status = StatusCode.Unauthorized
                });
            }
            catch (Exception)
            {
                return BadRequest(new ResponseObject<string>()
                {
                    Payload = new Payload<string>(string.Empty, "Lỗi hệ thống"),
                    Status = StatusCode.BadRequest
                });
            }
        }

        [HttpGet("count-order")]
        public async Task<IActionResult> CountOrder()
        {
            try
            {
                var response = await _orderService.CountOrder();
                return Ok(response);
            }
            catch (FormatException)
            {
                return Unauthorized(new ResponseObject<string>()
                {
                    Payload = new Payload<string>(string.Empty, "Bạn chưa đăng nhập"),
                    Status = StatusCode.Unauthorized
                });
            }
            catch (Exception)
            {
                return BadRequest(new ResponseObject<string>()
                {
                    Payload = new Payload<string>(string.Empty, "Lỗi hệ thống"),
                    Status = StatusCode.BadRequest
                });
            }
        }

        [HttpGet("count-money")]
        public async Task<IActionResult> CountMoney()
        {
            try
            {
                var response = await _orderService.CountMoney();
                return Ok(response);
            }
            catch (FormatException)
            {
                return Unauthorized(new ResponseObject<string>()
                {
                    Payload = new Payload<string>(string.Empty, "Bạn chưa đăng nhập"),
                    Status = StatusCode.Unauthorized
                });
            }
            catch (Exception)
            {
                return BadRequest(new ResponseObject<string>()
                {
                    Payload = new Payload<string>(string.Empty, "Lỗi hệ thống"),
                    Status = StatusCode.BadRequest
                });
            }
        }
    }
}
