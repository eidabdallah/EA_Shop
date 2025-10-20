using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EA_Ecommerce.DAL.DTO.Responses.Brand
{
    public class BrandResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //[JsonIgnore]
        public string MainImage { get; set; }
        //public string MainImageUrl => $"https://localhost:7169/images/{MainImage}";
    }
}
