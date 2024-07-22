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
    public class PackageRepository : RepositoryGeneric<Package>, IPackageRepository
    {
        private PetpalDbContext _dbContext;
        public PackageRepository(DbContextOptions<PetpalDbContext> contextOptions) : base(contextOptions)
        {
            _dbContext = new PetpalDbContext(contextOptions);
        }

        public async Task<List<Package>> GetWithPaging(Paging pagingRequest)
        {
            if (pagingRequest == null)
            {
                throw new ArgumentNullException(nameof(pagingRequest));
            }
            
            IQueryable<Package> query = _dbContext.Set<Package>() 
                    .Include(o => o.CareCenter)
                    .AsQueryable()
                ;

            query = query.Skip(pagingRequest.Size * (pagingRequest.Page - 1))
                .Take(pagingRequest.Size);

            return await query.ToListAsync();
        }

        public async Task<Package?> GetByIdAsync(Guid id)
        {
            return _dbContext.Packages.AsNoTracking().Where(o => o.Id.Equals(id))
                .Include(o => o.CareCenter)
                .FirstOrDefault();
        }
    }
}
