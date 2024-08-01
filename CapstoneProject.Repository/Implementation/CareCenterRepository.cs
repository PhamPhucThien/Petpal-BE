using CapstoneProject.Database;
using CapstoneProject.Database.Model;
using CapstoneProject.Database.Model.Meta;
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

        public async Task<CareCenter?> GetByPartnerId(Guid partnerId)
        {
            using PetpalDbContext context = new(_contextOptions);
            CareCenter? careCenter = await context.Set<CareCenter>().Include(m => m.Manager).FirstOrDefaultAsync(x => x.PartnerId == partnerId);
            return careCenter;
        }

        public async Task<CareCenter?> GetCareCenterByIdAsync(Guid careCenterId)
        {
            using PetpalDbContext context = new(_contextOptions);
            CareCenter? careCenter = await context.Set<CareCenter>().Include(m => m.Manager).FirstOrDefaultAsync(x => x.Id == careCenterId);
            return careCenter;
        }
    }
}
