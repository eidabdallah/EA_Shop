using EA_Ecommerce.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.DAL.Repositories.OrderItem
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(List<Models.OrderItem> items)
        {
            await _context.OrderItems.AddRangeAsync(items);
            //await  _context.SaveChangesAsync();
        }
    }
}
