using System;
using System.Threading.Tasks;
using UrlShortner.Models.Entities;

namespace UrlShortner.Data
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> FindAsync(Guid id);

        Task<T> CreateAsync(T entity);
    }
}