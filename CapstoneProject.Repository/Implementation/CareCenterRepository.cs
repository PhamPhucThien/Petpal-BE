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
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Repository.Repository
{
    public class CareCenterRepository(DbContextOptions<PetpalDbContext> contextOptions) : RepositoryGeneric<CareCenter>(contextOptions), ICareCenterRepository
    {
        private readonly DbContextOptions<PetpalDbContext> _contextOptions = contextOptions;

        public async Task<int> CountActiveCareCenter()
        {
            using PetpalDbContext context = new(_contextOptions);
            int count = await context.Set<CareCenter>().Where(x => x.Status == CareCenterStatus.ACTIVE).CountAsync();
            return count;
        }

        public async Task<int> CountActiveCareCenterByPartnerId(Guid userId)
        {
            using PetpalDbContext context = new(_contextOptions);
            int count = await context.Set<CareCenter>().Where(x => x.PartnerId == userId && x.Status == CareCenterStatus.ACTIVE).CountAsync();
            return count;
        }

        public async Task<CareCenter?> GetByManagerId(Guid managerId)
        {
            using PetpalDbContext context = new(_contextOptions);
            CareCenter? careCenter = await context.Set<CareCenter>().Include(m => m.Partner).Include(n => n.Manager).FirstOrDefaultAsync(x => x.ManagerId == managerId);
            return careCenter;
        }

        public async Task<Tuple<List<CareCenter>, int>> GetByPartnerId(Guid partnerId, Paging paging)
        {
            ArgumentNullException.ThrowIfNull(paging);

            using PetpalDbContext context = new(_contextOptions);

            IQueryable<CareCenter> query = context.Set<CareCenter>().AsQueryable();

            if (paging.Size <= 0) { paging.Size = 1; }

            query = query.Include(m => m.Partner).Include(n => n.Manager).Where(x => x.PartnerId == partnerId).AsQueryable();

            if (paging.Search != null && paging.Search.Length > 0)
            {
                query = query.Where(x => x.CareCenterName != null && x.CareCenterName.Contains(paging.Search));
            }

            int count = await query.CountAsync();

            count = count % paging.Size == 0 ? count / paging.Size : count / paging.Size + 1;

            query = query.Skip(paging.Size * (paging.Page - 1))
                         .Take(paging.Size);

            List<CareCenter> list = await query.ToListAsync();

            Tuple<List<CareCenter>, int> data = new(list, count);

            return data;
        }

        public async Task<CareCenter?> GetCareCenterByIdAsync(Guid careCenterId)
        {
            using PetpalDbContext context = new(_contextOptions);
            CareCenter? careCenter = await context.Set<CareCenter>().Include(m => m.Partner).Include(n => n.Manager).FirstOrDefaultAsync(x => x.Id == careCenterId);
            return careCenter;
        }

        public async Task<Tuple<List<CareCenter>, int>> GetPendingWithPagingCustom(Paging paging)
        {
            ArgumentNullException.ThrowIfNull(paging);

            using PetpalDbContext context = new(_contextOptions);

            IQueryable<CareCenter> query = context.Set<CareCenter>().AsQueryable();

            query = query.Where(x => x.Status == CareCenterStatus.PENDING).AsQueryable();

            if (paging.Search != null && paging.Search.Length > 0)
            {
                query = query.Where(x => x.CareCenterName != null && x.CareCenterName.Contains(paging.Search));
            }

            int count = await query.CountAsync();

            count = count % paging.Size == 0 ? count / paging.Size : count / paging.Size + 1;

            query = query.Skip(paging.Size * (paging.Page - 1))
                         .Take(paging.Size);

            List<CareCenter> list = await query.ToListAsync();

            Tuple<List<CareCenter>, int> data = new(list, count);

            return data;
        }

        public async Task<Tuple<List<CareCenter>, int>> GetWithPagingCustom(Paging paging)
        {
            ArgumentNullException.ThrowIfNull(paging);

            using PetpalDbContext context = new(_contextOptions);

            IQueryable<CareCenter> query = context.Set<CareCenter>().AsQueryable();

            query = query.Where(x => x.Status == CareCenterStatus.ACTIVE).AsQueryable();

            if (paging.Search != null && paging.Search.Length > 0)
            {
                query = query.Where(x => x.CareCenterName != null && x.CareCenterName.Contains(paging.Search));
            }

            int count = await query.CountAsync();

            count = count % paging.Size == 0 ? count / paging.Size : count / paging.Size + 1;

            query = query.Skip(paging.Size * (paging.Page - 1))
                         .Take(paging.Size);

            List<CareCenter> list = await query.ToListAsync();

            Tuple<List<CareCenter>, int> data = new(list, count);

            return data;
        }
    }
}
