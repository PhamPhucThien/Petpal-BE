using AutoMapper;
using CapstoneProject.Business.Interfaces;
using CapstoneProject.Database.Model;
using CapstoneProject.Database.Model.Meta;
using CapstoneProject.DTO;
using CapstoneProject.DTO.Request;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.CareCenters;
using CapstoneProject.DTO.Request.User;
using CapstoneProject.DTO.Response.Account;
using CapstoneProject.DTO.Response.CareCenters;
using CapstoneProject.Repository.Interface;
using System.Transactions;

namespace CapstoneProject.Business.Services
{
    public class CareCenterService(ICareCenterRepository careCenterRepository, IUserRepository userRepository, IAuthRepository authRepository, IMapper mapper) : ICareCenterService
    {
        private readonly ICareCenterRepository _careCenterRepository = careCenterRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IAuthRepository _authRepository = authRepository;
        private readonly IMapper _mapper = mapper;
        public UploadImageService uploadImage = new();
        public StatusCode StatusCode { get; set; } = new();

        public async Task<ResponseObject<CreateCareCenterAndManagerResponse>> CreateCareCenterAndManager(Guid userId, CreateCareCenterRequest request, FileDetails front_image, FileDetails back_image, FileDetails carecenter_image)
        {
            ResponseObject<CreateCareCenterAndManagerResponse> response = new();
            CreateCareCenterAndManagerResponse data = new();
            User? user = await _userRepository.GetByIdAsync(userId);
            User? findManager = await _authRepository.GetByUsername(request.Manager.Username);

            if (user != null)
            {
                if (findManager == null)
                {
                    User manager = new()
                    {
                        Id = Guid.NewGuid(),
                        FullName = request.Manager.FullName,
                        Username = request.Manager.Username,
                        Password = request.Manager.Password,
                        PhoneNumber = request.Manager.PhoneNumber,
                        Address = request.Manager.Address,
                        Email = request.Manager.Email,
                        Role = UserRole.MANAGER,
                        IdentityNumber = request.ManagerIdentity.Number,
                        IdentityCreatedAt = request.ManagerIdentity.CreatedAt,
                        IdentityCreatedLocation = request.ManagerIdentity.CreatedLocation,
                        Status = Database.Model.Meta.UserStatus.PENDING,
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = user.Username
                    };

                    List<FileDetails> files = [front_image, back_image, carecenter_image];
                    List<string> images = await uploadImage.UploadImage(files);

                    manager.IdentityFrontImage = images[0] ?? string.Empty;
                    manager.IdentityBackImage = images[1] ?? string.Empty;

                    CareCenter careCenter = new()
                    {
                        Id = Guid.NewGuid(),
                        PartnerId = user.Id,
                        ManagerId = manager.Id,
                        Address = request.CareCenter.Address,
                        Hotline = request.CareCenter.Hotline,
                        CareCenterName = request.CareCenter.CareCenterName,
                        Status = Database.Model.Meta.CareCenterStatus.PENDING,
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = user.Username
                    };

                    careCenter.ListImages = images[2] ?? string.Empty;

                    using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                    _ = await _userRepository.AddAsync(manager);
                    _ = await _careCenterRepository.AddAsync(careCenter);

                    scope.Complete();

                    response.Status = StatusCode.OK;
                    response.Payload.Message = "Tạo trung tâm và tài khoản thành công";

                    data.IsSucceed = true;

                    response.Payload.Data = data;
                }
                else
                {
                    response.Status = StatusCode.BadRequest;
                    response.Payload.Message = "Tên tài khoản quản lý đã tồn tại";
                    response.Payload.Data = null;
                }
            } 
            else
            {
                response.Status = StatusCode.BadRequest;
                response.Payload.Message = "Id người dùng không tồn tại";
                response.Payload.Data = null;
            }

            return response;
        }

        public async Task<ResponseObject<GetCareCenterListResponse>> GetList(ListRequest request)
        {
            ResponseObject<GetCareCenterListResponse> response = new();

            Paging paging = new()
            {
                Page = request.Page,
                Size = request.Size,
                Search = request.Search ?? string.Empty,
                MaxPage = 1
            };

            Tuple<List<CareCenter>, int> list = await _careCenterRepository.GetWithPagingCustom(paging);

            GetCareCenterListResponse listModel = new();

            foreach (CareCenter item in list.Item1)
            {
                CareCenterListModel model = new()
                {
                    Address = item.Address,
                    CareCenterName = item.CareCenterName,
                    AverageRating = item.AverageRating,
                    Description = item.Description,
                    Id = item.Id,
                    ListImages = item.ListImages
                };
                listModel.List.Add(model);
            }

            listModel.Paging = paging;
            listModel.Paging.MaxPage = list.Item2;

            response.Status = StatusCode.OK;
            response.Payload.Data = listModel;

            return response;
        }

        public async Task<ResponseObject<EditCareCenterRegistrationResponse>> EditCareCenterRegistration(EditCareCenterRegistrationRequest request, CareCenterStatus careCenterStatus, UserStatus userStatus)
        {
            ResponseObject<EditCareCenterRegistrationResponse> response = new();
            EditCareCenterRegistrationResponse data = new();
            CareCenter? careCenter = await _careCenterRepository.GetCareCenterByIdAsync(request.CareCenterId);

            if (careCenter != null && careCenter.Manager != null)
            {
                User? manager = careCenter.Manager;

                manager.Status = userStatus;
                careCenter.Status = careCenterStatus;

                using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                _ = await _userRepository.EditAsync(manager);
                _ = await _careCenterRepository.EditAsync(careCenter);

                scope.Complete();

                response.Status = StatusCode.OK;
                if (userStatus == UserStatus.ACTIVE)
                {
                    response.Payload.Message = "Xác nhận thành công";
                }
                else
                {
                    response.Payload.Message = "Từ chối thành công";
                }
            }
            else
            {
                response.Status = StatusCode.BadRequest;
                if (careCenter == null)
                {
                    response.Payload.Message = "Id trung tâm không tồn tại";
                } else
                {
                    response.Payload.Message = "Id quản lý không tồn tại";
                }
            }

            return response;
        }

        public async Task<ResponseObject<GetCareCenterListResponse>> GetCareCenterByRole(Guid userId, ListRequest request)
        {
            ResponseObject<GetCareCenterListResponse> response = new();
            GetCareCenterListResponse data = new();
            Paging paging = new()
            {
                Page = request.Page,
                Size = request.Size,
                Search = request.Search ?? string.Empty,
                MaxPage = 1
            };
            CareCenter? manager = await _careCenterRepository.GetByManagerId(userId);
            Tuple<List<CareCenter>, int> partner = await _careCenterRepository.GetByPartnerId(userId, paging);

            if (manager != null && partner != null)
            {
                if (manager != null)
                {
                    response.Status = StatusCode.OK;
                    response.Payload.Message = "Tìm thấy trung tâm cho quản lý";

                    CareCenterListModel model = new()
                    {
                        Id = manager.Id,
                        CareCenterName = manager.CareCenterName,
                        Address = manager.Address,
                        Description = manager.Description,
                        AverageRating = manager.AverageRating
                    };

                    response.Payload.Data = data;
                }
                else
                {
                    

                    Tuple<List<CareCenter>, int> listItem = await _careCenterRepository.GetWithPaging(paging);

                    List<CareCenter> list = _mapper.Map<List<CareCenter>>(listItem.Item1);


                    foreach (CareCenter item in list)
                    {
                        CareCenterListModel model = new()
                        {
                            Address = item.Address,
                            CareCenterName = item.CareCenterName,
                            AverageRating = item.AverageRating,
                            Id = item.Id,
                            ListImages = item.ListImages
                        };
                        data.List.Add(model);
                    }

                    data.Paging = paging;
                    data.Paging.MaxPage = listItem.Item2;

                    response.Status = StatusCode.OK;
                    response.Payload.Data = data;
                }
            }
            else
            {
                response.Status = StatusCode.BadRequest;
                response.Payload.Message = "Id không tồn tại";
                response.Payload.Data = null;
            }

            

            return response;
        }
    }
}
