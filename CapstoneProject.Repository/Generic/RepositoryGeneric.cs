﻿using CapstoneProject.Database;
using CapstoneProject.DTO.Request;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Repository.Generic
{
    public class RepositoryGeneric<T>(DbContextOptions<PetpalDbContext> contextOptions) : IRepository<T> where T : class
    {
        private readonly DbContextOptions<PetpalDbContext> _contextOptions = contextOptions ?? throw new ArgumentNullException(nameof(contextOptions));

        public async Task<T?> AddAsync(T entity)
        {
            using (PetpalDbContext context = new(_contextOptions))
            {
                _ = await context.Set<T>().AddAsync(entity);
                int check = await context.SaveChangesAsync();
                if (check > 0)
                {
                    return entity;
                } else
                {
                    return null;
                }
            }
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            using PetpalDbContext context = new(_contextOptions);
            T? entity = await context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _ = context.Set<T>().Remove(entity);
                _ = await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> EditAsync(T entity)
        {
            using PetpalDbContext context = new(_contextOptions);
            context.Entry(entity).State = EntityState.Modified;
            _ = await context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using PetpalDbContext context = new(_contextOptions);
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            using PetpalDbContext context = new(_contextOptions);
            T? entity = await context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                Console.WriteLine($"Entity with ID {id} not found.");
            }
            return entity;
        }

        public async Task<List<T>> GetWithPaging(Paging pagingRequest)
        {
            ArgumentNullException.ThrowIfNull(pagingRequest);

            using PetpalDbContext context = new(_contextOptions);
            IQueryable<T> query = context.Set<T>().AsQueryable();

            query = query.Skip(pagingRequest.Size * (pagingRequest.Page - 1))
                         .Take(pagingRequest.Size);

            return await query.ToListAsync();
        }
    }
}
