namespace EA_Ecommerce.DAL.DTO.Responses.CheckOut
{
    public class CheckOutResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string? URL { get; set; }
        public string ? PaymentId { get; set; }
    }
}
