using G_Transport.Models.Domain;
using G_Transport.Models.Enums;

namespace G_Transport.Dtos
{
    public class PaymentDto
    {
        public string Transaction { get; set; }
        public string RefrenceNo { get; set; }
        public string TicketNumber { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public Guid CustomerId { get; set; }
        public Guid TripId { get; set; }
        public TripDto Trip { get; set; }
        public Status Status { get; set; }
        public DateTime DateCreated { get; set; }
        public int SeatNo { get; set; }
    }
    public class PaymentRequestModel
    {
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public Guid TripId { get; set; }
        public Guid BookingId { get; set; }
    }
    public class PaymentRequest
    {
            public string Email { get; set; }
            public decimal Amount { get; set; }
            public string CallbackUrl { get; set; }
            public Guid BookingId { get; set; } 
    }

    public class PaystackInitializeResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public PaystackData data { get; set; }
    }

    public class PaystackData
    {
        public string authorization_url { get; set; }
        public string access_code { get; set; }
        public string reference { get; set; }
    }
    public class PaystackVerifyResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public PaystackVerifyData data { get; set; }
    }
    public class PaystackMetadata
    {
        public Guid BookingId { get; set; }
    }

    public class PaystackVerifyData
    {
        public string id { get; set; }
        public string status { get; set; }  
        public string reference { get; set; }
        public int amount { get; set; }
        public string gateway_response { get; set; }
        public string paid_at { get; set; }
        public string created_at { get; set; }
        public PaystackMetadata Metadata { get; set; }
        public PaystackVerifyCustomer customer { get; set; }
    }

    public class PaystackVerifyCustomer
    {
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string phone { get; set; }
    }

}