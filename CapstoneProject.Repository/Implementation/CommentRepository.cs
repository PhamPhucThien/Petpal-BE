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
    public class CommentRepository : RepositoryGeneric<Comment>, ICommentRepository
    {
        private PetpalDbContext _dbContext;
        public CommentRepository(DbContextOptions<PetpalDbContext> contextOptions) : base(contextOptions)
        {
            _dbContext = new PetpalDbContext(contextOptions);
        }

        public async Task<List<Comment>> GetWithPaging(Paging paging)
        {
            if (paging == null)
            {
                throw new ArgumentNullException(nameof(paging));
            }
            
            IQueryable<Comment> query = _dbContext.Set<Comment>() 
                    .Include(o => o.User)
                    .Include(o => o.ParentComment)
                    .AsQueryable()
                ;

            query = query.Skip(paging.Size * (paging.Page - 1))
                .Take(paging.Size);

            return await query.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(Guid id)
        {
            return _dbContext.Comments.AsNoTracking().Where(o => o.Id.Equals(id))
                .Include(o => o.User)
                .Include(o => o.ParentComment)
                .FirstOrDefault();
        }
    }
}
