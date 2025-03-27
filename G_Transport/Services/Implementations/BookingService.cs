using G_Transport.Dtos;
using G_Transport.Models.Domain;
using G_Transport.Models.Enums;
using G_Transport.Repositories.Implementations;
using G_Transport.Repositories.Interfaces;
using G_Transport.Services.Interfaces;

namespace G_Transport.Services.Implementations
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ITripRepository _tripRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;
        private readonly ICustomerRepository _customerRepository;
        public BookingService(IBookingRepository bookingRepository, ITripRepository tripRepository,IUnitOfWork unitOfWork,ICurrentUser currentUser,ICustomerRepository customerRepository)
        {
            _bookingRepository = bookingRepository;
            _tripRepository = tripRepository;
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
            _customerRepository = customerRepository;
        }
        public async Task<BaseResponse<BookingDto>> CreateAsync(CreateBookingRequestModel model)
        {
            var currentUser = _currentUser.GetCurrentUser();
            var customer = await _customerRepository.GetAsync(x => x.Email == currentUser);
            var trip = await _tripRepository.GetAsync(model.TripId);
            if (trip == null)
            {
                return new BaseResponse<BookingDto>
                {
                    Status = false,
                    Message = "Trip not found",
                    Data = null
                };
            }
            var res = customer.Bookings.Any(x => x.Trip == trip && x.Status == Status.Successful);
            if(res)
            {
                return new BaseResponse<BookingDto>
                {
                    Status = false,
                    Message = "Already Booked Trip",
                    Data = null
                };
            }
            var vehicleCapacity = trip.Vehicle.Capacity;
            var successfulBookingsCount = trip.Bookings.Count(b => b.Status == Status.Successful);
            if (successfulBookingsCount >= vehicleCapacity)
            {
                var failedBooking = new Booking
                {
                    StartingLocation = model.StartingLocation,
                    Destination = model.Destination,
                    Status = Status.Failed,
                    TripId = trip.Id,
                    Trip = trip,

                    CustomerId = customer.Id
                };
                await _bookingRepository.CreateAsync(failedBooking);
                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse<BookingDto>
                {
                    Status = false,
                    Message = "Vehicle capacity reached",
                    Data = new BookingDto
                    {
                        Id = failedBooking.Id,
                        StartingLocation = failedBooking.StartingLocation,
                        Destination = failedBooking.Destination,
                        Status = failedBooking.Status,
                        TripId = failedBooking.TripId,
                        CustomerId = failedBooking.CustomerId
                       
                    }
                };
            }
            var booking = new Booking
            {
                StartingLocation = model.StartingLocation,
                Destination = model.Destination,
                Status = Status.Pending,
                TripId = trip.Id,
                Trip = trip,
                CustomerId = customer.Id,
                Customer = customer,
            };
            await _bookingRepository.CreateAsync(booking);
            await _unitOfWork.SaveChangesAsync();
            return new BaseResponse<BookingDto>
            {
                Status = true,
                Message = "Booking created successfully",
                Data = new BookingDto
                {
                    Id = booking.Id,
                    StartingLocation = booking.StartingLocation,
                    Destination = booking.Destination,
                    Status = booking.Status,
                    TripId = booking.TripId,
                    CustomerId = booking.CustomerId,
                    SeatNo = successfulBookingsCount + 1
                }
            };

        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var booking = await _bookingRepository.GetAsync(id);

            if (booking == null)
            {
                return false;
            }

            booking.IsDeleted = true;
            _bookingRepository.Update(booking); // Use BaseRepository Update method
            await _unitOfWork.SaveChangesAsync(); // Save changes

            return true;

        }

        public async Task<BaseResponse<PaginationDto<BookingDto>>> GetAllAsync(PaginationRequest request)
        {
            var booking = await _bookingRepository.GetAllAsync(request);
            if(booking.Items.Count() == 0 ||booking == null)
            {
                return new BaseResponse<PaginationDto<BookingDto>>
                {
                    Status = false,
                    Message = "No bookings found",
                    Data = null
                };
            }
            var items = new PaginationDto<BookingDto>
            {
                Items = booking.Items.Select(x => new BookingDto
                {
                    Id = x.Id,
                    StartingLocation = x.StartingLocation,
                    Destination = x.Destination,
                    Status = x.Status,
                    TripId = x.TripId,
                    CustomerId = x.CustomerId
                }).ToList(),
                TotalItems = booking.TotalItems,
                TotalPages = booking.TotalPages,
                CurrentPage = booking.CurrentPage,
                HasNextPage = booking.HasNextPage,
                HasPreviousPage = booking.HasPreviousPage,
                PageSize = booking.PageSize
            };
            return new BaseResponse<PaginationDto<BookingDto>>
            {
                Status = true,
                Message = "Bookings retrieved successfully",
                Data = items
            };
        }

        public async Task<BaseResponse<PaginationDto<BookingDto>>> GetAllCustomerBookings(PaginationRequest pagination)
        {
            var currentUser = _currentUser.GetCurrentUser();
            var customer = await _customerRepository.GetAsync(x => x.Email == currentUser);
            var booking = await _bookingRepository.GetAllAsync(x => x.CustomerId == customer.Id,pagination);
            if (booking == null || booking.Items.Count()==0)
            {
                return new BaseResponse<PaginationDto<BookingDto>>
                {
                    Status = false,
                    Message = "No bookings found for this customer",
                    Data = null
                };
            }
            var items = new PaginationDto<BookingDto>
            {
                Items = booking.Items.Select(x => new BookingDto
                {
                    Id = x.Id,
                    StartingLocation = x.StartingLocation,
                    Destination = x.Destination,
                    Status = x.Status,
                    TripId = x.TripId,
                    CustomerId = x.CustomerId
                }).ToList(),
                TotalItems = booking.TotalItems,
                TotalPages = booking.TotalPages,
                CurrentPage = booking.CurrentPage,
                HasNextPage = booking.HasNextPage,
                HasPreviousPage = booking.HasPreviousPage,
                PageSize = booking.PageSize
            };
            return new BaseResponse<PaginationDto<BookingDto>>
            {
                Status = true,
                Message = "Bookings retrieved successfully",
                Data = items
            };
        }
    }
}
