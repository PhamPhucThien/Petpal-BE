using CapstoneProject.Database;
using CapstoneProject.Database.Model;
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
    public class OrderDetailRepository(DbContextOptions<PetpalDbContext> contextOptions) : RepositoryGeneric<OrderDetail>(contextOptions), IOrderDetailRepository
    {
        private readonly DbContextOptions<PetpalDbContext> _contextOptions = contextOptions;

        public async Task<OrderDetail?> GetByIdAsyncCustom(Guid petId)
        {
            using PetpalDbContext context = new(_contextOptions);

            IQueryable<OrderDetail> query = context.Set<OrderDetail>().AsQueryable();

            query = query.
                Include(od => od.Pet).
                Include(od => od.Order).
                Include(od => od.Package).
                    ThenInclude(p => p.CareCenter).
                    ThenInclude(c => c.Staffs).
                Where(x => x.Pet != null && x.Pet.Id == petId && x.Status == Database.Model.Meta.BaseStatus.ACTIVE).
                AsQueryable();

            OrderDetail? data = await query.FirstOrDefaultAsync();

            return data;
        }
    }
}
