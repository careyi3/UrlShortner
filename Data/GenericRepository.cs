using System;
using System.Threading.Tasks;
using UrlShortner.Models.Entities;

namespace UrlShortner.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {

        private ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> FindAsync(Guid id)
        {
            return await _context.FindAsync<T>(id);
        }

        public async Task<T> CreateAsync(T entity)
        {
            var result = await _context.AddAsync<T>(entity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
    }
}