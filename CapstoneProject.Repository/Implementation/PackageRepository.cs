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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CapstoneProject.Repository.Repository
{
    public class PackageRepository(DbContextOptions<PetpalDbContext> contextOptions) : RepositoryGeneric<Package>(contextOptions), IPackageRepository
    {
        private readonly DbContextOptions<PetpalDbContext> _contextOptions = contextOptions;

        public async Task<int> CountUsingPackageByPartnerId(Guid userId)
        {
            using PetpalDbContext context = new(_contextOptions);

            List<CareCenter> careCenters = await context.Set<CareCenter>().Where(x => x.PartnerId == userId).ToListAsync();

            List<Guid> listIds = [];

            foreach (CareCenter item in careCenters)
            {
                listIds.Add(item.Id);
            }

            List<Package> packages = await context.Set<Package>().Where(x => x.CareCenter != null && listIds.Any(y => y == x.CareCenter.Id)).ToListAsync();

            listIds.Clear();

            foreach (Package item in packages)
            {
                listIds.Add(item.Id);
            }

            List<OrderDetail> orderDetails = await context.Set<OrderDetail>().Include(x => x.Order).Where(x => x.Package != null && listIds.Any(y => y == x.Package.Id)).ToListAsync();

            listIds.Clear();

            foreach (OrderDetail item in orderDetails)
            {
                if (item.PackageId != null && !listIds.Contains((Guid)item.PackageId) && item.Order != null && item.Order.Status != OrderStatus.STOPPED && item.Order.Status != OrderStatus.ENDED)
                {
                    listIds.Add((Guid)item.PackageId);
                }
            }

            return listIds.Count();
        }

        public async Task<Tuple<List<Package>, int>> GetByCustomerId(Guid userId, Paging paging)
        {
            using PetpalDbContext context = new(_contextOptions);

            ArgumentNullException.ThrowIfNull(paging);

            IQueryable<Package> query = context.Set<Package>().AsQueryable();

            if (paging.Size <= 0) { paging.Size = 1; }
            
            query = query.
                Include(p => p.OrderDetails).
                ThenInclude(or => or.Pet).
                Where(x => x.OrderDetails != null && x.OrderDetails.Any(p => p.Pet != null && p.Pet.UserId == userId));

            int count = await query.CountAsync();

            count = count % paging.Size == 0 ? count / paging.Size : count / paging.Size + 1;

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

            if (paging.Size <= 0) { paging.Size = 1; }
            
            query = query.
                Include(p => p.CareCenter).
                ThenInclude(cc => cc!.Staffs).
                Include(p => p.OrderDetails).
                ThenInclude(or => or.Pet).
                Where(x => x.Status == BaseStatus.ACTIVE && x.CareCenter != null && x.CareCenter.Staffs != null && x.CareCenter.Staffs.Any(y => y.UserId == userId));

            int count = await query.CountAsync();

            count = count % paging.Size == 0 ? count / paging.Size : count / paging.Size + 1;

            query = query.Skip(paging.Size * (paging.Page - 1))
                         .Take(paging.Size);

            List<Package> list = await query.ToListAsync();

            Tuple<List<Package>, int> data = new(list, count);

            return data;
        }

        public async Task<Package?> GetByStaffIdAndPackageId(Guid userId, Guid packageId)
        {
            using PetpalDbContext context = new(_contextOptions);

            IQueryable<Package> query = context.Set<Package>().AsQueryable();

            query = query.
                Include(p => p.CareCenter).
                ThenInclude(cc => cc!.Staffs).
                Include(p => p.OrderDetails).
                ThenInclude(or => or.Pet).
                Where(x => x.Id == packageId && x.Status == BaseStatus.ACTIVE && x.CareCenter != null && x.CareCenter.Staffs != null && x.CareCenter.Staffs.Any(y => y.UserId == userId));


            Package? data = await query.FirstOrDefaultAsync();

            return data;
        }

        public async Task<Tuple<List<Package>, int>> GetWithPagingByCareCenterId(Guid careCenterId, Paging paging)
        {
            using PetpalDbContext context = new(_contextOptions);

            ArgumentNullException.ThrowIfNull(paging);

            IQueryable<Package> query = context.Set<Package>().AsQueryable();

            if (paging.Size <= 0) { paging.Size = 1; }
            
            query = query.Where(x => x.CareCenterId == careCenterId && x.Title != null && x.Title.Contains(paging.Search));

            int count = await query.CountAsync();

            count = count % paging.Size == 0 ? count / paging.Size : count / paging.Size + 1;

            query = query.Skip(paging.Size * (paging.Page - 1))
                         .Take(paging.Size);

            List<Package> list = await query.ToListAsync();

            Tuple<List<Package>, int> data = new(list, count);

            return data;
        }
    }
}
