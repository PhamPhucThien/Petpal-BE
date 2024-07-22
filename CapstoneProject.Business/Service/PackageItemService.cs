using CapstoneProject.Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CapstoneProject.Database.Model;
using CapstoneProject.DTO.Request;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.Package;
using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.Package;
using CapstoneProject.Repository.Interface;

namespace CapstoneProject.Business.Service
{
    public class PackageItemService : IPackageItemService
    {
        private IPackageItemRepository _packageItemRepository;
        private IPackageRepository _packageRepository;
        private IServiceRepository _serviceRepository;
        private IMapper _mapper;

        public PackageItemService(IPackageItemRepository packageItemRepository, 
            IPackageRepository  packageRepository,  IServiceRepository serviceRepository,  IMapper  mapper)
        {
            _packageItemRepository = packageItemRepository;
            _packageRepository = packageRepository;
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }
        
        public async Task<BaseListResponse<PackageItemResponse>> GetList(ListRequest request)
        {
            Paging paging = new()
            {
                Page = request.Page,
                Size = request.Size,
                MaxPage = 1
            };
            var listPackageItem = await _packageItemRepository.GetWithPaging(paging);
            var listPackageItemResponse = _mapper.Map<List<PackageItemResponse>>(listPackageItem);
            paging.Total = listPackageItemResponse.Count;
            BaseListResponse<PackageItemResponse> response = new()
            {
                List = listPackageItemResponse,
                Paging = paging,
            };
            return response;
        }

        public async Task<PackageItemResponse> GetById(string packageItemId)
        {
            var packageItem = await _packageItemRepository.GetByIdAsync(Guid.Parse(packageItemId));
            if (packageItem == null)
            {
                throw new Exception("Not found package item with this id");
               
            }
            var packageItemResponse = _mapper.Map<PackageItemResponse>(packageItem);
            return packageItemResponse;
        }

        public async Task<PackageItemResponse> Create(PackageItemCreateRequest request)
        {
            if (request.PackageId != null)
            {
                var packageCheck = await _packageRepository.GetByIdAsync(Guid.Parse(request.PackageId));
                if (packageCheck == null)
                {
                    throw new Exception("Package id is invalid.");
                }
            }
            
            var serviceCheck = await _serviceRepository.GetByIdAsync(Guid.Parse(request.ServiceId));
            if (serviceCheck == null)
            {
                throw new Exception("Service id is invalid.");
            }
            
            var packageItemCreate = _mapper.Map<PackageItem>(request);
            packageItemCreate.CreatedAt = DateTimeOffset.Now;
            var result = await _packageItemRepository.AddAsync(packageItemCreate);
            var packageItem = await _packageItemRepository.GetByIdAsync(result.Id);
            return _mapper.Map<PackageItemResponse>(packageItem);
        }

        public async Task<PackageItemResponse> Update(PackageItemUpdateRequest request)
        {
            var packageItemCheck = await _packageItemRepository.GetByIdAsync(Guid.Parse(request.Id));
            if (packageItemCheck == null)
            {
                throw new Exception("Package Item id is invalid.");
            }
            
            var packageCheck = await _packageRepository.GetByIdAsync(Guid.Parse(request.PackageId));
            if (packageCheck == null) 
            { 
                throw new Exception("Package id is invalid.");
            }
            
            var serviceCheck = await _serviceRepository.GetByIdAsync(Guid.Parse(request.ServiceId));
            if (serviceCheck == null)
            {
                throw new Exception("Service id is invalid.");
            }

            var packageItemUpdate = _mapper.Map<PackageItem>(request);
            packageItemUpdate.CreatedAt = packageItemCheck.CreatedAt;
            packageItemUpdate.CreatedBy = packageItemCheck.CreatedBy;
            packageItemUpdate.UpdatedAt = DateTimeOffset.Now;
            var result = await _packageItemRepository.EditAsync(packageItemUpdate);
            var packageItem =  await _packageItemRepository.GetByIdAsync(Guid.Parse(request.Id));
            return result ? _mapper.Map<PackageItemResponse>(packageItem) : null;
        }
    }
}
