using System.Linq;
using System.Linq.Expressions;
using G_Transport.Context;
using G_Transport.Dtos;
using G_Transport.Models.Domain;
using G_Transport.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace G_Transport.Repositories.Implementations
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(G_TransportContext context) : base(context)
        {
            
        }

        public async Task DeleteAsync(Guid id)
        {
            var customer = await _context.Set<Customer>().FirstOrDefaultAsync(a => a.Id == id);

            if (customer != null)
            {
                customer.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<PaginationDto<Customer>> GetAllAsync(PaginationRequest request)
        {
            try
            {
                var res =  _context.Set<Customer>()
                    .Include(c => c.Bookings)
                    .Include(c => c.Profile)
                    .Include(c => c.Payments)
                    .Include(c => c.Reviews)
                    .Where(c => c.IsDeleted == false)
                    .AsQueryable();
                var totalRecord = res.Count();
                var totalPages = totalRecord / request.PageSize;
                var items =  res.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize).ToList();
                return new PaginationDto<Customer>
                {
                    Items = items,
                    TotalItems = totalRecord,
                    TotalPages = totalPages,
                    HasNextPage = totalPages - request.CurrentPage == 0 ? false : true,
                    HasPreviousPage = request.CurrentPage - 1 == 0 ? false : true,
                    PageSize = request.PageSize,
                    CurrentPage = request.CurrentPage,
                  
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<Customer?> GetAsync(Guid id)
        {
            return await _context.Set<Customer>()
                .Include(c => c.Bookings)
                .Include(c => c.Payments)
                .Include(c => c.Profile)
                .Include(c => c.Reviews)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Customer?> GetAsync(Expression<Func<Customer,bool>> exp)
        {
            return await _context.Set<Customer>()
               .Include(c => c.Payments)
               .Include(c => c.Profile)
               .Include(c => c.Reviews)
               .Include(c => c.Bookings)
               .ThenInclude(c => c.Trip)
               .FirstOrDefaultAsync(exp);
        }
        public int GetAll(Expression<Func<Customer, bool>> exp)
        {
            return _context.Set<Customer>().Where(exp).Count();
        }
    }
}

