using System.Linq.Expressions;
using G_Transport.Dtos;
using G_Transport.Models.Domain;
using G_Transport.Repositories.Implementations;
using G_Transport.Repositories.Interfaces;
using G_Transport.Services.Interfaces;

namespace G_Transport.Services.Implementations
{
    public class TripService : ITripService
    {
        private readonly ITripRepository _tripRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;
        private readonly ICustomerRepository _customerRepository;
        private readonly IDriverRepository _driverRepository;
        public TripService(ITripRepository tripRepository, IVehicleRepository vehicleRepository, IUnitOfWork unitOfWork,ICustomerRepository customerRepository, IDriverRepository driverRepository)
        {
            _tripRepository = tripRepository;
            _vehicleRepository = vehicleRepository;
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
            _driverRepository = driverRepository;
        }
        public async Task<BaseResponse<TripDto>> CreateAsync(CreateTripRequestModel model)
        {
            var vehicle = await _vehicleRepository.GetAsync(model.VehicleId);
            if (vehicle == null)
            {
                return new BaseResponse<TripDto>
                {
                    Message = "Vehicle not found",
                    Status = false
                };
            }

            var trip = new Trip
            {
                StartingLocation = model.StartingLocation,
                Destination = model.Destination,
                DepartureTime = model.DepartureTime,
                DepartureDate = model.DepartureDate,
                Description = model.Description,
                Amount = model.Amount,
                VehicleId = model.VehicleId,
                Vehicle = vehicle
            };

            await _tripRepository.CreateAsync(trip);
            await _unitOfWork.SaveChangesAsync(); 
            var drivers = await _driverRepository.GetByIdsAsync(model.DriverIds);
            if (drivers.Count != model.DriverIds.Count)
            {
                return new BaseResponse<TripDto>
                {
                    Message = "One or more drivers not found",
                    Status = false
                };
            }

            var tripDrivers = drivers.Select(driver => new TripDriver
            {
                TripId = trip.Id,
                Trip = trip,
                DriverId = driver.Id,
                Driver = driver
            }).ToList();

            trip.Drivers = tripDrivers;
            await _unitOfWork.SaveChangesAsync(); 

            return new BaseResponse<TripDto>
            {
                Message = "Trip created successfully",
                Status = true,
                Data = new TripDto
                {
                    Id = trip.Id,
                    StartingLocation = trip.StartingLocation,
                    Destination = trip.Destination,
                    DepartureTime = trip.DepartureTime ?? default,
                    DepartureDate = trip.DepartureDate,
                    DriverNo = trip.Drivers.Count,
                    Drivers = drivers.Select(d => new DriverDto
                    {
                        Id = d.Id,
                        FirstName = d.Profile.FirstName,
                        LastName = d.Profile.LastName
                    }).ToList(),
                    VehicleId = trip.VehicleId,
                    Vehicle = trip.Vehicle,
                    Description = trip.Description,
                    Amount = trip.Amount,
                    Status = trip.Status
                }
            };
        }



        public async Task<bool> DeleteAsync(Guid id)
        {
            var trip = await _tripRepository.GetAsync(id);

            if (trip == null)
            {
                return false;
            }

            trip.IsDeleted = true;
            _tripRepository.Update(trip); 
            await _unitOfWork.SaveChangesAsync(); 

            return true;
        }


        public async Task<BaseResponse<PaginationDto<TripDto>>> GetAllAsync(PaginationRequest request)
        {
            var trips = await _tripRepository.GetAllAsync(request);
            if (trips.Items.Count() == 0 || trips == null)
            {
                return new BaseResponse<PaginationDto<TripDto>>
                {
                    Status = false,
                    Message = "No trips found",
                    Data = null
                };
            }
            var result = new PaginationDto<TripDto>
            {
                TotalItems = trips.TotalItems,
                TotalPages = trips.TotalPages,
                CurrentPage = trips.CurrentPage,
                HasNextPage = trips.HasNextPage,
                HasPreviousPage = trips.HasPreviousPage,
                PageSize = trips.PageSize,
                Items = trips.Items.Select(x => new TripDto
                {
                    Id = x.Id,
                    StartingLocation = x.StartingLocation,
                    Destination = x.Destination,
                    DepartureTime = (TimeSpan)x.DepartureTime,
                    DepartureDate = x.DepartureDate,
                    VehicleId = x.VehicleId,
                    Vehicle = x.Vehicle,
                    Drivers = x.Drivers.Select(c => new DriverDto
                    {
                        Id = c.Driver.Id,
                        FirstName = c.Driver.Profile.FirstName,
                        LastName = c.Driver.Profile.LastName,
                        DriverNo = c.Driver.DriverNo
                    }).ToList(),
                    Description = x.Description,
                    Amount = x.Amount,
                    Status = x.Status
                }).ToList()
            };
            return new BaseResponse<PaginationDto<TripDto>>
            {
                Status = true,
                Message = "Trips retrieved successfully",
                Data = result
            };
        }

        public async Task<BaseResponse<PaginationDto<TripDto>>> GetAllAvailableAsync(PaginationRequest request)
        {
            var currentDateTime = DateTime.Now;
            var trips = await _tripRepository.GetAllAsync(x => x.DepartureDate > currentDateTime, request);


            if (trips == null)
            {
                return new BaseResponse<PaginationDto<TripDto>>
                {
                    Status = false,
                    Message = "No upcoming trips found",
                    Data = null
                };
            }

            var result = new PaginationDto<TripDto>
            {
                TotalItems = trips.TotalItems,
                TotalPages = trips.TotalPages,
                CurrentPage = trips.CurrentPage,
                HasNextPage = trips.HasNextPage,
                HasPreviousPage = trips.HasPreviousPage,
                PageSize = trips.PageSize,
                Items = trips.Items.Select(x => new TripDto
                {
                    Id = x.Id,
                    StartingLocation = x.StartingLocation,
                    Destination = x.Destination,
                    DepartureTime = (TimeSpan)x.DepartureTime,
                    DepartureDate = x.DepartureDate,
                    VehicleId = x.VehicleId,
                    Vehicle = x.Vehicle,
                    Drivers = x.Drivers.Select(c => new DriverDto
                    {
                        Id = c.Driver.Id,
                        FirstName = c.Driver.Profile.FirstName,
                        LastName = c.Driver.Profile.LastName,
                        DriverNo = c.Driver.DriverNo
                    }).ToList(),
                    Description = x.Description,
                    Amount = x.Amount,
                    Status = x.Status
                }).ToList()
            };

            return new BaseResponse<PaginationDto<TripDto>>
            { 
                Status = true,
                Message = "Upcoming trips retrieved successfully",
                Data = result
            };
        }


        public async Task<BaseResponse<PaginationDto<TripDto>>> GetAllRecent(PaginationRequest request)
        {
            var trips = await _tripRepository.GetAllAsync(x => x.DateCreated > DateTime.UtcNow.AddDays(-7), request);
            if (trips.Items.Count() == 0 || trips == null)
            {
                return new BaseResponse<PaginationDto<TripDto>>
                {
                    Status = false,
                    Message = "No trips found",
                    Data = null
                };
            }
            var result = new PaginationDto<TripDto>
            {
                TotalItems = trips.TotalItems,
                TotalPages = trips.TotalPages,
                CurrentPage = trips.CurrentPage,
                HasNextPage = trips.HasNextPage,
                HasPreviousPage = trips.HasPreviousPage,
                PageSize = trips.PageSize,
                Items = trips.Items.Select(x => new TripDto
                {
                    Id = x.Id,
                    StartingLocation = x.StartingLocation,
                    Destination = x.Destination,
                    DepartureTime = (TimeSpan)x.DepartureTime,
                    DepartureDate = x.DepartureDate,
                    VehicleId = x.VehicleId,
                    Vehicle = x.Vehicle,
                    Drivers = x.Drivers.Select(c => new DriverDto
                    {
                        Id = c.Driver.Id,
                        FirstName = c.Driver.Profile.FirstName,
                        LastName = c.Driver.Profile.LastName,
                        DriverNo = c.Driver.DriverNo
                    }).ToList(),
                    Description = x.Description,
                    Amount = x.Amount,
                    Status = x.Status
                }).ToList()
            };
            return new BaseResponse<PaginationDto<TripDto>>
            {
                Status = true,
                Message = "Recent trips retrieved successfully",
                Data = result
            };
        }

        public async Task<BaseResponse<PaginationDto<TripDto>>> GetAllWithoutReviewAsync(PaginationRequest request)
        {
            var user = _currentUser.GetCurrentUser();
            var customer = await _customerRepository.GetAsync(x => x.Email == user);
            var trips = await _tripRepository.GetAllAsync(x => !x.Reviews.Any(r => r.CustomerId == customer.Id), request);
            if (trips.Items.Count() == 0 || trips == null)
            {
                return new BaseResponse<PaginationDto<TripDto>>
                {
                    Status = false,
                    Message = "No trips found",
                    Data = null
                };
            }
            var result = new PaginationDto<TripDto>
            {
                TotalItems = trips.TotalItems,
                TotalPages = trips.TotalPages,
                CurrentPage = trips.CurrentPage,
                HasNextPage = trips.HasNextPage,
                HasPreviousPage = trips.HasPreviousPage,
                PageSize = trips.PageSize,
                Items = trips.Items.Select(x => new TripDto
                {
                    Id = x.Id,
                    StartingLocation = x.StartingLocation,
                    Destination = x.Destination,
                    DepartureTime = (TimeSpan)x.DepartureTime,
                    DepartureDate = x.DepartureDate,
                    VehicleId = x.VehicleId,
                    Vehicle = x.Vehicle,
                    Drivers = x.Drivers.Select(c => new DriverDto
                    {
                        Id = c.Driver.Id,
                        FirstName = c.Driver.Profile.FirstName,
                        LastName = c.Driver.Profile.LastName,
                        DriverNo = c.Driver.DriverNo
                    }).ToList(),
                    Description = x.Description,
                    Amount = x.Amount,
                    Status = x.Status
                }).ToList()
            };
            return new BaseResponse<PaginationDto<TripDto>>
            {
                Status = true,
                Message = "Trips retrieved successfully",
                Data = result
            };
        }

        public async Task<BaseResponse<TripDto>> GetAsync(Expression<Func<Trip, bool>> exp)
        {
           var trip = await _tripRepository.GetAsync(exp);
            if (trip == null)
            {
                return new BaseResponse<TripDto>
                {
                    Status = false,
                    Message = "Trip not found",
                    Data = null
                };
            }
            return new BaseResponse<TripDto>
            {
                Status = true,
                Message = "Trip retrieved successfully",
                Data = new TripDto
                {
                    Id = trip.Id,
                    StartingLocation = trip.StartingLocation,
                    Destination = trip.Destination,
                    DepartureTime = (TimeSpan)trip.DepartureTime,
                    DepartureDate = trip.DepartureDate,
                    VehicleId = trip.VehicleId,
                    Vehicle = trip.Vehicle,
                    Description = trip.Description,
                    Amount = trip.Amount,
                    Status = trip.Status,
                }
            };
        }

        public async Task<BaseResponse<TripDto>> GetDriverAssigned()
        {
            var user = _currentUser.GetCurrentUser();
            var driver = await _driverRepository.GetAsync(x => x.Email == user);

            if (driver == null)
            {
                return new BaseResponse<TripDto>
                {
                    Status = false,
                    Message = "Driver not found",
                    Data = null
                };
            }

            var today = DateTime.UtcNow.Date;
            var trip = await _tripRepository.GetAsync(x =>
                x.Drivers.Any(c => c.DriverId == driver.Id) && x.DepartureDate.Date == today);

            if (trip == null)
            {
                return new BaseResponse<TripDto>
                {
                    Status = false,
                    Message = "No assigned trip for today",
                    Data = null
                };
            }

            var tripDto = new TripDto
            {
                Id = trip.Id,
                DepartureTime = trip.DepartureTime ?? TimeSpan.Zero,
                DepartureDate = trip.DepartureDate,
                Destination = trip.Destination,
                VehicleId = trip.VehicleId,
                Amount = trip.Amount,
                Description = trip.Description,
                Status = trip.Status
            };

            return new BaseResponse<TripDto>
            {
                Status = true,
                Message = "Trip retrieved successfully",
                Data = tripDto
            };
        }

        public async Task<BaseResponse<TripDto>> UpdateAsync(UpdateTripRequestModel model)
        {
            var trip = await _tripRepository.GetAsync(model.Id);
            if (trip == null)
            {
                return new BaseResponse<TripDto>
                {
                    Status = false,
                    Message = $"Trip with Id: {model.Id} not found",
                    Data = null
                };
            }
            trip.DepartureTime = model.DepartureTime;
            trip.DepartureDate = model.DepartureDate;
            trip.Destination = model.Destination;
            trip.Amount = model.Amount;
            trip.Description = model.Description;
            trip.Status = model.Status;
             _tripRepository.Update(trip);
            await _unitOfWork.SaveChangesAsync();
            return new BaseResponse<TripDto> {
                Status = true,
                Message = "Trip updated successfully",
                Data = new TripDto
                {
                    Id = trip.Id,
                    StartingLocation = trip.StartingLocation,
                    Destination = trip.Destination,
                    DepartureTime = (TimeSpan)trip.DepartureTime,
                    DepartureDate = trip.DepartureDate,
                    VehicleId = trip.VehicleId,
                    Vehicle = trip.Vehicle,
                    Drivers = trip.Drivers.Select(c => new DriverDto
                    {
                        Id = c.Driver.Id,
                        FirstName = c.Driver.Profile.FirstName,
                        LastName = c.Driver.Profile.LastName,
                        DriverNo = c.Driver.DriverNo
                    }).ToList(),
                    Description = trip.Description,
                    Amount = trip.Amount,
                    Status = trip.Status,
                }
            };
        }
        public int TripCount(Expression<Func<Trip, bool>> exp)
        {
            return  _tripRepository.GetAll(exp);
        }
       
    }
}

