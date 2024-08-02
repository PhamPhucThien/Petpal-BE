using AutoMapper;
using CapstoneProject.Business.Interfaces;
using CapstoneProject.DTO.Request;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.Service;
using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.Service;
using CapstoneProject.Repository.Interface;

namespace CapstoneProject.Business.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;

        public ServiceService(IServiceRepository serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }

        public async Task<BaseListResponse<ServiceResponse>> GetList(ListRequest request)
        {
            Paging paging = new()
            {
                Page = request.Page,
                Size = request.Size,
                MaxPage = 1
            };
            List<Database.Model.Service> listService = await _serviceRepository.GetWithPaging(paging);
            List<ServiceResponse> listServiceResponse = _mapper.Map<List<ServiceResponse>>(listService);
            paging.Total = listServiceResponse.Count;
            BaseListResponse<ServiceResponse> response = new()
            {
                List = listServiceResponse,
                Paging = paging,
            };
            return response;
        }

        public async Task<ServiceResponse> GetById(string serviceId)
        {
            Database.Model.Service? service = await _serviceRepository.GetByIdAsync(Guid.Parse(serviceId));
            if (service == null)
            {
                throw new Exception("Not found Service with this id");
            }
            ServiceResponse serviceResponse = _mapper.Map<ServiceResponse>(service);
            return serviceResponse;
        }

        public async Task<ServiceResponse> Create(ServiceCreateRequest request)
        {
            Database.Model.Service serviceCreate = _mapper.Map<Database.Model.Service>(request);
            serviceCreate.CreatedAt = DateTimeOffset.Now;
            Database.Model.Service? result = await _serviceRepository.AddAsync(serviceCreate);
            Database.Model.Service? service = await _serviceRepository.GetByIdAsync(result.Id);
            return _mapper.Map<ServiceResponse>(service);
        }

        public async Task<ServiceResponse?> Update(ServiceUpdateRequest request)
        {
            Database.Model.Service? serviceCheck = await _serviceRepository.GetByIdAsync(Guid.Parse(request.Id));
            if (serviceCheck == null)
            {
                throw new Exception("ID is invalid.");
            }
            Database.Model.Service serviceUpdate = _mapper.Map<Database.Model.Service>(request);
            serviceUpdate.CreatedAt = serviceCheck.CreatedAt;
            serviceUpdate.CreatedBy = serviceCheck.CreatedBy;
            serviceUpdate.UpdatedAt = DateTimeOffset.Now;
            bool result = await _serviceRepository.EditAsync(serviceUpdate);
            return result ? _mapper.Map<ServiceResponse>(serviceUpdate) : null;
        }
    }
}
