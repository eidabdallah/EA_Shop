using EA_Ecommerce.DAL.DTO.Requests.CheckOut;
using EA_Ecommerce.DAL.DTO.Responses.CheckOut;
using EA_Ecommerce.DAL.Models;
using EA_Ecommerce.DAL.Repositories.Carts;
using EA_Ecommerce.DAL.Repositories.Order;
using EA_Ecommerce.DAL.Repositories.OrderItem;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.BLL.Services.CheckOut
{
    public class CheckOutService : ICheckOutService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IEmailSender _emailSender;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;

        public CheckOutService(ICartRepository cartRepository , IEmailSender emailSender ,IOrderRepository orderRepository, IOrderItemRepository orderItemRepository)
        {
            _cartRepository = cartRepository;
            _emailSender = emailSender;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
        }

        public async Task<bool> HandlePaymentSuccessAsync(int orderId)
        {
            var order = await _orderRepository.GetUserByOrderAsync(orderId);
            if (order == null)
                return false;
            var subject = "";
            var body = "";
            if (order.PaymentMethod == PaymentMethodEnum.Visa)
            {
                order.OrderStatus = OrderStatusEnum.Approved;
                var carts = await _cartRepository.GetUserCartAsync(order.UserId);
                var orderItems = new List<OrderItem>();
                foreach (var item in carts)
                {
                   var orderItem = new OrderItem
                   {
                       OrderId = order.Id,
                       ProductId = item.ProductId,
                       TotalPrice = item.Product.Price * item.Count,
                       Count = item.Count,
                       Price = item.Product.Price
                   };
                    orderItems.Add(orderItem);
                }
              await _orderItemRepository.AddAsync(orderItems);
              await _cartRepository.ClearCartAsync(order.UserId);

                subject = "Payment Successfully – Ecommerce";
                body =
                    $"<h1>Thank you for your payment</h1>" +
                    $"<p>Your payment for order #{order.Id}</p>" +
                    $"<p>Total amount: {order.TotalAmount}$</p>";
            }
            else if (order.PaymentMethod == PaymentMethodEnum.Cash)
            {
                subject = "Order placed Successfully";
                body =
                    $"<h1>Thank you for your order</h1>" +
                    $"<p>Your order number: #{order.Id}</p>" +
                    $"<p>Total amount: {order.TotalAmount}$</p>";
            }
            await _emailSender.SendEmailAsync(order.User.Email!, subject, body);
            return true;
        }

        public async Task<CheckOutResponseDTO> ProcessPaymentAsync(CheckOutRequestDTO request, string UserId , HttpRequest Request)
        {
            var carItems = await _cartRepository.GetUserCartAsync(UserId);
            // if cart is empty
            if (!carItems.Any())
            {
                return new CheckOutResponseDTO
                {
                    Success = false,
                    Message = "Cart is empty."
                };
            }
            Order order = new Order
            {
               UserId = UserId,
               PaymentMethod = request.PaymentMethod,
               TotalAmount = carItems.Sum(i => i.Product.Price * i.Count)
            };
            await _orderRepository.CreateAsync(order);

            if (request.PaymentMethod == PaymentMethodEnum.Cash){
                return new CheckOutResponseDTO
                {
                    Success = true,
                    Message = "Cash."
                };
            }
            if (request.PaymentMethod == PaymentMethodEnum.Visa) {
                var options = new SessionCreateOptions{
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>{},
                    Mode = "payment",
                    SuccessUrl = $"{Request.Scheme}://{Request.Host}/api/Customer/CheckOut/success/{order.Id}",
                    CancelUrl = $"{Request.Scheme}://{Request.Host}/api/Customer/CheckOut/cancel",
                };
                foreach(var item in carItems)
                {
                    options.LineItems.Add(new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {                            
                            Currency = "USD",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product.Name,
                                Description = item.Product.Description,
                            },
                            UnitAmount = (long)(item.Product.Price * 100),
                        },
                        Quantity = item.Count,
                    });
                }
                var service = new SessionService();
                var session = await service.CreateAsync(options);

                order.PaymentId = session.Id;

                return new CheckOutResponseDTO
                {
                    Success = true,
                    Message = "Payment processed successfully.",
                    URL = session.Url,
                    PaymentId = session.Id
                };
            }
            return new CheckOutResponseDTO
            {
                Success = false,
                Message = "invalid payment method."
            };

        }



    }
}
