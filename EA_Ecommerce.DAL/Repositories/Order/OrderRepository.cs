using EA_Ecommerce.DAL.Data;
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

        public async Task<Models.Order?> GetUserByOrderAsync(int orderId)
        {
             return await _context.Orders.Include(o=>o.User).FirstOrDefaultAsync(o => o.Id == orderId);
        }
    }
}
