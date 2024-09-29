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
    public class PackageItemRepository(DbContextOptions<PetpalDbContext> contextOptions) : RepositoryGeneric<PackageItem>(contextOptions), IPackageItemRepository
    {
        private PetpalDbContext _dbContext = new PetpalDbContext(contextOptions);
        private readonly DbContextOptions<PetpalDbContext> _contextOptions = contextOptions;

        public async Task<PackageItem?> GetByIdAsync(Guid id)
        {
            return  _dbContext.PackageItems.AsNoTracking().Where(o => o.Id.Equals(id))
                .Include(o => o.Package)
                .Include(o => o.Service)
                .FirstOrDefault();
        }

        public async Task<List<PackageItem>> GetByPackageIdAsync(Guid packageId)
        {
            using PetpalDbContext context = new(_contextOptions);

            IQueryable<PackageItem> query = context.Set<PackageItem>().AsQueryable();

            query = query.Where(x => x.PackageId == packageId && x.Status == Database.Model.Meta.BaseStatus.ACTIVE);

            List<PackageItem> data = await query.ToListAsync();

            return data;
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
