using EA_Ecommerce.DAL.DTO.Requests.CheckOut;
using EA_Ecommerce.DAL.DTO.Responses.CheckOut;
using EA_Ecommerce.DAL.Repositories.Carts;
using Microsoft.AspNetCore.Http;
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

        public CheckOutService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task<CheckOutResponseDTO> ProcessPaymentAsync(CheckOutRequestDTO request, string UserId , HttpRequest Request)
        {
            var carItems = await _cartRepository.GetUserCart(UserId);
            // if cart is empty
            if (!carItems.Any())
            {
                return new CheckOutResponseDTO
                {
                    Success = false,
                    Message = "Cart is empty."
                };
            }
            // process payment based on payment method
            if (request.PaymentMethodId == "Cash"){
                return new CheckOutResponseDTO
                {
                    Success = true,
                    Message = "Cash."
                };
            }
            if (request.PaymentMethodId == "Visa") {
                var options = new SessionCreateOptions{
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>{},
                    Mode = "payment",
                    SuccessUrl = $"{Request.Scheme}://{Request.Host}/checkout/success",
                    CancelUrl = $"{Request.Scheme}://{Request.Host}/checkout/cancel",
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
                            UnitAmount = (long)item.Product.Price,
                        },
                        Quantity = item.Count,
                    });
                }
                var service = new SessionService();
                var session = service.Create(options);
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
                Message = "invalid."
            };

        }
    }
}
