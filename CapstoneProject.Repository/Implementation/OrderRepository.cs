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
    public class OrderRepository(DbContextOptions<PetpalDbContext> contextOptions) : RepositoryGeneric<Order>(contextOptions), IOrderRepository
    {
        private readonly DbContextOptions<PetpalDbContext> _contextOptions = contextOptions;
        public async Task<Tuple<List<Order>, int>> GetByUserId(Guid userId, Paging paging)
        {
            ArgumentNullException.ThrowIfNull(paging);
            using PetpalDbContext context = new(_contextOptions);

            IQueryable<Order> query = context.Set<Order>().AsQueryable();

            int count = await query.CountAsync();

            count = count % paging.Size == 0 ? count / paging.Size : count / paging.Size + 1;

            query = query.
                Include(od => od.OrderDetail).
                    ThenInclude(p => p.Package).         
                    ThenInclude(c => c.CareCenter).
                Include(od => od.OrderDetail).
                    ThenInclude(od => od.Pet).
                Where(x => x.UserId == userId).
                AsQueryable();

            query = query.Skip(paging.Size * (paging.Page - 1))
                         .Take(paging.Size);

            List<Order> list = await query.ToListAsync();

            Tuple<List<Order>, int> data = new(list, count);

            return data;
        }

        public async Task<Tuple<List<Order>, int>> GetByPartnerId(Guid userId, Paging paging)
        {
            ArgumentNullException.ThrowIfNull(paging);

            using PetpalDbContext context = new(_contextOptions);

            IQueryable<Order> query = context.Set<Order>().AsQueryable();

            int count = await query.CountAsync();

            count = count % paging.Size == 0 ? count / paging.Size : count / paging.Size + 1;

            query = query.
                Include(od => od.OrderDetail).
                    ThenInclude(p => p.Package).
                    ThenInclude(c => c.CareCenter).
                Include(od => od.OrderDetail).
                    ThenInclude(od => od.Pet).
                Where(x => x.OrderDetail != null && x.OrderDetail.Package != null && x.OrderDetail.Package.CareCenter != null && x.OrderDetail.Package.CareCenter.PartnerId == userId).
                AsQueryable();

            query = query.Skip(paging.Size * (paging.Page - 1))
                         .Take(paging.Size);

            List<Order> list = await query.ToListAsync();

            Tuple<List<Order>, int> data = new(list, count);

            return data;
        }
        public async Task<Tuple<List<Order>, int>> GetByManagerId(Guid userId, Paging paging)
        {
            ArgumentNullException.ThrowIfNull(paging);

            using PetpalDbContext context = new(_contextOptions);

            IQueryable<Order> query = context.Set<Order>().AsQueryable();

            int count = await query.CountAsync();

            count = count % paging.Size == 0 ? count / paging.Size : count / paging.Size + 1;

            query = query.
                Include(od => od.OrderDetail).
                    ThenInclude(p => p.Package).
                    ThenInclude(c => c.CareCenter).
                Include(od => od.OrderDetail).
                    ThenInclude(od => od.Pet).
                Where(x => x.OrderDetail != null && x.OrderDetail.Package != null && x.OrderDetail.Package.CareCenter != null && x.OrderDetail.Package.CareCenter.ManagerId == userId).
                AsQueryable();

            query = query.Skip(paging.Size * (paging.Page - 1))
                         .Take(paging.Size);

            List<Order> list = await query.ToListAsync();

            Tuple<List<Order>, int> data = new(list, count);

            return data;
        }

        public async Task<int> Count()
        {
            using PetpalDbContext context = new(_contextOptions);
            int count = await context.Set<Order>().CountAsync();
            return count;
        }

        public async Task<double?> CountMoney()
        {
            using PetpalDbContext context = new(_contextOptions);
            double? money = await context.Set<Order>().Where(a => a.Status == OrderStatus.PAID).SumAsync(x => x.CurrentPrice);
            return money;
        }

        public async Task<Order?> GetByOrderId(Guid id)
        {
            using PetpalDbContext context = new(_contextOptions);
            Order? order = await context.Set<Order>().Include(od => od.OrderDetail).
                    ThenInclude(p => p.Package).
                    ThenInclude(c => c.CareCenter).
                Include(od => od.OrderDetail).
                    ThenInclude(od => od.Pet).
                    FirstOrDefaultAsync(a => a.Id == id);
            return order;
        }

        public async Task<Tuple<List<Order>, int>> GetPendingByManagerId(Guid userId, Paging paging)
        {
            ArgumentNullException.ThrowIfNull(paging);

            using PetpalDbContext context = new(_contextOptions);

            IQueryable<Order> query = context.Set<Order>().AsQueryable();

            int count = await query.CountAsync();

            count = count % paging.Size == 0 ? count / paging.Size : count / paging.Size + 1;

            query = query.
                Include(od => od.OrderDetail).
                    ThenInclude(p => p.Package).
                    ThenInclude(c => c.CareCenter).
                Include(od => od.OrderDetail).
                    ThenInclude(od => od.Pet).
                Where(x => x.OrderDetail != null && x.OrderDetail.Package != null && x.OrderDetail.Package.CareCenter != null && x.OrderDetail.Package.CareCenter.ManagerId == userId && x.Status == OrderStatus.CREATED).
                AsQueryable();

            query = query.Skip(paging.Size * (paging.Page - 1))
                         .Take(paging.Size);

            List<Order> list = await query.ToListAsync();

            Tuple<List<Order>, int> data = new(list, count);

            return data;
        }
    }
}
