using EA_Ecommerce.DAL.DTO.Requests.Cart;
using EA_Ecommerce.DAL.DTO.Responses.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.BLL.Services.Carts
{
    public interface ICartService
    {
        Task<bool> AddToCart (CartRequestDTO request , string UserId);
        Task<CartSummaryResponse> getCart(string UserId);
        Task<bool> ClearCartAsync(string UserId);
    }
}
