using CapstoneProject.Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CapstoneProject.DTO.Request;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.Service;
using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.PetType;
using CapstoneProject.DTO.Response.Service;
using CapstoneProject.Repository.Interface;

namespace CapstoneProject.Business.Service
{
    public class ServiceService : IServiceService
    {
        private IServiceRepository _serviceRepository;
        private IMapper _mapper;

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
            var listService = await _serviceRepository.GetWithPaging(paging);
            var listServiceResponse = _mapper.Map<List<ServiceResponse>>(listService);
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
            var service = await _serviceRepository.GetByIdAsync(Guid.Parse(serviceId));
            if (service == null)
            {
                throw new Exception("Not found Service with this id");
            }
            var serviceResponse = _mapper.Map<ServiceResponse>(service);
            return serviceResponse;
        }

        public async Task<ServiceResponse> Create(ServiceCreateRequest request)
        {
            var serviceCreate = _mapper.Map<Database.Model.Service>(request);
            serviceCreate.CreatedAt = DateTimeOffset.Now;
            var result = await _serviceRepository.AddAsync(serviceCreate);
            var service = await _serviceRepository.GetByIdAsync(result.Id);
            return _mapper.Map<ServiceResponse>(service);
        }

        public async Task<ServiceResponse> Update(ServiceUpdateRequest request)
        {
            var serviceCheck = await _serviceRepository.GetByIdAsync(Guid.Parse(request.Id));
            if (serviceCheck == null)
            {
                throw new Exception("ID is invalid.");
            }
            var serviceUpdate = _mapper.Map<Database.Model.Service>(request);
            serviceUpdate.CreatedAt = serviceCheck.CreatedAt;
            serviceUpdate.CreatedBy = serviceCheck.CreatedBy;
            serviceUpdate.UpdatedAt = DateTimeOffset.Now;
            var result = await _serviceRepository.EditAsync(serviceUpdate);
            return result ? _mapper.Map<ServiceResponse>(serviceUpdate) : null;
        }
    }
}
