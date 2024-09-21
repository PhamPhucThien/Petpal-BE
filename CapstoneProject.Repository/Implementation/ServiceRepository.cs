using CapstoneProject.Database;
using CapstoneProject.Database.Model;
using CapstoneProject.DTO.Request;
using CapstoneProject.Repository.Generic;
using CapstoneProject.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Repository.Repository
{
    public class ServiceRepository(DbContextOptions<PetpalDbContext> contextOptions) : RepositoryGeneric<Service>(contextOptions), IServiceRepository
    {
        private readonly DbContextOptions<PetpalDbContext> _contextOptions = contextOptions;

        public async Task<List<Service>> GetByListIdsAsync(List<Guid> listIds, string creator)
        {
            using PetpalDbContext context = new(_contextOptions);

            List<Service> services = await context.Set<Service>().Where(x => 
                (x.CreatedBy != null && (x.CreatedBy.Equals(creator) || x.CreatedBy.Equals("Admin"))) &&
                listIds.Any(i => i == x.Id) && x.Status == Database.Model.Meta.BaseStatus.ACTIVE
            ).ToListAsync();

            return services;
        }

        public async Task<Service?> GetByUsernameAndName(string? username, string name)
        {
            Service? service = null;

            using PetpalDbContext context = new(_contextOptions);

            service = await context.Set<Service>().Where(x => x.CreatedBy == username && x.Name == name).FirstOrDefaultAsync();

            return service;
        }

        public async Task<Tuple<List<Service>, int>> GetWithPagingCustom(Paging paging, string creator)
        {
            ArgumentNullException.ThrowIfNull(paging);

            using PetpalDbContext context = new(_contextOptions);
            IQueryable<Service> query = context.Set<Service>().AsQueryable();

            if (paging.Size <= 0) { paging.Size = 1; }

            if (paging.Search != string.Empty)
            {
                query = query.Where(o => (o.Name != null && o.Name.Contains(paging.Search)));
            }

            int count = await query.CountAsync();

            count = count % paging.Size == 0 ? count / paging.Size : count / paging.Size + 1;

            query = query.Where(o => o.CreatedBy != null && (o.CreatedBy.Equals(creator) || o.CreatedBy.Equals("Admin")));

            query = query.Skip(paging.Size * (paging.Page - 1))
                         .Take(paging.Size);

            List<Service> services = await query.ToListAsync();

            Tuple<List<Service>, int> data = new(services, count);

            return data;
        }
    }
}
