using CapstoneProject.Business.Interface;
using CapstoneProject.Business.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        }
    }
}
