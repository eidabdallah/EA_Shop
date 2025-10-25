using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.DAL.Models
{
    public class Product : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public double Rate { get; set; }
        public string MainImage { get; set; }          
        public string? MainImagePublicId { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public Brand? Brand { get; set; }
        public int? BrandId { get; set; }
        public List<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
    }
}
