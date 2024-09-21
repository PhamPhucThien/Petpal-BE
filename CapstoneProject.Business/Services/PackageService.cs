using AutoMapper;
using CapstoneProject.Business.Interfaces;
using CapstoneProject.Database.Model;
using CapstoneProject.DTO;
using CapstoneProject.DTO.Request;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.Package;
using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.Package;
using CapstoneProject.DTO.Response.PackageItem;
using CapstoneProject.DTO.Response.Service;
using CapstoneProject.Repository.Interface;
using CapstoneProject.Repository.Repository;
using System.Transactions;

namespace CapstoneProject.Business.Services
{
    public class PackageService(IPackageRepository packageRepository, ICareCenterRepository careCenterRepository,
            IMapper mapper, IServiceRepository serviceRepository, IPackageItemRepository packageItemRepository,
            IUserRepository userRepository) : IPackageService
    {
        private readonly ICareCenterRepository _careCenterRepository = careCenterRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IPackageRepository _packageRepository = packageRepository;
        private readonly IServiceRepository _serviceRepository = serviceRepository;
        private readonly IPackageItemRepository _packageItemRepository = packageItemRepository;
        private readonly IUserRepository _userRepository = userRepository;
        public StatusCode StatusCode { get; set; } = new();
        public UploadImageService uploadImage { get; set; } = new();

        public async Task<ResponseObject<ListPackageResponse>> GetList(ListRequest request, Guid userId)
        {
            ResponseObject<ListPackageResponse> response = new();
            CareCenter? careCenter = await _careCenterRepository.GetByManagerId(userId);

            Paging paging = new()
            {
                Page = request.Page,
                Size = request.Size,
                Search = request.Search ?? "",
                MaxPage = 1
            };

            if (careCenter != null)
            {
                Tuple<List<Package>, int> list = await _packageRepository.GetWithPagingByCareCenterId(careCenter.Id, paging);

                if (list.Item1 == null || list.Item1.Count == 0)
                {
                    response.Status = StatusCode.NotFound;
                    response.Payload.Message = "Dữ liệu không tồn tại";
                }
                else
                {
                    ListPackageResponse data = new();
                    List<PackageResponseModel> listModel = [];

                    foreach (Package item in list.Item1)
                    {
                        PackageResponseModel model = new()
                        {
                            Id = item.Id,
                            TotalPrice = item.TotalPrice,
                            Title = item.Title,
                            Image = item.Image,
                            Description = item.Description,
                            Type = item.Type
                        };
                        listModel.Add(model);
                    }
                    data.List.AddRange(listModel);


                    response.Status = StatusCode.OK;
                    response.Payload.Message = "Lấy dữ liệu thành công";

                    data.Paging = paging;
                    data.Paging.MaxPage = list.Item2;

                    response.Payload.Data = data;
                }
            }
            else
            {
                response.Status = StatusCode.NotFound;
                response.Payload.Message = "Không tìm thấy trung tâm chăm sóc";
            }

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

        public async Task<ResponseObject<CreatePackageResponse>> Create(PackageCreateRequest request, Guid userId)
        {
            ResponseObject<CreatePackageResponse> response = new();
            CreatePackageResponse data = new();

            CareCenter? careCenter = await _careCenterRepository.GetByManagerId(userId);

            if (careCenter != null && careCenter.Partner != null && careCenter.Partner.Username != null && careCenter.Manager != null && careCenter.Manager.Username != null)
            {
                List<Guid> listIds = [];

                foreach (PackageItemModel model in request.PackageItems)
                {
                    listIds.Add(model.ServiceId);
                }

                List<Service> services = await _serviceRepository.GetByListIdsAsync(listIds, careCenter.Partner.Username);

                Package package = new();
                package.Id = Guid.NewGuid();
                double total = 0;

                using TransactionScope scope = new(TransactionScopeAsyncFlowOption.Enabled);

                List<PackageItem> packageItems = [];

                if (request.PackageItems.Count == services.Count)
                {
                    foreach (PackageItemModel model in request.PackageItems)
                    {
                        PackageItem item = new()
                        {
                            Id = Guid.NewGuid(),
                            PackageId = package.Id,
                            ServiceId = model.ServiceId,
                            ServiceName = services.Where(x => x.Id == model.ServiceId).First().Name ?? "",
                            CurrentPrice = model.CurrentPrice,
                            Detail = model.Detail,
                            Status = Database.Model.Meta.BaseStatus.ACTIVE,
                            CreatedAt = DateTime.UtcNow.AddHours(7),
                            CreatedBy = careCenter.Manager.Username
                        };

                        total += model.CurrentPrice;

                        packageItems.Add(item);
                    }

                    package.TotalPrice = total;
                    package.Title = request.Title;
                    package.Type = request.Type;
                    package.Description = request.Description;
                    package.CareCenterId = careCenter.Id;
                    package.Status = Database.Model.Meta.BaseStatus.ACTIVE;
                    package.CreatedAt = DateTime.UtcNow.AddHours(7);
                    package.CreatedBy = careCenter.Manager.Username;
                    package.PetTypeId = request.PetTypeId;

                    /*if (fileDetails.IsContain)
                    {
                        List<FileDetails> images = [fileDetails];

                        List<string> fileName = await uploadImage.UploadImage(images);

                        package.Image = String.Join(",", fileName);
                    }
*/
                    await _packageRepository.AddAsync(package);

                    foreach (PackageItem item in packageItems)
                    {
                        await _packageItemRepository.AddAsync(item);
                    }

                    scope.Complete();

                    data.PackageId = package.Id;
                    response.Status = StatusCode.OK;
                    response.Payload.Data = data;
                    response.Payload.Message = "Tạo gói dịch vụ thành công";
                    
                }
                else
                {
                    response.Status = StatusCode.NotFound;
                    response.Payload.Message = "Tồn tại dịch vụ không thuộc về trung tâm này";
                }
            }
            else
            {
                response.Status = StatusCode.NotFound;
                response.Payload.Message = "Người dùng không tồn tại";
            }

            return response;
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
                Search = request.Search ?? "",
                MaxPage = 1
            };

            Tuple<List<Package>, int> list = await _packageRepository.GetWithPagingByCareCenterId(request.CareCenterId, paging);

            if (list.Item1 == null || list.Item1.Count == 0)
            {
                response.Status = StatusCode.NotFound;
                response.Payload.Message = "Dữ liệu không tồn tại";
            }
            else
            {
                ListPackageResponse data = new();
                List<PackageResponseModel> listModel = [];

                foreach (Package item in list.Item1)
                {
                    PackageResponseModel model = new()
                    {
                        Id = item.Id,
                        TotalPrice = item.TotalPrice,
                        Title = item.Title,
                        Image = item.Image,
                        Description = item.Description,
                        Type = item.Type
                    };
                    listModel.Add(model);
                }
                data.List.AddRange(listModel);


                response.Status = StatusCode.OK;
                response.Payload.Message = "Lấy dữ liệu thành công";

                data.Paging = paging;
                data.Paging.MaxPage = list.Item2;

                response.Payload.Data = data;
            }



            return response;
        }

        public async Task<ResponseObject<CreatePackageResponse>> UploadPackageImage(Guid packageId, Guid userId, FileDetails filesDetail)
        {
            ResponseObject<CreatePackageResponse> response = new();
            CreatePackageResponse data = new();

            CareCenter? careCenter = await _careCenterRepository.GetByManagerId(userId);


            if (careCenter != null && careCenter.Partner != null && careCenter.Partner.Username != null && careCenter.Manager != null && careCenter.Manager.Username != null)
            {
                Package? package = await _packageRepository.GetByIdAsync(packageId);

                if (package != null && package.CareCenterId == careCenter.Id)
                {
                    if (filesDetail.IsContain)
                    {
                        List<FileDetails> images = [filesDetail];

                        List<string> fileName = await uploadImage.UploadImage(images);

                        package.Image = String.Join(",", fileName);

                        await _packageRepository.EditAsync(package);

                        data.PackageId = package.Id;
                        response.Status = StatusCode.OK;
                        response.Payload.Data = data;
                        response.Payload.Message = "Tải hình ảnh thành công";
                    }
                    else
                    {
                        response.Status = StatusCode.NotFound;
                        response.Payload.Message = "Không có hình ảnh gửi đi";
                    }
                } 
                else
                {
                    response.Status = StatusCode.NotFound;
                    response.Payload.Message = "Gói hàng không tồn tại";
                }
            }
            else
            {
                response.Status = StatusCode.NotFound;
                response.Payload.Message = "Người dùng không tồn tại";
            }

            return response;
        }
    }
}
