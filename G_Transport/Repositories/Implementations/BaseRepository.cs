using System.Linq.Expressions;
using G_Transport.Context;
using G_Transport.Models.Domain;
using G_Transport.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace G_Transport.Repositories.Implementations
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected G_TransportContext _context;
        public BaseRepository(G_TransportContext context)
        {
            _context = context;
        }
        public async Task<bool> CheckAsync(Expression<Func<T,bool>> exp)
        {
           return await _context.Set<T>().AnyAsync(exp);
        }

        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }


        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
