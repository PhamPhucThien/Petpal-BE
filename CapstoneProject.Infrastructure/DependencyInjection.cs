using CapstoneProject.Business.Interface;
using CapstoneProject.Business.Service;
using CapstoneProject.Database;
using CapstoneProject.Repository.Interface;
using CapstoneProject.Repository.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CapstoneProject.Infrastructure.Profile;
using CapstoneProject.Infrastructure.Jwt;

namespace CapstoneProject.Infrastructure
{
    public static class DependencyInjection
    {
        public static void ConfigureServiceManager(this IServiceCollection services)
        {
            _ = services.AddHttpContextAccessor();
            _ = services.AddScoped<IAuthService, AuthService>();
            _ = services.AddScoped<IUserService, UserService>();
            _ = services.AddScoped<IBlogService, BlogService>();
            _ = services.AddScoped<ICalendarService, CalendarService>();
            _ = services.AddScoped<ICareCenterService, CareCenterService>();
            _ = services.AddScoped<ICommentService, CommentService>();
            _ = services.AddScoped<IInvoiceService, InvoiceService>();
            _ = services.AddScoped<INotificationService, NotificationService>();
            _ = services.AddScoped<IOrderService, OrderService>();
            _ = services.AddScoped<IPackageItemService, PackageItemService>();
            _ = services.AddScoped<IPetService, PetService>();
            _ = services.AddScoped<IServiceService, ServiceService>();
            _ = services.AddScoped<IPetTypeService, PetTypeService>();
            _ = services.AddScoped<IPackageService, PackageService>();
            _ = services.AddScoped<IOrderDetailService, OrderDetailService>();

            _ = services.AddScoped<IUserRepository, UserRepository>();
            _ = services.AddScoped<IBlogRepository, BlogRepository>();
            _ = services.AddScoped<ICalendarRepository, CalendarRepository>();
            _ = services.AddScoped<ICareCenterRepository, CareCenterRepository>();
            _ = services.AddScoped<ICommentRepository, CommentRepository>();
            _ = services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            _ = services.AddScoped<INotificationRepository, NotificationRepository>();
            _ = services.AddScoped<IOrderRepository, OrderRepository>();
            _ = services.AddScoped<IPackageItemRepository, PackageItemRepository>();
            _ = services.AddScoped<IPetRepository, PetRepository>();
            _ = services.AddScoped<IServiceRepository, ServiceRepository>();
            _ = services.AddScoped<IPetTypeRepository, PetTypeRepository>();
            _ = services.AddScoped<IPackageRepository, PackageRepository>();
            _ = services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            _ = services.AddScoped<IAuthRepository, AuthRepository>();
            _ = services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            
            //add auto mapper
            _ = services.AddAutoMapper(typeof(UserProfle), typeof(BlogProfile), typeof(CalendarProfile),  typeof(CommentProfile), typeof(PackageProfile)
            , typeof(PackageItemProfile), typeof(PetProfile), typeof(PetTypeProfile), typeof(ServiceProfile));
        }

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(configuration);

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            _ = services.AddDbContext<PetpalDbContext>(opts =>
                opts.UseSqlServer(connectionString), ServiceLifetime.Transient);
        }
    }
}
