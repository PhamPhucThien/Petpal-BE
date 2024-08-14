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

        public async Task<CareCenter?> GetByManagerId(Guid managerId)
        {
            using PetpalDbContext context = new(_contextOptions);
            CareCenter? careCenter = await context.Set<CareCenter>().Include(m => m.Partner).Include(n => n.Manager).FirstOrDefaultAsync(x => x.ManagerId == managerId);
            return careCenter;
        }

        public async Task<Tuple<List<CareCenter>, int>> GetByPartnerId(Guid partnerId, Paging pagingRequest)
        {
            ArgumentNullException.ThrowIfNull(pagingRequest);

            using PetpalDbContext context = new(_contextOptions);

            IQueryable<CareCenter> query = context.Set<CareCenter>().AsQueryable();

            int count = await query.CountAsync();

            count = count % pagingRequest.Size == 0 ? count / pagingRequest.Size : count / pagingRequest.Size + 1;

            query = query.Include(m => m.Partner).Include(n => n.Manager).Where(x => x.PartnerId == partnerId).AsQueryable();

            query = query.Skip(pagingRequest.Size * (pagingRequest.Page - 1))
                         .Take(pagingRequest.Size);

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

        public async Task<Tuple<List<CareCenter>, int>> GetWithPagingCustom(Paging pagingRequest)
        {
            ArgumentNullException.ThrowIfNull(pagingRequest);

            using PetpalDbContext context = new(_contextOptions);

            IQueryable<CareCenter> query = context.Set<CareCenter>().AsQueryable();

            int count = await query.CountAsync();

            count = count % pagingRequest.Size == 0 ? count / pagingRequest.Size : count / pagingRequest.Size + 1;

            query = query.Where(x => x.Status == CareCenterStatus.ACTIVE).AsQueryable();

            if (pagingRequest.Search != null && pagingRequest.Search.Length > 0)
            {
                query = query.Where(x => x.CareCenterName != null && x.CareCenterName.Contains(pagingRequest.Search));
            }

            query = query.Skip(pagingRequest.Size * (pagingRequest.Page - 1))
                         .Take(pagingRequest.Size);

            List<CareCenter> list = await query.ToListAsync();

            Tuple<List<CareCenter>, int> data = new(list, count);

            return data;
        }
    }
}
