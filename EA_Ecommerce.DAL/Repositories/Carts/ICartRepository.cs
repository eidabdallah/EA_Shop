using EA_Ecommerce.DAL.Models;
using EA_Ecommerce.DAL.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.DAL.Repositories.Carts
{
    public interface ICartRepository {
        Task<int> AddAsync(Cart cart);
        Task<List<Cart>> GetUserCartAsync(string UserId);
        Task<bool> ClearCartAsync(string UserId);
    } 
}
