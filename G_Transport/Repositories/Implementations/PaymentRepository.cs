using System.Linq.Expressions;
using G_Transport.Context;
using G_Transport.Dtos;
using G_Transport.Models.Domain;
using G_Transport.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;

namespace G_Transport.Repositories.Implementations
{
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(G_TransportContext context) : base(context)
        {
            _context = context;
        }
        public async Task<PaginationDto<Payment>> GetAllAsync(PaginationRequest request)
        {
            var payment = _context.Set<Payment>()
                          .Include(c => c.Trip)
                           .Include(c => c.Customer)
                          .AsQueryable();
            var totalRecord = payment.Count();
            var totalPages = totalRecord / request.PageSize;
            var result = await _context.Set<Payment>()
            .Skip((request.CurrentPage - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return new PaginationDto<Payment>
            {
                TotalPages = totalPages,
                TotalItems = totalRecord,
                Items = result,
                HasNextPage = totalPages / request.CurrentPage == 1 ? false : true,
                HasPreviousPage = request.CurrentPage - 1 == 0 ? false : true,
                PageSize = request.PageSize,
                CurrentPage = request.CurrentPage

            };
        }

        public async Task<PaginationDto<Payment>> GetAllAsync(Expression<Func<Payment, bool>> exp,PaginationRequest request)
        {
            var payment = _context.Set<Payment>()
                           .Include(c => c.Trip)
                           .ThenInclude(c => c.Bookings)
                           .Include(c => c.Customer)
                           .Where(exp)
                          .AsQueryable();
            var totalRecord = payment.Count();
            var totalPages = totalRecord / request.PageSize;
            var result = await _context.Set<Payment>()
            .Skip((request.CurrentPage - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return new PaginationDto<Payment>
            {
                TotalPages = totalPages,
                TotalItems = totalRecord,
                Items = result,
                HasNextPage = totalPages / request.CurrentPage == 1 ? false : true,
                HasPreviousPage = request.CurrentPage - 1 == 0 ? false : true,
                PageSize = request.PageSize,
                CurrentPage = request.CurrentPage

            };

        }

        public async Task<Payment?> GetAsync(Guid id)
        {
            return await _context.Set<Payment>()
               .Include(c => c.Trip)
               .Include(c => c.Customer)
               .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Payment?> GetAsync(Expression<Func<Payment, bool>> exp)
        {
            return await _context.Set<Payment>()
              .Include(c => c.Trip)
              .Include(c => c.Customer)
              .FirstOrDefaultAsync(exp);
        }
    }
}
