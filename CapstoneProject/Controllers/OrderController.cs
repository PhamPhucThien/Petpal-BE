using CapstoneProject.Business.Interface;
using CapstoneProject.Business.Service;
using CapstoneProject.DTO.Request.Order;
using CapstoneProject.Infrastructure.Extension;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

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
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
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
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
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
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
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
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("get-transaction-status-vnpay")]
        public async Task<IActionResult> GetTransactionStatusVNPay()
        {
            try
            {
                var response = await _orderService.GetTransactionStatusVNPay();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("get-order-request")]
        public async Task<IActionResult> GetOrderRequest(GetListOrderById request)
        {
            try
            {
                Guid userId = Guid.Parse(HttpContext.GetName());
                var response = await _orderService.GetByUserId(userId, request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
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
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
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
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
