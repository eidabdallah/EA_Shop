using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace EA_Ecommerce.DAL.DTO.Responses.Product
{
    public class ProductResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public double Rate { get; set; }
        //[JsonIgnore]
        public string MainImage { get; set; }
        //public string MainImageUrl => $"https://localhost:7169/images/{MainImage}";
        public int CategoryId { get; set; }
        public int? BrandId { get; set; }
    }
}
