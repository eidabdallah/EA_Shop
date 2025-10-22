using EA_Ecommerce.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.DAL.Repositories.Order
{
    public interface IOrderRepository
    {
        Task<DAL.Models.Order?> GetUserByOrderAsync(int orderId);
        Task<DAL.Models.Order?> CreateAsync(DAL.Models.Order order);
        Task<List<Models.Order>> GetOrdersByStatusAsync(OrderStatusEnum Status);
        Task<List<Models.Order>> GetAllOrdersByUserAsync(string UserId);
    }
}
