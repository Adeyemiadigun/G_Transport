using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using G_Transport.Dtos;
using G_Transport.Models.Domain;
using G_Transport.Models.Enums;
using G_Transport.Repositories.Interfaces;
using G_Transport.Services.Interfaces;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace G_Transport.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;
        private readonly ICustomerRepository _customerRepository;
        private readonly HttpClient _httpClient;
        private readonly string _paystackSecretKey;

        public PaymentService(IBookingRepository bookingRepository, IPaymentRepository paymentRepository, IUnitOfWork unitOfWork, ICustomerRepository customerRepository, ICurrentUser currentUser, IConfiguration configuration)
        {
            _bookingRepository = bookingRepository;
            _paymentRepository = paymentRepository;
            _unitOfWork = unitOfWork; 
            _currentUser = currentUser;
            _customerRepository = customerRepository;
            _httpClient = new HttpClient();
            _paystackSecretKey = configuration["Paystack:SecretKey"];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",_paystackSecretKey);
        }
        public Task<BaseResponse<PaymentDto>> GetPaymentAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<PaymentDto>> GetPaymentAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<PaginationDto<PaymentDto>>> GetPaymentsAsync(PaginationRequest request)
        {
            var result = await _paymentRepository.GetAllAsync(request);
            if (result == null || result.Items.Count() == 0)
            {
                return new BaseResponse<PaginationDto<PaymentDto>>
                {
                    Status = false,
                    Message = "No payments found",
                    Data = null
                };
            }
            var payments = new PaginationDto<PaymentDto>
            {
                TotalItems = result.TotalItems,
                TotalPages = result.TotalPages,
                CurrentPage = result.CurrentPage,
                HasNextPage = result.HasNextPage,
                HasPreviousPage = result.HasPreviousPage,
                PageSize = result.PageSize,
                Items = result.Items.Select(x => new PaymentDto
                {
                    RefrenceNo = x.RefrenceNo,
                    Transaction = x.Transaction,
                    Amount = x.Amount,
                    Status = x.Status,
                    CustomerId = x.CustomerId,
                    DateCreated = x.DateCreated,
                    TripId = x.TripId,
                    Trip = new TripDto
                    {
                        DepartureTime = (TimeSpan)x.Trip.DepartureTime,
                        Destination = x.Trip.Destination,
                        VehicleId = x.Trip.VehicleId,
                        Amount = x.Trip.Amount,
                        Description = x.Trip.Description,
                        Status = x.Trip.Status
                    }
                }).ToList()
            };
            return new BaseResponse<PaginationDto<PaymentDto>>
            {
                Message = "Data retrieved",
                Status = true,
                Data = payments
            };
        }

        public async Task<BaseResponse<PaginationDto<PaymentDto>>> GetCustomerPaymentsAsync(PaginationRequest request)
        {
            var currentUser = _currentUser.GetCurrentUser();
            var customer = await _customerRepository.GetAsync(x => x.Email == currentUser);

            if (customer == null)
            {
                return new BaseResponse<PaginationDto<PaymentDto>>
                {
                    Status = false,
                    Message = "Customer not found",
                    Data = null
                };
            }

            var result = await _paymentRepository.GetAllAsync(x => x.CustomerId == customer.Id, request);

            if (result == null || result.Items == null || !result.Items.Any())
            {
                return new BaseResponse<PaginationDto<PaymentDto>>
                {
                    Status = false,
                    Message = "No payments found",
                    Data = null
                };
            }

            var payments = new PaginationDto<PaymentDto>
            {
                TotalItems = result.TotalItems,
                TotalPages = result.TotalPages,
                CurrentPage = result.CurrentPage,
                HasNextPage = result.HasNextPage,
                HasPreviousPage = result.HasPreviousPage,
                PageSize = result.PageSize,
                Items = result.Items.Select(x => new PaymentDto
                {
                    RefrenceNo = x.RefrenceNo,
                    Transaction = x.Transaction,
                    Amount = x.Amount,
                    Status = x.Status,
                    CustomerId = x.CustomerId,
                    DateCreated = x.DateCreated,
                    TripId = x.TripId,
                    Trip = x.Trip != null ? new TripDto
                    {
                        DepartureTime = x.Trip.DepartureTime ?? TimeSpan.Zero,  // Null-safe conversion
                        Destination = x.Trip.Destination,
                        VehicleId = x.Trip.VehicleId,
                        Amount = x.Trip.Amount,
                        Description = x.Trip.Description,
                        Status = x.Trip.Status
                    } : null!
                }).ToList()
            };

            return new BaseResponse<PaginationDto<PaymentDto>>
            {
                Message = "Data retrieved",
                Status = true,
                Data = payments
            };
        }

     public async Task<BaseResponse<string>> InitializePayment(PaymentRequest request)
        {
            var payload = new
            {
                email = request.Email,
                amount = request.Amount,
                callback_url = request.CallbackUrl,
                metadata = new { BookingId = request.BookingId }
            };

            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://api.paystack.co/transaction/initialize", content);
            var result = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<PaystackInitializeResponse>(result);

            if (jsonResponse != null && jsonResponse.status)
            {
                return new BaseResponse<string>
                {
                    Status = true,
                    Message = "Payment initialized successfully",
                    Data = jsonResponse.data.authorization_url
                };
            }

            return new BaseResponse<string>
            {
                Status = false,
                Message = "Failed to initialize payment",
                Data = null
            };
        }
        public async Task<BaseResponse<PaymentDto>> VerifyPaymentAsync(string reference)
        {
            var secretKey = _paystackSecretKey;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", secretKey);

            var response = await _httpClient.GetAsync($"https://api.paystack.co/transaction/verify/{reference}");
            var result = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<PaystackVerifyResponse>(result);

            if (jsonResponse == null || jsonResponse.data == null)
            {
                return new BaseResponse<PaymentDto>
                {
                    Status = false,
                    Message = "Invalid response from Paystack",
                    Data = null
                };
            }

            bool isPaymentSuccessful = jsonResponse.status && jsonResponse.data.status == "success";

            // ✅ Extract BookingId from Paystack metadata
            var bookingId = jsonResponse?.data?.Metadata?.BookingId;
            if (bookingId == null)
            {
                return new BaseResponse<PaymentDto>
                {
                    Status = false,
                    Message = "Booking ID not found in payment metadata",
                    Data = null
                };
            }

            var booking = await _bookingRepository.GetAsync(x => x.Id == bookingId);
            if (booking == null)
            {
                return new BaseResponse<PaymentDto>
                {
                    Status = false,
                    Message = "Booking not found",
                    Data = null
                };
            }

            // Create payment record
            var payment = new Payment
            {
                RefrenceNo = reference,
                Transaction = "Trip Payment",
                Amount = jsonResponse.data.amount / 100, // Convert from Kobo to Naira
                Status = isPaymentSuccessful ? Status.Successful : Status.Failed,
                CustomerId = booking.CustomerId,
                TripId = booking.TripId
            };

            await _paymentRepository.CreateAsync(payment);

            if (isPaymentSuccessful)
            {
                // Update booking status
                booking.Status = Status.Successful;
                _bookingRepository.Update(booking);

                await _unitOfWork.SaveChangesAsync();

                // Return PaymentDto with Ticket
                var paymentDto = new PaymentDto
                {
                    RefrenceNo = payment.RefrenceNo,
                    Transaction = payment.Transaction,
                    TicketNumber = $"{payment.RefrenceNo.Substring(0, 8)}-{booking.Id.ToString().Substring(0, 8)}",
                    Amount = payment.Amount,
                    Status = payment.Status,
                    CustomerId = booking.CustomerId,
                    DateCreated = payment.DateCreated,
                    TripId = booking.TripId,
                    Trip = new TripDto
                    {
                        DepartureTime = (TimeSpan)booking.Trip.DepartureTime,
                        Destination = booking.Trip.Destination,
                        VehicleId = booking.Trip.VehicleId,
                        Amount = booking.Trip.Amount,
                        Description = booking.Trip.Description,
                        Status = booking.Trip.Status
                    }
                };

                return new BaseResponse<PaymentDto>
                {
                    Status = true,
                    Message = "Payment verified successfully, booking confirmed",
                    Data = paymentDto
                };
            }

            return new BaseResponse<PaymentDto>
            {
                Status = false,
                Message = "Payment verification failed",
                Data = null
            };
        }

    }
}


