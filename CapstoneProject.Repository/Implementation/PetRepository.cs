using CapstoneProject.Database;
using CapstoneProject.Database.Model;
using CapstoneProject.Repository.Generic;
using CapstoneProject.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapstoneProject.DTO.Request;

namespace CapstoneProject.Repository.Repository
{
    public class PetRepository(DbContextOptions<PetpalDbContext> contextOptions) : RepositoryGeneric<Pet>(contextOptions), IPetRepository
    {
        private readonly DbContextOptions<PetpalDbContext> _contextOptions = contextOptions;

        public async Task<List<Pet>?> GetActiveByUserId(Guid userId, Paging paging)
        {
            ArgumentNullException.ThrowIfNull(paging);

            using PetpalDbContext context = new(_contextOptions);
            IQueryable<Pet> query = context.Set<Pet>().
                Where(x => x.UserId == userId && x.Status == Database.Model.Meta.PetStatus.ACTIVE).
                AsQueryable();

            query = query.Skip(paging.Size * (paging.Page - 1))
                         .Take(paging.Size);

            return await query.ToListAsync();
        }

        public async Task<Pet?> GetByIdAsync(Guid id)
        {
            using PetpalDbContext context = new(_contextOptions);

            return context.Pets.AsNoTracking().Where(o => o.Id.Equals(id))
                .Include(o => o.PetType)
                .Include(o => o.User)
                .FirstOrDefault();
        }

        public async Task<List<Pet>?> GetByUserId(Guid userId, Paging paging)
        {
            ArgumentNullException.ThrowIfNull(paging);

            using PetpalDbContext context = new(_contextOptions);
            IQueryable<Pet> query = context.Set<Pet>().
                Where(x => x.UserId == userId).
                AsQueryable();

            query = query.Skip(paging.Size * (paging.Page - 1))
                         .Take(paging.Size);

            return await query.ToListAsync();
        }

        public async Task<List<Pet>> GetWithPaging(Paging paging)
        {
            if (paging == null)
            {
                throw new ArgumentNullException(nameof(paging));
            }
            using PetpalDbContext context = new(_contextOptions);

            IQueryable<Pet> query = context.Set<Pet>() 
                    .Include(o => o.PetType)
                    .Include(o => o.User)
                    .AsQueryable()
               ;

            query = query.Skip(paging.Size * (paging.Page - 1))
                .Take(paging.Size);

            return await query.ToListAsync();
        }
    }
}
