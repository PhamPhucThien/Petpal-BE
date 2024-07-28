using CapstoneProject.Database;
using CapstoneProject.Database.Model;
using CapstoneProject.Database.Model.Meta;
using CapstoneProject.DTO.Request;
using CapstoneProject.Repository.Generic;
using CapstoneProject.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Repository.Repository
{
    public class PackageRepository(DbContextOptions<PetpalDbContext> contextOptions) : RepositoryGeneric<Package>(contextOptions), IPackageRepository
    {
        private readonly DbContextOptions<PetpalDbContext> _contextOptions = contextOptions;

        public async Task<Package?> GetByIdIncludePackageItem(Guid packageId)
        {
            using PetpalDbContext context = new(_contextOptions);
            Package? entity = await context.Set<Package>().Include(p => p.PackageItems).FirstOrDefaultAsync(x => x.Id == packageId);
            if (entity == null)
            {
                Console.WriteLine($"Entity with ID {packageId} not found.");
            }
            return entity;
        }

        public async Task<List<Package>> GetWithPagingByCareCenterId(Guid careCenterId, Paging pagingRequest)
        {
            using PetpalDbContext context = new(_contextOptions);

            ArgumentNullException.ThrowIfNull(pagingRequest);

            IQueryable<Package> query = context.Set<Package>().Where(x => x.CareCenterId == careCenterId).AsQueryable();

            query = query.Skip(pagingRequest.Size * (pagingRequest.Page - 1))
                         .Take(pagingRequest.Size);

            return await query.ToListAsync();
        }
    }
}
