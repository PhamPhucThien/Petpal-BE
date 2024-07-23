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
    public class CalendarRepository : RepositoryGeneric<Calendar>, ICalendarRepository
    {
        private PetpalDbContext _dbContext;
        public CalendarRepository(DbContextOptions<PetpalDbContext> contextOptions) : base(contextOptions)
        {
            _dbContext = new PetpalDbContext(contextOptions);
        }

        public async Task<List<Calendar>> GetWithPaging(Paging pagingRequest)
        {
            if (pagingRequest == null)
            {
                throw new ArgumentNullException(nameof(pagingRequest));
            }
            
            IQueryable<Calendar> query = _dbContext.Set<Calendar>() 
                    .Include(o => o.CareCenter)
                    .AsQueryable()
                ;

            query = query.Skip(pagingRequest.Size * (pagingRequest.Page - 1))
                .Take(pagingRequest.Size);

            return await query.ToListAsync();
        }

        public async Task<Calendar?> GetByIdAsync(Guid id)
        {
            return _dbContext.Calendars.AsNoTracking().Where(o => o.Id.Equals(id))
                .Include(o => o.CareCenter)
                .FirstOrDefault();
        }
    }
}
