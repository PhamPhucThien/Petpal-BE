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
    public class PetRepository : RepositoryGeneric<Pet>, IPetRepository
    {
        private PetpalDbContext _dbContext;
        public PetRepository(DbContextOptions<PetpalDbContext> contextOptions) : base(contextOptions)
        {
            _dbContext = new PetpalDbContext(contextOptions);
        }

        public async Task<Pet?> GetByIdAsync(Guid id)
        {
            return _dbContext.Pets.AsNoTracking().Where(o => o.Id.Equals(id))
                .Include(o => o.PetType)
                .Include(o => o.User)
                .FirstOrDefault();
        }
        

        public async Task<List<Pet>> GetWithPaging(Paging pagingRequest)
        {
            if (pagingRequest == null)
            {
                throw new ArgumentNullException(nameof(pagingRequest));
            }
            
            IQueryable<Pet> query = _dbContext.Set<Pet>() 
                    .Include(o => o.PetType)
                    .Include(o => o.User)
                    .AsQueryable()
               ;

            query = query.Skip(pagingRequest.Size * (pagingRequest.Page - 1))
                .Take(pagingRequest.Size);

            return await query.ToListAsync();
        }
    }
}
