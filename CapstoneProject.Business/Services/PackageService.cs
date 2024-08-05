using AutoMapper;
using CapstoneProject.Business.Interfaces;
using CapstoneProject.Database.Model;
using CapstoneProject.DTO;
using CapstoneProject.DTO.Request;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.Package;
using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.Package;
using CapstoneProject.Repository.Interface;
using CapstoneProject.Repository.Repository;
using System.Transactions;

namespace CapstoneProject.Business.Services
{
    public class PackageService(IPackageRepository packageRepository, ICareCenterRepository careCenterRepository,
            IMapper mapper, IServiceRepository serviceRepository, IPackageItemRepository packageItemRepository) : IPackageService
    {
        private readonly ICareCenterRepository _careCenterRepository = careCenterRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IPackageRepository _packageRepository = packageRepository;
        private readonly IServiceRepository _serviceRepository = serviceRepository;
        private readonly IPackageItemRepository _packageItemRepository = packageItemRepository;
        public StatusCode StatusCode { get; set; } = new();

        public async Task<BaseListResponse<PackageResponse>> GetList(ListRequest request)
        {
            Paging paging = new()
            {
                Page = request.Page,
                Size = request.Size,
                MaxPage = 1
            };
            List<Package> listPackage = await _packageRepository.GetWithPaging(paging);
            List<PackageResponse> listPackageResponse = _mapper.Map<List<PackageResponse>>(listPackage);
            paging.Total = listPackageResponse.Count;
            BaseListResponse<PackageResponse> response = new()
            {
                List = listPackageResponse,
                Paging = paging,
            };
            return response;
        }

        /*public async Task<PackageResponse> GetById(string packageId)
        {
            var package = await _packageRepository.GetByIdAsync(Guid.Parse(packageId));
            if (package == null)
            {
                throw new Exception("Not found package with this id");
               
            }
            var packageResponse = _mapper.Map<PackageResponse>(package);
            return packageResponse;
        }*/

        public async Task<PackageResponse> Create(PackageCreareRequest request)
        {
            CareCenter? careCenter = await _careCenterRepository.GetByIdAsync(Guid.Parse(request.CareCenterId));
            if (careCenter == null)
            {
                throw new Exception("Carecenter id is invalid.");
            }

            Package packageCreate = _mapper.Map<Package>(request);
            packageCreate.CreatedAt = DateTimeOffset.Now;
            Package? result = await _packageRepository.AddAsync(packageCreate);
            Package? package = await _packageRepository.GetByIdAsync(result.Id);
            return _mapper.Map<PackageResponse>(package);
        }

        public async Task<PackageResponse?> Update(PackageUpdateRequest request)
        {
            Package? packageCheck = await _packageRepository.GetByIdAsync(Guid.Parse(request.Id));
            if (packageCheck == null)
            {
                throw new Exception("ID is invalid.");
            }

            CareCenter? careCenter = await _careCenterRepository.GetByIdAsync(Guid.Parse(request.CareCenterId));
            if (careCenter == null)
            {
                throw new Exception("Carecenter id is invalid.");
            }

            Package packageUpdate = _mapper.Map<Package>(request);
            packageUpdate.CreatedAt = packageCheck.CreatedAt;
            packageUpdate.CreatedBy = packageCheck.CreatedBy;
            packageUpdate.UpdatedAt = DateTimeOffset.Now;
            bool result = await _packageRepository.EditAsync(packageUpdate);
            Package? package = await _packageRepository.GetByIdAsync(packageUpdate.Id);
            return result ? _mapper.Map<PackageResponse>(package) : null;
        }

        public Task<bool> DataGenerator()
        {
            throw new NotImplementedException();
        }


        public async Task<ResponseObject<PackageResponseModel>> GetById(GetPackageByIdRequest request)
        {
            ResponseObject<PackageResponseModel> response = new();

            Package? package = await _packageRepository.GetByIdIncludePackageItem(request.Id);

            if (package == null)
            {
                response.Status = StatusCode.NotFound;
                response.Payload.Message = "Id không tồn tại";
            }
            else
            {
                response.Status = StatusCode.OK;
                response.Payload.Message = "Lấy dữ liệu thành công";

                PackageResponseModel packageResponse = new()
                {
                    Id = package.Id,
                    TotalPrice = package.TotalPrice,
                    Title = package.Title,
                    Image = package.Image,
                    Duration = package.Duration,
                    Description = package.Description,
                    Type = package.Type
                };

                List<ListPackageItemResponseModel> list = [];

                foreach (PackageItem packageItem in package.PackageItems)
                {
                    ListPackageItemResponseModel item = new()
                    {
                        Id = packageItem.Id,
                        CurrentPrice = packageItem.CurrentPrice,
                        Detail = packageItem.Detail
                    };
                    list.Add(item);
                }
                packageResponse.Items = list;
                response.Payload.Data = packageResponse;
            }

            return response;
        }

        public async Task<ResponseObject<ListPackageResponse>> GetListByCareCenterId(GetListPackageByCareCenterIdRequest request)
        {
            ResponseObject<ListPackageResponse> response = new();

            Paging paging = new()
            {
                Page = request.Page,
                Size = request.Size,
                MaxPage = 1
            };

            List<Package> list = await _packageRepository.GetWithPagingByCareCenterId(request.CareCenterId, paging);

            if (list == null || list.Count == 0)
            {
                response.Status = StatusCode.NotFound;
                response.Payload.Message = "Dữ liệu không tồn tại";
            }
            else
            {
                ListPackageResponse data = new();
                List<PackageResponseModel> listModel = [];

                foreach (Package item in list)
                {
                    PackageResponseModel model = new()
                    {
                        Id = item.Id,
                        TotalPrice = item.TotalPrice,
                        Title = item.Title,
                        Image = item.Image,
                        Duration = item.Duration,
                        Description = item.Description,
                        Type = item.Type
                    };
                    listModel.Add(model);
                }
                data.List.AddRange(listModel);


                response.Status = StatusCode.OK;
                response.Payload.Message = "Lấy dữ liệu thành công";

                data.Paging = paging;
                data.Paging.Total = paging.Total;

                response.Payload.Data = data;
            }



            return response;
        }
    }
}
