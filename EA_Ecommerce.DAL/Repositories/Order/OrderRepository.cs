using EA_Ecommerce.DAL.Data;
using EA_Ecommerce.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.DAL.Repositories.Order
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Models.Order?> CreateAsync(Models.Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }
        // user info from order
        public async Task<Models.Order?> GetUserByOrderAsync(int orderId)
        {
             return await _context.Orders.Include(o=>o.User).FirstOrDefaultAsync(o => o.Id == orderId);
        }
        
        public async Task<List<Models.Order>> GetAllOrdersByUserAsync(string UserId)
        {
            return await _context.Orders.Include(o => o.User).Where(o => o.UserId == UserId).OrderByDescending(o => o.OrderDate).ToListAsync();
        } 
        public async Task<List<Models.Order>> GetOrdersByStatusAsync(OrderStatusEnum Status)
        {
            return await _context.Orders.Where(o => o.OrderStatus == Status).OrderByDescending(o=>o.OrderDate).ToListAsync();
        }
        public async Task<bool> ChangeStatusAsync(int orderId, OrderStatusEnum newStatus)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                return false;
            }
            order.OrderStatus = newStatus;
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
