using G_Transport.Dtos;
using G_Transport.Repositories.Interfaces;
using G_Transport.Services.Interfaces;

namespace G_Transport.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseResponse<PaginationDto<UserDto>>> GetAllAsync(PaginationRequest request)
        {
            var response = await _userRepository.GetAllAsync(request);
            if (response == null)
            {
                return new BaseResponse<PaginationDto<UserDto>>
                {
                    Message = "No users found",
                    Status = false,
                    Data = null
                };
            }
            var users = new PaginationDto<UserDto>
            {
                Items = response.Items.Select(user => new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    DateCreated = user.DateCreated,
                    IsDeleted = user.IsDeleted
                }).ToList(),
                TotalItems = response.TotalItems,
                TotalPages = response.TotalPages,
                CurrentPage = response.CurrentPage,
                HasNextPage = response.HasNextPage,
                HasPreviousPage = response.HasPreviousPage,
                PageSize = response.PageSize
            };
            return new BaseResponse<PaginationDto<UserDto>>
            {
                Message = "Users found",
                Status = true,
                Data =users
            };
        }

        public async Task<BaseResponse<UserDto>> GetAsync(Guid id)
        {
            var user = await _userRepository.GetAsync(id);
            if (user == null)
            {
                return new BaseResponse<UserDto>
                {
                    Message = $"User with Id: {id} not found",
                    Status = false,
                    Data = null
                };
            }
            return new BaseResponse<UserDto>
            {
                Message = "User found",
                Status = true,
                Data = new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    DateCreated = user.DateCreated,
                    IsDeleted = user.IsDeleted

                }
            };

        }

        public async Task<BaseResponse<UserDto>> LoginAsync(LoginRequestModel model)
        {
            var user = await _userRepository.GetAsync(x => x.Email == model.Email);
            if(user == null)
            {
                return new BaseResponse<UserDto>
                {
                    Message = "Email does not exist",
                    Status = false,
                    Data = null
                };
            }
            if(BCrypt.Net.BCrypt.Verify(model.Password,user.Password))
            {
                return new BaseResponse<UserDto>
                {
                    Message = "Login Successfull",
                    Status = true,
                    Data = new UserDto
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Roles = user.Roles,
                        DateCreated = user.DateCreated,
                    }
                };
            }
            return new BaseResponse<UserDto>
            {
                Message = "Invalid Credentials",
                Status = false,
                Data = null
            };
        }
    }
}
