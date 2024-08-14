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
    public class PackageItemRepository : RepositoryGeneric<PackageItem>, IPackageItemRepository
    {
        private PetpalDbContext _dbContext;
        
        public PackageItemRepository(DbContextOptions<PetpalDbContext> contextOptions) : base(contextOptions)
        {
            _dbContext = new PetpalDbContext(contextOptions);
        }

        public async Task<PackageItem?> GetByIdAsync(Guid id)
        {
            return  _dbContext.PackageItems.AsNoTracking().Where(o => o.Id.Equals(id))
                .Include(o => o.Package)
                .Include(o => o.Service)
                .FirstOrDefault();
        }

        public async Task<List<PackageItem>> GetWithPaging(Paging paging)
        {
            if (paging == null)
            {
                throw new ArgumentNullException(nameof(paging));
            }
            
            IQueryable<PackageItem> query = _dbContext.Set<PackageItem>()
                    .Include(o => o.Package)
                    .Include(o => o.Service)
                    .AsQueryable()
                ;

            query = query.Skip(paging.Size * (paging.Page - 1))
                .Take(paging.Size);

            return await query.ToListAsync();
        }
    }
}
