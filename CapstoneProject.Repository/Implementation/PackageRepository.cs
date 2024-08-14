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
using CapstoneProject.DTO.Request;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CapstoneProject.Repository.Repository
{
    public class PackageRepository(DbContextOptions<PetpalDbContext> contextOptions) : RepositoryGeneric<Package>(contextOptions), IPackageRepository
    {
        private readonly DbContextOptions<PetpalDbContext> _contextOptions = contextOptions;
        private PetpalDbContext _dbContext;

        public async Task<Tuple<List<Package>, int>> GetByCustomerId(Guid userId, Paging paging)
        {
            using PetpalDbContext context = new(_contextOptions);

            ArgumentNullException.ThrowIfNull(paging);

            IQueryable<Package> query = context.Set<Package>().AsQueryable();

            int count = await query.CountAsync();

            count = count % paging.Size == 0 ? count / paging.Size : count / paging.Size + 1;

            query = query.
                Include(p => p.OrderDetails).
                ThenInclude(or => or.Pet).
                Where(x => x.OrderDetails != null && x.OrderDetails.Any(p => p.Pet != null && p.Pet.UserId == userId));

            query = query.Skip(paging.Size * (paging.Page - 1))
                         .Take(paging.Size);

            List<Package> list = await query.ToListAsync();

            Tuple<List<Package>, int> data = new(list, count);

            return data;
        }

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

        public async Task<Tuple<List<Package>, int>> GetByStaffId(Guid userId, Paging paging)
        {
            using PetpalDbContext context = new(_contextOptions);

            ArgumentNullException.ThrowIfNull(paging);

            IQueryable<Package> query = context.Set<Package>().AsQueryable();

            int count = await query.CountAsync();

            count = count % paging.Size == 0 ? count / paging.Size : count / paging.Size + 1;

            query = query.
                Include(p => p.CareCenter).
                ThenInclude(cc => cc!.Staffs).
                Include(p => p.OrderDetails).
                ThenInclude(or => or.Pet).
                Where(x => x.CareCenter != null && x.CareCenter.Staffs != null && x.CareCenter.Staffs.Any(y => y.UserId == userId));

            query = query.Skip(paging.Size * (paging.Page - 1))
                         .Take(paging.Size);

            List<Package> list = await query.ToListAsync();

            Tuple<List<Package>, int> data = new(list, count);

            return data;
        }

        public async Task<Tuple<List<Package>, int>> GetWithPagingByCareCenterId(Guid careCenterId, Paging paging)
        {
            using PetpalDbContext context = new(_contextOptions);

            ArgumentNullException.ThrowIfNull(paging);

            IQueryable<Package> query = context.Set<Package>().AsQueryable();

            int count = await query.CountAsync();

            count = count % paging.Size == 0 ? count / paging.Size : count / paging.Size + 1;

            query = query.Where(x => x.CareCenterId == careCenterId);

            query = query.Skip(paging.Size * (paging.Page - 1))
                         .Take(paging.Size);

            List<Package> list = await query.ToListAsync();

            Tuple<List<Package>, int> data = new(list, count);

            return data;
        }
    }
}
