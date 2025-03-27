using System.Linq.Expressions;
using G_Transport.Dtos;
using G_Transport.Models.Domain;
using G_Transport.Repositories.Implementations;
using G_Transport.Repositories.Interfaces;
using G_Transport.Services.Interfaces;

namespace G_Transport.Services.Implementations
{
    public class VehicleService : IVehicleService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public VehicleService(IDriverRepository driverRepository, IUnitOfWork unitOfWork,IVehicleRepository vehicleRepository)
        {
            _driverRepository = driverRepository;
            _unitOfWork = unitOfWork;
            _vehicleRepository = vehicleRepository;
        }

        public async Task<BaseResponse<VehicleDto>> CreateAsync(RegisterVehicleRequestModel model)
        {
            var driver = await _driverRepository.GetAsync(model.DriverId);
            if (driver == null)
            {
                return new BaseResponse<VehicleDto>
                {
                    Status = false,
                    Message = "Driver not found",
                    Data = null
                };
            }
            var vehicle = new Vehicle
            {
                Name = model.Name,
                Description = model.Description,
                Capacity = model.Capacity,
                PlateNo = model.PlateNo,
                Driver = driver,
                DriverId = driver.Id
            };
            driver.Vehicle = vehicle;
             _driverRepository.Update(driver);
            await _vehicleRepository.CreateAsync(vehicle);
            await _unitOfWork.SaveChangesAsync();
            return new BaseResponse<VehicleDto>
            {
                Status = true,
                Message = "Vehicle created successfully",
                Data = new VehicleDto
                {
                    Id = vehicle.Id,
                    Name = vehicle.Name,
                    Description = vehicle.Description,
                    Capacity = vehicle.Capacity,
                    PlateNo = vehicle.PlateNo,
                    DriverId = driver.Id,
                    DriverNo = driver.DriverNo
                }
            };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var vehicle = await _vehicleRepository.GetAsync(id);

            if (vehicle == null)
            {
                return false;
            }

            vehicle.IsDeleted = true;
            _vehicleRepository.Update(vehicle); // Use BaseRepository Update method
            await _unitOfWork.SaveChangesAsync(); // Save changes

            return true;
        }

        public async Task<BaseResponse<PaginationDto<VehicleDto>>> GetAllAsync(PaginationRequest request)
        {
            var vehicles = await _vehicleRepository.GetAllAsync(request);
            if(vehicles.Items.Count() == 0 || vehicles == null)
            {
                return new BaseResponse<PaginationDto<VehicleDto>>
                {
                    Status = false,
                    Message = "No vehicles found",
                    Data = null
                };
            }
            var pagination = new PaginationDto<VehicleDto>
            {
                TotalPages = vehicles.TotalPages,
                TotalItems = vehicles.TotalItems,
                CurrentPage = vehicles.CurrentPage,
                HasNextPage = vehicles.HasNextPage,
                HasPreviousPage = vehicles.HasPreviousPage,
                PageSize = vehicles.PageSize,
                Items = vehicles.Items.Select(x => new VehicleDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Capacity = x.Capacity,
                    PlateNo = x.PlateNo,
                    DriverId = x.DriverId,
                    DriverNo = x.Driver.DriverNo
                }).ToList()
            };
            return new BaseResponse<PaginationDto<VehicleDto>>
            {
                Status = true,
                Message = "Vehicles retrieved successfully",
                Data = pagination
            };
        }

        public async Task<BaseResponse<VehicleDto>> GetAsync(Expression<Func<Vehicle, bool>> exp)
        {
            var vehicle = await _vehicleRepository.GetAsync(exp);
            if (vehicle == null)
            {
                return new BaseResponse<VehicleDto>
                {
                    Status = false,
                    Message = "Vehicle not found",
                    Data = null
                };
            }
            return new BaseResponse<VehicleDto>
            {
                Status = true,
                Message = "Vehicle retrieved successfully",
                Data = new VehicleDto
                {
                    Id = vehicle.Id,
                    Name = vehicle.Name,
                    Description = vehicle.Description,
                    Capacity = vehicle.Capacity,
                    PlateNo = vehicle.PlateNo,
                    DriverId = vehicle.DriverId,
                    DriverNo = vehicle.Driver.DriverNo
                }
            };
        }

        public Task<BaseResponse<ICollection<VehicleDto>>> GetSelected(List<Guid> ids)
        {
            throw new NotImplementedException();
        }
    }
}
