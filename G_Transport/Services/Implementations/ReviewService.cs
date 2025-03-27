using G_Transport.Dtos;
using G_Transport.Models.Domain;
using G_Transport.Repositories.Implementations;
using G_Transport.Repositories.Interfaces;
using G_Transport.Services.Interfaces;
using Org.BouncyCastle.Asn1.Ocsp;

namespace G_Transport.Services.Implementations
{
    public class ReviewService : IReviewService
    {
        private readonly ITripRepository _tripRepository;
        private readonly ICurrentUser _currentUser;
        private readonly ICustomerRepository _customerRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ReviewService(ITripRepository tripRepository, ICurrentUser currentUser, ICustomerRepository customerRepository, IReviewRepository reviewRepository,IUnitOfWork unitOfWork)
        {
            _tripRepository = tripRepository;
            _currentUser = currentUser;
            _customerRepository = customerRepository;
            _reviewRepository = reviewRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponse<ReviewDto>> CreateAsync(CreateReviewDto model)
        {
            var trip = await _tripRepository.GetAsync(model.TripId);
            if(trip == null)
            {
                return new BaseResponse<ReviewDto>
                {
                    Message = "Trip not found",
                    Status = false,
                    Data = null
                };
            }
            var user = _currentUser.GetCurrentUser();
            var customer = await _customerRepository.GetAsync(x => x.Email == user);

            var review = new Review
            {
                TripId = trip.Id,
                Trip = trip,
                CustomerId = customer!.Id,
                Customer = customer,
                Rating = model.Rating,
                Comment = model.Comment
            };
            await _reviewRepository.CreateAsync(review);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponse<ReviewDto>
            {
                Message = "Trip Reviewed Successfully",
                Status = true,
                Data = new ReviewDto
                {
                    Id = review.Id,
                    TripId = trip.Id,
                    CustomerId= customer!.Id,
                    Comment = model.Comment,
                    Rating = model.Rating,
                }
            };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var review = await _reviewRepository.GetAsync(id);

            if (review == null)
            {
                return false;
            }

            review.IsDeleted = true;
            _reviewRepository.Update(review); // Use BaseRepository Update method
            await _unitOfWork.SaveChangesAsync(); // Save changes

            return true;
        }

        public async Task<BaseResponse<PaginationDto<ReviewDto>>> GetAllAsync(PaginationRequest request)
        {
            var review = await _reviewRepository.GetAllAsync(request);
            if (review.Items.Count() == 0 || review == null)
            {
                return new BaseResponse<PaginationDto<ReviewDto>>
                {
                    Status = false,
                    Message = "No trips found",
                    Data = null
                };
            }
            var result = new PaginationDto<ReviewDto>
            {
                TotalItems = review.TotalItems,
                TotalPages = review.TotalPages,
                CurrentPage = review.CurrentPage,
                HasNextPage = review.HasNextPage,
                HasPreviousPage = review.HasPreviousPage,
                PageSize = review.PageSize,
                Items = review.Items.Select(x => new ReviewDto
                {
                    Id = x.Id,
                    CustomerId = x.Id,
                    Comment = x.Comment,
                    Rating = x.Rating,
                    TripId = x.TripId

                }).ToList()
            };
            return new BaseResponse<PaginationDto<ReviewDto>>
            {
                Status = true,
                Message = "Trips retrieved successfully",
                Data = result
            };
        }

        public async Task<BaseResponse<PaginationDto<ReviewDto>>> GetAllCustomerReviewsAsync(PaginationRequest request)
        {
            var user = _currentUser.GetCurrentUser();
            var customer = await _customerRepository.GetAsync(x => x.Email == user);
            var review = await _reviewRepository.GetAllAsync(x => x.CustomerId == customer!.Id, request);
            if (review.Items.Count() == 0 || review == null)
            {
                return new BaseResponse<PaginationDto<ReviewDto>>
                {
                    Status = false,
                    Message = "No trips found",
                    Data = null
                };
            }
            var result = new PaginationDto<ReviewDto>
            {
                TotalItems = review.TotalItems,
                TotalPages = review.TotalPages,
                CurrentPage = review.CurrentPage,
                HasNextPage = review.HasNextPage,
                HasPreviousPage = review.HasPreviousPage,
                PageSize = review.PageSize,
                Items = review.Items.Select(x => new ReviewDto
                {
                    Id = x.Id,
                    CustomerId = x.Id,
                    Comment = x.Comment,
                    Rating = x.Rating,
                    TripId = x.TripId

                }).ToList()
            };
            return new BaseResponse<PaginationDto<ReviewDto>>
            {
                Status = true,
                Message = "Trips retrieved successfully",
                Data = result
            };
        }

        public Task<BaseResponse<TripDto>> UpdateAsync(UpdateTripRequestModel model)
        {
            throw new NotImplementedException();
        }
    }
}
