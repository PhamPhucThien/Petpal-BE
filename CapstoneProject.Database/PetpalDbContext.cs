using CapstoneProject.Database.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CapstoneProject.Database
{
    public class PetpalDbContext : DbContext
    {
        public DbSet<User>? Users { get; set; }
        public DbSet<Blog>? Blogs { get; set; }
        public DbSet<CareCenter>? CareCenters { get; set; }
        public DbSet<CareCenterStaff>? CareCenterStaffs { get; set; }
        public DbSet<Invoice>? Invoices { get; set; }
        public DbSet<Notification>? Notifications { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<OrderDetail>? OrderDetails { get; set; }
        public DbSet<Package>? Packages { get; set; }
        public DbSet<PackageItem>? PackageItems { get; set; }
        public DbSet<Pet>? Pets { get; set; }
        public DbSet<PetType>? PetTypes { get; set; }
        public DbSet<Service>? Services { get; set; }

        public PetpalDbContext(DbContextOptions<PetpalDbContext> options) : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }
    }
}
