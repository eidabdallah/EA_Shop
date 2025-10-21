using EA_Ecommerce.DAL.Models;
using System.Text.Json.Serialization;

namespace EA_Ecommerce.DAL.DTO.Requests.CheckOut
{
    public class CheckOutRequestDTO
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaymentMethodEnum PaymentMethod { get; set; }
    }
}
