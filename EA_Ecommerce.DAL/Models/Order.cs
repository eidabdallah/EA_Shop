using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.DAL.Models
{
    public enum OrderStatusEnum
    {
        Pending = 1,
        Canceled = 2,
        Approved = 3,
        Shipped = 4,
        Delivered = 5
    }
    public enum PaymentMethodEnum
    {
        Cash = 1,
        Visa = 2
    }
    public class Order
    {
        // order
        public int Id { get; set; }
        public OrderStatusEnum OrderStatus { get; set; } = OrderStatusEnum.Approved;
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime ShippedDate { get; set; }
        public decimal TotalAmount { get; set; }
        // payment 
        public PaymentMethodEnum PaymentMethod { get; set; }
        public string? PaymentId { get; set; }

        // carrier
        public string? CarrierName { get; set; }
        public string? TrackingNumber { get; set; }

        // relation 
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    }
}
