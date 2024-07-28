using CapstoneProject.Database;
using CapstoneProject.Database.Model;
using CapstoneProject.Repository.Generic;
using CapstoneProject.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapstoneProject.DTO.Request;

namespace CapstoneProject.Repository.Repository
{
    public class BlogRepository(DbContextOptions<PetpalDbContext> contextOptions) : RepositoryGeneric<Blog>(contextOptions), IBlogRepository
    {
        private readonly PetpalDbContext _dbContext = new(contextOptions);

        public async Task<Blog?> GetByIdAsync(Guid id)
        {
            return _dbContext.Blogs.AsNoTracking().Where(o => o.Id.Equals(id))
                .Include(o => o.User)
                .FirstOrDefault();
        }
        

        public async Task<List<Blog>> GetWithPaging(Paging pagingRequest)
        {
            ArgumentNullException.ThrowIfNull(pagingRequest);

            IQueryable<Blog> query = _dbContext.Set<Blog>() 
                    .Include(o => o.User)
                    .AsQueryable()
                ;

            query = query.Skip(pagingRequest.Size * (pagingRequest.Page - 1))
                .Take(pagingRequest.Size);

            return await query.ToListAsync();
        }
    }
}
