using EA_Ecommerce.DAL.DTO.Responses.Order;
using EA_Ecommerce.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.BLL.Services.Order
{
    public interface IOrderService
    {
        Task<DAL.Models.Order?> AddOrderAsync(DAL.Models.Order order);
        Task<DAL.Models.Order?> GetUserByOrderAsync(int orderId);
        Task<List<OrderResponseDTO>> GetOrdersByStatusAsync(OrderStatusEnum Status);
        Task<List<OrderResponseDTO>> GetAllOrdersByUserAsync(string UserId);
        Task<bool> ChangeStatusAsync(int orderId, OrderStatusEnum newStatus);
    }
}
