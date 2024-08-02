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
    public class CareCenterRepository(DbContextOptions<PetpalDbContext> contextOptions) : RepositoryGeneric<CareCenter>(contextOptions), ICareCenterRepository
    {
        private readonly DbContextOptions<PetpalDbContext> _contextOptions = contextOptions;

        public async Task<CareCenter?> GetByManagerId(Guid managerId)
        {
            using PetpalDbContext context = new(_contextOptions);
            CareCenter? careCenter = await context.Set<CareCenter>().Include(m => m.Partner).Include(n => n.Manager).FirstOrDefaultAsync(x => x.ManagerId == managerId);
            return careCenter;
        }

        public async Task<List<CareCenter>?> GetByPartnerId(Guid partnerId, Paging pagingRequest)
        {
            ArgumentNullException.ThrowIfNull(pagingRequest);

            using PetpalDbContext context = new(_contextOptions);
            IQueryable<CareCenter> query = context.Set<CareCenter>().Include(m => m.Partner).Include(n => n.Manager).Where(x => x.PartnerId == partnerId).AsQueryable();

            query = query.Skip(pagingRequest.Size * (pagingRequest.Page - 1))
                         .Take(pagingRequest.Size);

            return await query.ToListAsync();
        }

        public async Task<CareCenter?> GetCareCenterByIdAsync(Guid careCenterId)
        {
            using PetpalDbContext context = new(_contextOptions);
            CareCenter? careCenter = await context.Set<CareCenter>().Include(m => m.Partner).Include(n => n.Manager).FirstOrDefaultAsync(x => x.Id == careCenterId);
            return careCenter;
        }

        public async Task<List<CareCenter>> GetWithPagingCustom(Paging pagingRequest)
        {
            ArgumentNullException.ThrowIfNull(pagingRequest);

            using PetpalDbContext context = new(_contextOptions);
            IQueryable<CareCenter> query = context.Set<CareCenter>().Where(x => x.Status == CareCenterStatus.ACTIVE).AsQueryable();

            query = query.Skip(pagingRequest.Size * (pagingRequest.Page - 1))
                         .Take(pagingRequest.Size);

            return await query.ToListAsync();
        }
    }
}
