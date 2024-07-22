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
using CapstoneProject.DTO.Response.Calendar;
using CapstoneProject.DTO.Response.Package;
using CapstoneProject.Repository.Interface;
using CapstoneProject.Repository.Repository;

namespace CapstoneProject.Business.Service
{
    public class PackageService : IPackageService
    {
        private IPackageRepository _packageRepository;
        private ICareCenterRepository _careCenterRepository;
        private IMapper _mapper;
        
        public PackageService(IPackageRepository packageRepository, ICareCenterRepository careCenterRepository,
            IMapper mapper)
        {
            _packageRepository = packageRepository;
            _careCenterRepository = careCenterRepository;
            _mapper = mapper;
        }
        
        public async Task<BaseListResponse<PackageResponse>> GetList(ListRequest request)
        {
            Paging paging = new()
            {
                Page = request.Page,
                Size = request.Size,
                MaxPage = 1
            };
            var listPackage = await _packageRepository.GetWithPaging(paging);
            var listPackageResponse = _mapper.Map<List<PackageResponse>>(listPackage);
            paging.Total = listPackageResponse.Count;
            BaseListResponse<PackageResponse> response = new()
            {
                List = listPackageResponse,
                Paging = paging,
            };
            return response;
        }

        public async Task<PackageResponse> GetById(string packageId)
        {
            var package = await _packageRepository.GetByIdAsync(Guid.Parse(packageId));
            if (package == null)
            {
                throw new Exception("Not found package with this id");
               
            }
            var packageResponse = _mapper.Map<PackageResponse>(package);
            return packageResponse;
        }

        public async Task<PackageResponse> Create(PackageCreareRequest request)
        {
            var careCenter = await _careCenterRepository.GetByIdAsync(Guid.Parse(request.CareCenterId));
            if (careCenter == null)
            {
                throw new Exception("Carecenter id is invalid.");
            }
            
            var packageCreate = _mapper.Map<Package>(request);
            packageCreate.CreatedAt = DateTimeOffset.Now;
            var result = await _packageRepository.AddAsync(packageCreate);
            var package = await _packageRepository.GetByIdAsync(result.Id);
            return _mapper.Map<PackageResponse>(package);
        }

        public async Task<PackageResponse> Update(PackageUpdateRequest request)
        {
            var packageCheck = await _packageRepository.GetByIdAsync(Guid.Parse(request.Id));
            if (packageCheck == null)
            {
                throw new Exception("ID is invalid.");
            }
            
            var careCenter =  await _careCenterRepository.GetByIdAsync(Guid.Parse(request.CareCenterId));
            if (careCenter == null)
            {
                throw new Exception("Carecenter id is invalid.");
            }

            var packageUpdate = _mapper.Map<Package>(request);
            packageUpdate.CreatedAt = packageCheck.CreatedAt;
            packageUpdate.CreatedBy = packageCheck.CreatedBy;
            packageUpdate.UpdatedAt = DateTimeOffset.Now;
            var result = await _packageRepository.EditAsync(packageUpdate);
            var package = await _packageRepository.GetByIdAsync(packageUpdate.Id);
            return result ? _mapper.Map<PackageResponse>(package) : null;
        }
    }
}
