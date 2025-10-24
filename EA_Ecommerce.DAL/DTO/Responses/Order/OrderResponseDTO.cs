using EA_Ecommerce.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EA_Ecommerce.DAL.DTO.Responses.Order
{
    public class OrderResponseDTO
    {
        public int Id { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OrderStatusEnum OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public decimal TotalAmount { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaymentMethodEnum PaymentMethod { get; set; }
        public string PaymentId { get; set; }
        public string UserId { get; set; }
    }
}