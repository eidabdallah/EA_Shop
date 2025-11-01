using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.DAL.DTO.Requests.Cart
{
    public class CartCountRequestDTO
    {
        public int ProductId { get; set; }
        public string Operation { get; set; }
    }
}
