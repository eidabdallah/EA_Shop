using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.DAL.DTO.Requests.Reviews
{
    public class ReviewRequestDTO
    {
        public int ProductId { get; set; }
        public int Rate { get; set; } 
        public string? Comment { get; set; }
    }
}
