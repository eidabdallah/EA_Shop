using EA_Ecommerce.DAL.DTO.Requests.CheckOut;
using EA_Ecommerce.DAL.DTO.Responses.CheckOut;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.BLL.Services.CheckOut
{
    public interface ICheckOutService
    {
        Task<CheckOutResponseDTO> ProcessPaymentAsync(CheckOutRequestDTO request , string UserId , HttpRequest Request);
        Task<bool> HandlePaymentSuccessAsync(int orderId);
    }
}
