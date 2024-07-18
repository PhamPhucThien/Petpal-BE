using CapstoneProject.Business.Interface;
using CapstoneProject.DTO.Request.Order;
using CapstoneProject.DTO.Response.Order;
using CapstoneProject.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Business.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public Task<CreateOrderResponse> CreateOrderRequest(CreateOrderRequest request)
        {
            return null;
        }
    }
}