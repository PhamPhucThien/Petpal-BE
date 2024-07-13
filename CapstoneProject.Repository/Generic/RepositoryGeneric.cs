using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Repository.Generic
{
    public class RepositoryGeneric<T> : IRepository<T> where T : class
    {
        private readonly DbContextOptions<FooDrinkDbContext> _contextOptions;

        public RepositoryGeneric(DbContextOptions<FooDrinkDbContext> contextOptions)
        {
            _contextOptions = contextOptions ?? throw new ArgumentNullException(nameof(contextOptions));
        }

        public async Task<T> AddAsync(T entity)
        {
            using (FooDrinkDbContext context = new(_contextOptions))
            {
                _ = await context.Set<T>().AddAsync(entity);
                _ = await context.SaveChangesAsync();
            }
            return entity;
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            using FooDrinkDbContext context = new(_contextOptions);
            T? entity = await context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _ = context.Set<T>().Remove(entity);
                _ = await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> EditAsync(T entity)
        {
            using FooDrinkDbContext context = new(_contextOptions);
            context.Entry(entity).State = EntityState.Modified;
            _ = await context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using FooDrinkDbContext context = new(_contextOptions);
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            using FooDrinkDbContext context = new(_contextOptions);
            T? entity = await context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                Console.WriteLine($"Entity with ID {id} not found.");
            }
            return entity;
        }

        /*public IEnumerable<T> GetWithPaging(IPagingRequest pagingRequest)
        {
            if (pagingRequest == null)
            {
                throw new ArgumentNullException(nameof(pagingRequest));
            }

            using FooDrinkDbContext context = new(_contextOptions);
            IQueryable<T> query = context.Set<T>().AsQueryable();

            query = query.Skip(pagingRequest.PageSize * (pagingRequest.PageIndex - 1))
                         .Take(pagingRequest.PageSize);

            return query.ToList();
        }*/
    }
}
