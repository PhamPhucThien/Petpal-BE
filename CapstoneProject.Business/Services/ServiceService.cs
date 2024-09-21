using AutoMapper;
using CapstoneProject.Business.Interfaces;
using CapstoneProject.Database.Model;
using CapstoneProject.Database.Model.Meta;
using CapstoneProject.DTO;
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
        private readonly ICareCenterRepository _careCenterRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public StatusCode StatusCode { get; set; } = new();

        public ServiceService(IServiceRepository serviceRepository, IMapper mapper, IUserRepository userRepository, ICareCenterRepository careCenterRepository)
        {
            _careCenterRepository = careCenterRepository;
            _serviceRepository = serviceRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ResponseObject<ListServiceResponse>> GetList(ListRequest request, Guid userId)
        {
            ResponseObject<ListServiceResponse> response = new();

            User? user = await _userRepository.GetByIdAsync(userId);

            if (user != null)
            {
                if (user.Role == UserRole.PARTNER || user.Role == UserRole.MANAGER || user.Role == UserRole.ADMIN)
                {
                    ListServiceResponse data = new();

                    string creator = user.Username ?? string.Empty;

                    if (user.Role == UserRole.MANAGER)
                    {
                        CareCenter? careCenter = await _careCenterRepository.GetByManagerId(userId);
                        if (careCenter != null && careCenter.Partner != null && careCenter.Partner.Username != null)
                        {
                            creator = careCenter.Partner.Username;
                        }
                    }

                    Paging paging = new()
                    {
                        Page = request.Page,
                        Size = request.Size,
                        Search = request.Search ?? string.Empty,
                        MaxPage = 1
                    };

                    Tuple<List<Service>, int> listService = await _serviceRepository.GetWithPagingCustom(paging, creator);
                    List<ServiceResponseModel> listServiceResponse = _mapper.Map<List<ServiceResponseModel>>(listService.Item1);
                    paging.MaxPage = listService.Item2;

                    data.Paging = paging;
                    data.List = listServiceResponse;

                    response.Payload.Data = data;
                    response.Status = StatusCode.OK;
                }
                else
                {
                    response.Status = StatusCode.NotFound;
                    response.Payload.Message = "Chỉ đối tác, quản lý và admin mới có thể sử dụng";
                }
            }
            else
            {
                response.Status = StatusCode.NotFound;
                response.Payload.Message = "Không tìm thấy người dùng";
            }

            return response;
        }

        public async Task<ResponseObject<ServiceResponseModel>> GetById(Guid serviceId, Guid userId)
        {
            ResponseObject<ServiceResponseModel> response = new();
            ServiceResponseModel data = new();

            User? user = await _userRepository.GetByIdAsync(userId);

            if (user != null)
            {
                if (user.Role == UserRole.PARTNER || user.Role == UserRole.ADMIN || user.Role == UserRole.MANAGER)
                {
                    Service? service = await _serviceRepository.GetByIdAsync(serviceId);

                    if (service != null)
                    {
                        string creator = user.Username ?? string.Empty;

                        if (user.Role == UserRole.MANAGER)
                        {
                            CareCenter? careCenter = await _careCenterRepository.GetByManagerId(userId);
                            if (careCenter != null && careCenter.Partner != null && careCenter.Partner.Username != null)
                            {
                                creator = careCenter.Partner.Username;
                            }
                        }

                        ServiceResponseModel serviceResponse = _mapper.Map<ServiceResponseModel>(service);

                        if (service.CreatedBy != null && (service.CreatedBy.Equals(creator) || service.CreatedBy.Equals("Admin")))
                        {
                            data = serviceResponse;

                            response.Status = StatusCode.OK;
                            response.Payload.Message = "Lấy dịch vụ thành công";
                            response.Payload.Data = data;
                        }
                        else
                        {
                            response.Status = StatusCode.NotFound;
                            response.Payload.Message = "Không tìm thấy dịch vụ";
                        }
                    }
                    else
                    {
                        response.Status = StatusCode.NotFound;
                        response.Payload.Message = "Không tìm thấy dịch vụ";
                    }
                }
                else
                {
                    response.Status = StatusCode.NotFound;
                    response.Payload.Message = "Chỉ đối tác, quản  admin mới có thể sử dụng";
                }
            }
            else
            {
                response.Status = StatusCode.NotFound;
                response.Payload.Message = "Không tìm thấy người dùng";
            }

            return response;
        }

        public async Task<ResponseObject<CreateServiceResponse>> Create(ServiceCreateRequest request, Guid userId)
        {
            ResponseObject<CreateServiceResponse> response = new();
            CreateServiceResponse data = new();

            User? user = await _userRepository.GetByIdAsync(userId);

            if (user != null)
            {
                if (user.Role == UserRole.PARTNER || user.Role == UserRole.ADMIN)
                {
                    Service? service = await _serviceRepository.GetByUsernameAndName(user.Username, request.Name);

                    if (service == null)
                    {
                        Service newService = new()
                        {
                            Id = Guid.NewGuid(),
                            Name = request.Name,
                            Description = request.Description,
                            CreatedBy = user.Username,
                            CreatedAt = DateTime.UtcNow.AddHours(7),
                            IsBase = user.Role == UserRole.ADMIN
                        };

                        await _serviceRepository.AddAsync(newService);

                        data.IsSucceed = true;

                        response.Status = StatusCode.OK;
                        response.Payload.Message = "Dịch vụ đã tạo thành công";
                        response.Payload.Data = data;
                    }
                    else
                    {
                        response.Status = StatusCode.NotFound;
                        response.Payload.Message = "Tên dịch vụ đã tồn tại";
                    }
                }
                else
                {
                    response.Status = StatusCode.NotFound;
                    response.Payload.Message = "Chỉ đối tác và admin mới có thể sử dụng";
                }
            }
            else 
            {
                response.Status = StatusCode.NotFound;
                response.Payload.Message = "Không tìm thấy người dùng";
            }

            return response;
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
