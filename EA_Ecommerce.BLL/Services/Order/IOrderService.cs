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
    }
}
