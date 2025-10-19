using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.DAL.Models
{
    public class Category : BaseModel
    {
        public string Name { get; set; }
        public string MainImage { get; set; }

        public List<Product> products { get; set; } = new List<Product>();
    }
}
