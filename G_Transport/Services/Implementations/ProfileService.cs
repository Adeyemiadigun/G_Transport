using G_Transport.Dtos;
using G_Transport.Models.Domain;
using G_Transport.Repositories.Interfaces;
using G_Transport.Services.Interfaces;

namespace G_Transport.Services.Implementations
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ProfileService(IProfileRepository profileRepository,ICustomerRepository customerRepository,IUnitOfWork unitOfWork)
        {
            _profileRepository = profileRepository;
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var profile = await _profileRepository.GetAsync(x => x.Id == id);
            if (profile == null) 
                return false;
            profile.IsDeleted = true;
            _profileRepository.Update(profile);
            return true;
        }

        public async Task<BaseResponse<PaginationDto<ProfileDto>>> GetAllAsync(PaginationRequest request)
        {
            var customer = await _customerRepository.GetAllAsync(request);
            if (customer == null || customer.Items.Count() == 0)
            {
                return new BaseResponse<PaginationDto<ProfileDto>>
                {
                    Message = "No customer Found",
                    Status = false,
                    Data = null
                };
            }
            var pagination = new PaginationDto<ProfileDto>
            {
                TotalItems = customer.TotalItems,
                TotalPages = customer.TotalPages,
                CurrentPage = customer.CurrentPage,
                HasNextPage = customer.HasNextPage,
                HasPreviousPage = customer.HasPreviousPage,
                PageSize = customer.PageSize,
                Items = customer.Items.Select(x => new ProfileDto
                {
                    Id = x.Id,
                    FirstName = x.Profile.FirstName,
                    LastName = x.Profile.LastName,
                    MiddleName = x.Profile.MiddleName,
                    Gender = x.Profile.Gender
                }).ToList()

            };
            return new BaseResponse<PaginationDto<ProfileDto>>
            {
                Message = "All customers",
                Status = true,
                Data = pagination
            };
        }

        public async Task<Profile> GetAsync(Guid id)
        {
            return await _profileRepository.GetAsync(x => x.Id == id);
        }

        public async Task<BaseResponse<ProfileDto>> UpdateAsync(UpdateProfileRequestModel model)
        {
            var customer = await _customerRepository.GetAsync(model.CustomerId);
            if(customer == null)
            {
                return new BaseResponse<ProfileDto>
                {
                    Message = "Customer not found",
                    Status = false,
                    Data = null
                };
            }
            if (customer.Profile == null)
            {
                return new BaseResponse<ProfileDto>
                {
                    Message = "Profile not found",
                    Status = false,
                    Data = null
                };
            }
            customer.Profile.FirstName = model.FirstName;
            customer.Profile.LastName = model.LastName;
            customer.Profile.MiddleName = model.MiddleName;
            customer.Profile.ImageUrl = model.ImageUrl;
            _profileRepository.Update(customer.Profile);
            await _unitOfWork.SaveChangesAsync();
            return new BaseResponse<ProfileDto>
            {
                Message = "Profile Updated Successfully",
                Status = true,
                Data = new ProfileDto
                {
                    Id = customer.Profile.Id,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MiddleName = model.MiddleName,
                    ImageUrl = model.ImageUrl,
                    Gender = customer.Profile.Gender
                }
            };
        }
    }
}
