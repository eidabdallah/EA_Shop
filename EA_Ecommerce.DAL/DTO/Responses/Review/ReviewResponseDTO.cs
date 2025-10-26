using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.DAL.DTO.Responses.Review
{
    public class ReviewResponseDTO
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public string? Comment { get; set; }
        public string FullName { get; set; }
        public string UserId { get; set; }
    }
}
