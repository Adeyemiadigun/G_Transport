using System.Data;
using System.Linq.Expressions;
using G_Transport.Dtos;
using G_Transport.Models.Domain;
using G_Transport.Repositories.Interfaces;
using G_Transport.Services.Interfaces;

namespace G_Transport.Services.Implementations
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        public DriverService(IDriverRepository driverRepository, IUserRepository userRepository, IUnitOfWork unitOfWork, IProfileRepository profileRepository,IUserRoleRepository userRoleRepository,IRoleRepository roleRepository)
        {
            _driverRepository = driverRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _profileRepository = profileRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
        }
        public async Task<BaseResponse<DriverDto>> CreateAsync(RegisterDriverRequestModel model)
        {
            var res = await _userRepository.CheckAsync(x => x.Email == model.Email);
            if (res == true)
            {
                return new BaseResponse<DriverDto>
                {
                    Status = false,
                    Message = "Email already exists.",
                    Data = null
                };

            }
            var user = new User
            {
                Email = model.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password)
            };
            var profile = new Profile
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                ImageUrl = null,
                Gender = model.Gender
            };
            var driver = new Driver
            {
                Email = model.Email,
                Profile = profile,
                ProfileId = profile.Id,
                PhoneNumber = model.PhoneNumber,
                DriverNo = await GenDriverNo(),
                Address = model.Address
            };
            var role = await _roleRepository.GetRoleAsync("Driver");
            var userRole = new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id,
                Role = role,
                User = user
            };
            await _userRoleRepository.CreateAsync(userRole);
            await _userRepository.CreateAsync(user);
            await _driverRepository.CreateAsync(driver);
            await _profileRepository.CreateAsync(profile);
            await _unitOfWork.SaveChangesAsync();
            return new BaseResponse<DriverDto>
            {
                Message = "Driver created successfully",
                Status = true,
                Data = new DriverDto
                {
                    Id = driver.Id,
                    Email = driver.Email,
                    FirstName = driver.Profile.FirstName,
                    LastName = driver.Profile.LastName,
                    MiddleName = driver.Profile.MiddleName,
                    DriverNo = driver.DriverNo,
                    PhoneNumber = driver.PhoneNumber,
                    Gender = driver.Profile.Gender,
                    DateCreated = driver.DateCreated,
                }
            };
        }
        private async Task<int> GenDriverNo()
        {
             return await _driverRepository.Count()+1;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var driver = await _driverRepository.GetAsync(id);

            if (driver == null)
            {
                return false;
            }

            driver.IsDeleted = true;
            _driverRepository.Update(driver); 
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<BaseResponse<PaginationDto<DriverDto>>> GetAllAsync(PaginationRequest request)
        {
            var drivers = await _driverRepository.GetAllAsync(request);
            
            if (drivers.Items.Count() == 0 || drivers == null)
            {
                return new BaseResponse<PaginationDto<DriverDto>>
                {
                    Status = false,
                    Message = "Driver Table is empty",
                    Data = null
                };
            }
            var items = new PaginationDto<DriverDto>
            {
                TotalItems = drivers.TotalItems,
                TotalPages = drivers.TotalPages,
                CurrentPage = drivers.CurrentPage,
                HasNextPage = drivers.HasNextPage,
                HasPreviousPage = drivers.HasPreviousPage,
                PageSize = drivers.PageSize,
                Items = drivers.Items.Select(x => new DriverDto
                {
                    Id = x.Id,
                    Email = x.Email,
                    FirstName = x.Profile!.FirstName,
                    LastName = x.Profile.LastName,
                    MiddleName = x.Profile.MiddleName,
                    PhoneNumber = x.PhoneNumber,
                    DriverNo = x.DriverNo,
                    Gender = x.Profile.Gender,
                    DateCreated = x.DateCreated

                }).ToList()

            };
            return new BaseResponse<PaginationDto<DriverDto>>
            {
                Status = true,
                Message = "Drivers Retrived Successfully",
                Data = items
            };
        }

        public Task<BaseResponse<ICollection<DriverDto>>> GetSelected(List<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<DriverDto>> GetAsync(Expression<Func<Driver, bool>> exp)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<ICollection<DriverDto?>>> GetAllAsync()
        {
            var today = DateTime.UtcNow.Date;
            var availableDrivers = await _driverRepository.GetAllAsync(d =>
                !d.Trips.Any(td => td.Trip.DepartureDate.Date == today));
            if (availableDrivers.Count == 0|| availableDrivers is null)
            {
                return new BaseResponse<ICollection<DriverDto?>>
                {
                    Status = false,
                    Message = "No available drivers found",
                    Data = null
                };
            }
            var items = availableDrivers.Select(x => new DriverDto
            {
                Id = x.Id,
                Email = x.Email,
                FirstName = x.Profile.FirstName,
                LastName = x.Profile.LastName,
                MiddleName = x.Profile.MiddleName,
                PhoneNumber = x.PhoneNumber,
                DriverNo = x.DriverNo,
                Gender = x.Profile.Gender,
                DateCreated = x.DateCreated
            }).ToList();
            return new BaseResponse<ICollection<DriverDto?>>
            {
                Status = true,
                Message = "Drivers Retrived Successfully",
                Data = items
            };
        }

        public async Task<BaseResponse<DriverDto>> GetAsync(Guid id)
        {
            var driver = await _driverRepository.GetAsync(id);
            if(driver is null)
            {
                return new BaseResponse<DriverDto>
                { 
                    Data = null,
                    Message = "not found",
                    Status = false
                };
            }
            return new BaseResponse<DriverDto>
            {
                Message = "Record found",
                Status = true,
                Data = new DriverDto
                {
                    Id = driver.Id,
                    Email = driver.Email,
                    FirstName = driver.Profile.FirstName,
                    LastName = driver.Profile.LastName,
                    MiddleName = driver.Profile.MiddleName,
                    PhoneNumber = driver.PhoneNumber,
                    DriverNo = driver.DriverNo,
                    Gender = driver.Profile.Gender,
                    DateCreated = driver.DateCreated
                }
            };
        }
    }
}
