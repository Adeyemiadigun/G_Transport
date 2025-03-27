using System.Linq.Expressions;
using G_Transport.Dtos;
using G_Transport.Models.Domain;
using G_Transport.Repositories.Implementations;
using G_Transport.Repositories.Interfaces;
using G_Transport.Services.Interfaces;

namespace G_Transport.Services.Implementations
{
    public class CustomerService : ICustomerServices
    {
        private readonly IUserRepository _userRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IProfileService _profileService;
        private readonly ICurrentUser _currentUser;


        public CustomerService(IUserRepository userRepository, ICustomerRepository customerRepository,IProfileRepository profileRepository, IUnitOfWork unitOfWork,IRoleRepository roleRepository, IUserRoleRepository userRoleRepository, IProfileService profileService,ICurrentUser currentUser)
        {
            _userRepository = userRepository;
            _customerRepository = customerRepository;
            _profileRepository = profileRepository;
            _unitOfWork = unitOfWork;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _profileService = profileService;
            _currentUser = currentUser;
        }
        public async Task<BaseResponse<CustomerDto>> CreateAsync(RegisterCUstomerRequestModel model)
        {
            var res = await _userRepository.CheckAsync(x => x.Email == model.Email);
            if (res == true)
            {
                return new BaseResponse<CustomerDto>
                {
                    Message = "Email already Exist",
                    Status = false,
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
        
            var customer = new Customer
            {
                Email = model.Email,
                Address = model.Address,
                Profile = profile,
                ProfileId = profile.Id,
                PhoneNumber = model.PhoneNumber,
            };
            var role = await _roleRepository.GetRoleAsync("Customer");
            var userRole = new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id,
                Role = role,
                User = user
            };
         
            await _userRoleRepository.CreateAsync(userRole);
            await _userRepository.CreateAsync(user);
            await _customerRepository.CreateAsync(customer);
            await _profileRepository.CreateAsync(profile);
            await _unitOfWork.SaveChangesAsync();
            return new BaseResponse<CustomerDto>
            {
                Message = "Customer created successfully",
                Status = true,
                Data = new CustomerDto
                {
                    Id = customer.Id,
                    Email = customer.Email,
                    FirstName = customer.Profile.FirstName,
                    LastName = customer.Profile.LastName,
                    MiddleName = customer.Profile.MiddleName,
                    PhoneNumber = customer.PhoneNumber,
                    Gender = customer.Profile.Gender
                }
            };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var customer = await _customerRepository.GetAsync(id);

            if (customer == null)
            {
                return false;
            }

            customer.IsDeleted = true;
            await _profileService.DeleteAsync(customer.ProfileId);
            _customerRepository.Update(customer); // Use BaseRepository Update method
            await _unitOfWork.SaveChangesAsync(); // Save changes

            return true;

        }

        public async Task<BaseResponse<PaginationDto<CustomerDto>>> GetAllAsync(PaginationRequest request)
        {
            var customer = await _customerRepository.GetAllAsync(request);
            if(customer == null || customer.Items.Count() == 0)
            {
                return new BaseResponse<PaginationDto<CustomerDto>>
                {
                    Message = "No customer Found",
                    Status = false,
                    Data = null
                };
            }
            var pagination = new PaginationDto<CustomerDto>
            {
                TotalItems = customer.TotalItems,
                TotalPages = customer.TotalPages,
                CurrentPage = customer.CurrentPage,
                HasNextPage = customer.HasNextPage,
                HasPreviousPage = customer.HasPreviousPage,
                PageSize = customer.PageSize,
                Items = customer.Items.Select(x => new CustomerDto
                {
                    Id = x.Id,
                    Email = x.Email,
                    FirstName = x.Profile.FirstName,
                    LastName = x.Profile.LastName,
                    MiddleName = x.Profile.MiddleName,
                    PhoneNumber = x.PhoneNumber,
                    Gender = x.Profile.Gender
                }).ToList()

            };
            return new BaseResponse<PaginationDto<CustomerDto>>
            {
                Message = "All customers",
                Status = true,
                Data = pagination
            };
        }

        public async Task<BaseResponse<CustomerDto>> GetAsync(Expression<Func<Customer, bool>> exp)
        {
            var customer = await _customerRepository.GetAsync(exp);
            if (customer == null)
            {
                return new BaseResponse<CustomerDto>
                {
                    Status = false,
                    Message = "Customer not found",
                    Data = null
                };
            }
            return new BaseResponse<CustomerDto>
            {
                Status = true,
                Message = "Customer found",
                Data = new CustomerDto
                {
                    Id = customer.Id,
                    Email = customer.Email,
                    FirstName = customer.Profile.FirstName,
                    LastName = customer.Profile.LastName,
                    MiddleName = customer.Profile.MiddleName,
                    PhoneNumber = customer.PhoneNumber,
                    Gender = customer.Profile.Gender
                }
            };
        }

        public async Task<BaseResponse<CustomerDto>> GetAsync()
        {
            var currentUser = _currentUser.GetCurrentUser();
           var customer = await _customerRepository.GetAsync(x => x.Email == currentUser);
           if (customer == null)
            {
                return new BaseResponse<CustomerDto>
                {
                    Status = false,
                    Message = "Customer not found",
                    Data = null
                };
            }
            return new BaseResponse<CustomerDto>
            {
                Status = true,
                Message = "Customer found",
                Data = new CustomerDto
                {
                    Id = customer.Id,
                    Email = customer.Email,
                    FirstName = customer.Profile.FirstName,
                    LastName = customer.Profile.LastName,
                    MiddleName = customer.Profile.MiddleName,
                    PhoneNumber = customer.PhoneNumber,
                    Gender = customer.Profile.Gender
                }
            };
        }
        public int CustomerCount()
        {
            return _customerRepository.GetAll(x => !x.IsDeleted);
        }
        public Task<BaseResponse<ICollection<CustomerDto>>> GetSelected(List<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<CustomerDto>> Update(RegisterCUstomerRequestModel model)
        {
            throw new NotImplementedException();
        }
    }
}
