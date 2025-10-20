using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.DAL.DTO.Responses.Cart
{
    public class CartSummaryResponse
    {
        public List<CartResponseDTO> Items { get; set; } = new List<CartResponseDTO>();
        public decimal TotalAmount => Items.Sum(i => i.TotalPrice);
    }
}
