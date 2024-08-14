using AutoMapper;
using CapstoneProject.Business.Interfaces;
using CapstoneProject.Database.Model;
using CapstoneProject.DTO.Request;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.Package;
using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.Package;
using CapstoneProject.Repository.Interface;

namespace CapstoneProject.Business.Services
{
    public class PackageItemService : IPackageItemService
    {
        private readonly IPackageItemRepository _packageItemRepository;
        private readonly IPackageRepository _packageRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;

        public PackageItemService(IPackageItemRepository packageItemRepository,
            IPackageRepository packageRepository, IServiceRepository serviceRepository, IMapper mapper)
        {
            _packageItemRepository = packageItemRepository;
            _packageRepository = packageRepository;
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }

        public async Task<BaseListResponse<ListPackageItemResponse>> GetList(ListRequest request)
        {
            Paging paging = new()
            {
                Page = request.Page,
                Size = request.Size,
                MaxPage = 1
            };
            Tuple<List<PackageItem>, int> listPackageItem = await _packageItemRepository.GetWithPaging(paging);
            List<ListPackageItemResponse> listPackageItemResponse = _mapper.Map<List<ListPackageItemResponse>>(listPackageItem.Item1);
            paging.MaxPage = listPackageItem.Item2;
            BaseListResponse<ListPackageItemResponse> response = new()
            {
                List = listPackageItemResponse,
                Paging = paging,
            };
            return response;
        }

        public async Task<ListPackageItemResponse> GetById(string packageItemId)
        {
            PackageItem? packageItem = await _packageItemRepository.GetByIdAsync(Guid.Parse(packageItemId));
            if (packageItem == null)
            {
                throw new Exception("Not found package item with this id");

            }
            ListPackageItemResponse packageItemResponse = _mapper.Map<ListPackageItemResponse>(packageItem);
            return packageItemResponse;
        }

        public async Task<ListPackageItemResponse> Create(PackageItemCreateRequest request)
        {
            if (request.PackageId != null)
            {
                Package? packageCheck = await _packageRepository.GetByIdAsync(Guid.Parse(request.PackageId));
                if (packageCheck == null)
                {
                    throw new Exception("Package id is invalid.");
                }
            }

            Database.Model.Service? serviceCheck = await _serviceRepository.GetByIdAsync(Guid.Parse(request.ServiceId));
            if (serviceCheck == null)
            {
                throw new Exception("Service id is invalid.");
            }

            PackageItem packageItemCreate = _mapper.Map<PackageItem>(request);
            packageItemCreate.CreatedAt = DateTimeOffset.Now;
            PackageItem? result = await _packageItemRepository.AddAsync(packageItemCreate);
            PackageItem? packageItem = await _packageItemRepository.GetByIdAsync(result.Id);
            return _mapper.Map<ListPackageItemResponse>(packageItem);
        }

        public async Task<ListPackageItemResponse?> Update(PackageItemUpdateRequest request)
        {
            PackageItem? packageItemCheck = await _packageItemRepository.GetByIdAsync(Guid.Parse(request.Id));
            if (packageItemCheck == null)
            {
                throw new Exception("Package Item id is invalid.");
            }

            Package? packageCheck = await _packageRepository.GetByIdAsync(Guid.Parse(request.PackageId));
            if (packageCheck == null)
            {
                throw new Exception("Package id is invalid.");
            }

            Database.Model.Service? serviceCheck = await _serviceRepository.GetByIdAsync(Guid.Parse(request.ServiceId));
            if (serviceCheck == null)
            {
                throw new Exception("Service id is invalid.");
            }

            PackageItem packageItemUpdate = _mapper.Map<PackageItem>(request);
            packageItemUpdate.CreatedAt = packageItemCheck.CreatedAt;
            packageItemUpdate.CreatedBy = packageItemCheck.CreatedBy;
            packageItemUpdate.UpdatedAt = DateTimeOffset.Now;
            bool result = await _packageItemRepository.EditAsync(packageItemUpdate);
            PackageItem? packageItem = await _packageItemRepository.GetByIdAsync(Guid.Parse(request.Id));
            return result ? _mapper.Map<ListPackageItemResponse>(packageItem) : null;
        }
    }
}
