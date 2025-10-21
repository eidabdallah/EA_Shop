using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.DAL.Repositories.OrderItem
{
    public interface IOrderItemRepository
    {
        Task AddAsync(List<Models.OrderItem> items);
    }
}
