using EA_Ecommerce.DAL.DTO.Responses.Order;
using EA_Ecommerce.DAL.Models;
using EA_Ecommerce.DAL.Repositories.Order;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.BLL.Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<DAL.Models.Order?> AddOrderAsync(DAL.Models.Order order)
        {
           return await _orderRepository.CreateAsync(order);
        }
        public async Task<bool> ChangeStatusAsync(int orderId, OrderStatusEnum newStatus)
        {
            return await _orderRepository.ChangeStatusAsync(orderId, newStatus);
        }

        public async Task<List<OrderResponseDTO>> GetAllOrdersByUserAsync(string UserId)
        {
            var result = await _orderRepository.GetAllOrdersByUserAsync(UserId);
            return result.Adapt<List<OrderResponseDTO>>();
        }

        public async Task<List<OrderResponseDTO>> GetOrdersByStatusAsync(OrderStatusEnum Status)
        {
           var Result = await _orderRepository.GetOrdersByStatusAsync(Status);
            return Result.Adapt<List<OrderResponseDTO>>();
        }

        public async Task<DAL.Models.Order?> GetUserByOrderAsync(int orderId)
        {
            return await _orderRepository.GetUserByOrderAsync(orderId);
        }
    } 
}
