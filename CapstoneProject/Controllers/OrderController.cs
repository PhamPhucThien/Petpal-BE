using CapstoneProject.Business.Interface;
using CapstoneProject.Business.Service;
using CapstoneProject.DTO.Request.Order;
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
                var response = await _orderService.CreateOrderRequest(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
