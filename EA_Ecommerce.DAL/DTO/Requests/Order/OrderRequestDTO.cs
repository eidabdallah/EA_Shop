using EA_Ecommerce.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EA_Ecommerce.DAL.DTO.Requests.Order
{
    public class OrderRequestDTO
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OrderStatusEnum status { get; set; }
    }
}
