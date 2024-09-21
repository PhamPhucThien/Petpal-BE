using CapstoneProject.Business.Interfaces;
using CapstoneProject.Database.Model.Meta;
using CapstoneProject.DTO;
using CapstoneProject.DTO.Response.Dashboard;
using CapstoneProject.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Business.Services
{
    public class DashboardService(IUserRepository userRepository, ICareCenterRepository careCenterRepository, IInvoiceRepository invoiceRepository, IOrderRepository orderRepository, IPackageRepository packageRepository) : IDashboardService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ICareCenterRepository _careCenterRepository = careCenterRepository;
        private readonly IInvoiceRepository _invoiceRepository = invoiceRepository;
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IPackageRepository _packageRepository = packageRepository;

        public StatusCode StatusCode { get; set; } = new();
        public async Task<ResponseObject<AdminDashboard>> GetDashBoardAdmin()
        {
            ResponseObject<AdminDashboard> response = new();
            AdminDashboard data = new()
            {
                Users = await _userRepository.CountActiveUser(),
                Partners = await _userRepository.CountActivePartner(),
                CareCenters = await _careCenterRepository.CountActiveCareCenter(),
                Invoices = await _invoiceRepository.CountActiveInvoice()
            };

            response.Payload.Data = data;
            response.Payload.Message = "Lấy dữ liệu thành công";
            response.Status = StatusCode.OK;

            return response;
        }

        public async Task<ResponseObject<PartnerDashboard>> GetDashBoardPartner(Guid userId)
        {
            ResponseObject<PartnerDashboard> response = new();
            PartnerDashboard data = new()
            {
                CareCenters = await _careCenterRepository.CountActiveCareCenterByPartnerId(userId),
                Orders = await _orderRepository.CountOrderByPartnerId(userId),
                Customers = await _userRepository.CountCustomerByPartnerId(userId),
                UsingPackages = await _packageRepository.CountUsingPackageByPartnerId(userId)
            };

            response.Payload.Data = data;
            response.Payload.Message = "Lấy dữ liệu thành công";
            response.Status = StatusCode.OK;

            return response;
        }
    }
}
