using AutoMapper;
using CapstoneProject.Business.Interfaces;
using CapstoneProject.Database.Model;
using CapstoneProject.Database.Model.Meta;
using CapstoneProject.DTO;
using CapstoneProject.DTO.Request;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.Pet;
using CapstoneProject.DTO.Response.Package;
using CapstoneProject.DTO.Response.Pet;
using CapstoneProject.Repository.Interface;
using CapstoneProject.Repository.Repository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CapstoneProject.Business.Services
{
    public class PetService(IPetRepository petRepository, IPackageRepository packageRepository, IMapper mapper, IUserRepository userRepository, IPetTypeRepository petTypeRepository, IOrderDetailRepository orderDetailRepository) : IPetService
    {
        private readonly IPetRepository _petRepository = petRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPetTypeRepository _petTypeRepository = petTypeRepository;
        private readonly IPackageRepository _packageRepository = packageRepository;
        private readonly IOrderDetailRepository _orderDetailRepository = orderDetailRepository;
        private readonly IMapper _mapper = mapper;
        public UploadImageService uploadImage = new();
        public StatusCode StatusCode { get; set; } = new();

        public async Task<ResponseObject<ListPetModel>> GetList(Guid userId, ListRequest request)
        {
            ResponseObject<ListPetModel> response = new();
            ListPetModel data = new();

            User? user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
            {
                response.Status = StatusCode.BadRequest;
                response.Payload.Message = "Không thể tìm thấy người dùng";
            }
            else
            {
                List<PetModel> petList = [];

                Paging paging = new()
                {
                    Page = request.Page,
                    Size = request.Size,
                    MaxPage = 1
                };

                if (user.Role == UserRole.CUSTOMER)
                {
                    List<Pet>? list = await _petRepository.GetByUserId(userId, paging);

                    if (list != null && list.Count != 0)
                    {
                        foreach (Pet item in list)
                        {
                            PetModel model = new()
                            {
                                Id = item.Id,
                                FullName = item.FullName,
                                Birthday = item.Birthday,
                                Weight = item.Weight,
                                Breed = item.Breed,
                                Sterilise = item.Sterilise,
                                Gender = item.Gender,
                                ProfileImage = item.ProfileImage,
                                Description = item.Description,
                                Status = item.Status
                            };
                            petList.Add(model);
                        }
                        data.List = petList;
                        data.Paging = paging;

                        response.Status = StatusCode.OK;
                        response.Payload.Message = "Lấy dữ liệu thành công";
                        response.Payload.Data = data;
                    }
                    else
                    {
                        response.Status = StatusCode.OK;
                        response.Payload.Message = "Bạn chưa có thú cưng nào";
                    }
                }
                else
                {
                    response.Status = StatusCode.BadRequest;
                    response.Payload.Message = "Chỉ khách hàng mới có thú cưng";
                }
            }

            return response;
        }

        public async Task<PetResponse> GetPetById(string petId)
        {
            Pet? pet = await _petRepository.GetByIdAsync(Guid.Parse(petId));
            if (pet == null)
            {
                throw new Exception("Not found Pet with this id");

            }
            PetResponse petResponse = _mapper.Map<PetResponse>(pet);
            return petResponse;
        }

        public async Task<ResponseObject<CreatePetResponse>> CreatePet(Guid userId, PetCreateRequest request, FileDetails fileDetail)
        {
            ResponseObject<CreatePetResponse> response = new();
            CreatePetResponse data = new();

            User? user = await _userRepository.GetByIdAsync(userId);
            PetType? petType = await _petTypeRepository.GetByIdAsync(request.PetTypeId);

            if (user == null)
            {
                response.Status = StatusCode.BadRequest;
                response.Payload.Message = "Không thể tìm thấy người dùng";
            }
            else if(petType == null)
            {
                response.Status = StatusCode.BadRequest;
                response.Payload.Message = "Không thể tìm thấy loại thú cưng";
            }
            else
            {
                if (user.Role == UserRole.CUSTOMER)
                {
                    Pet newPet = new()
                    {
                        Id = Guid.NewGuid(),
                        UserId = userId,
                        PetTypeId = petType.Id,
                        FullName = request.Fullname,
                        Description = request.Description ?? "Không có",
                        Birthday = request.Birthday,
                        Weight = request.Weight,
                        Gender = request.Gender,
                        Breed = request.Breed,
                        Sterilise = request.Sterilise,
                        CreatedAt = DateTime.UtcNow.AddHours(7),
                        CreatedBy = user.Username
                    };

                    if (fileDetail.IsContain)
                    {
                        List<FileDetails> images = [fileDetail];

                        List<string> fileName = await uploadImage.UploadImage(images);

                        newPet.ProfileImage = String.Join(",", fileName);
                    }

                    Pet? checkPet = await _petRepository.AddAsync(newPet);

                    if (checkPet != null)
                    {
                        response.Status = StatusCode.OK;
                        response.Payload.Message = "Tạo thú cưng thành công";

                        data.IsSucceed = true;
                        response.Payload.Data = data;
                    }
                    else
                    {
                        response.Status = StatusCode.BadRequest;
                        response.Payload.Message = "Tạo thú cưng thất bại";
                    }   
                }
                else
                {
                    response.Status = StatusCode.BadRequest;
                    response.Payload.Message = "Chỉ khách hàng mới có thể tạo thú cưng";
                }
            }

            return response;
        }

        public async Task<PetResponse?> UpdatePet(PetUpdateRequest request)
        {
            Pet? petCheck = await _petRepository.GetByIdAsync(Guid.Parse(request.Id));
            if (petCheck == null)
            {
                throw new Exception("ID is invalid.");
            }

            User? userCheck = await _userRepository.GetByIdAsync(Guid.Parse(request.UserId));
            if (userCheck == null)
            {
                throw new Exception("User id is invalid.");
            }

            PetType? petTypeCheck = await _petTypeRepository.GetByIdAsync(Guid.Parse(request.PetTypeId));
            if (petTypeCheck == null)
            {
                throw new Exception("Pet type id is invalid.");
            }

            Pet petUpdate = _mapper.Map<Pet>(request);
            petUpdate.CreatedAt = petCheck.CreatedAt;
            petUpdate.CreatedBy = petCheck.CreatedBy;
            petUpdate.UpdatedAt = DateTimeOffset.Now;
            petUpdate.UpdatedBy = request.UpdatedBy;
            bool result = await _petRepository.EditAsync(petUpdate);
            petUpdate.User = petCheck.User;
            petUpdate.PetType = petCheck.PetType;
            return result ? _mapper.Map<PetResponse>(petUpdate) : null;
        }

        public async Task<ResponseObject<GetCareCenterPetListResponse>> GetCareCenterPetList(Guid userId, ListRequest request)
        {
            ResponseObject<GetCareCenterPetListResponse> response = new();
            GetCareCenterPetListResponse data = new();

            Paging paging = new()
            {
                Page = request.Page,
                Size = request.Size,
                MaxPage = 1
            };

            User? user = await _userRepository.GetByIdAsync(userId);
            
            if (user != null)
            {
                if (user.Role == UserRole.CUSTOMER)
                {
                    Tuple<List<Package>, int> list = await _packageRepository.GetByCustomerId(userId, paging);

                    List<PetModel> petModels = [];

                    foreach (var item in list.Item1)
                    {
                        item.OrderDetails.ForEach(x =>
                        {
                            PetModel newPet = _mapper.Map<PetModel>(x.Pet);

                            List<Dictionary<int, List<string>>> model = [];

                            model = JsonConvert.DeserializeObject<List<Dictionary<int, List<string>>>>(x.AttendanceList ?? "") ?? [];

                            int dayDif = (DateTimeOffset.UtcNow.AddHours(7) - x.FromDate).Days;

                            if (model[dayDif][dayDif][0] == "0")
                            {
                                newPet.IsCheckIn = false;
                            }
                            else
                            {
                                newPet.IsCheckIn = true;
                                if (model[dayDif][dayDif][0] != "1")
                                {
                                    newPet.CheckInImg = model[dayDif][dayDif][0];
                                }
                            }

                            if (model[dayDif][dayDif][1] == "0")
                            {
                                newPet.IsCheckOut = false;
                            }
                            else
                            {
                                newPet.IsCheckOut = true;
                                if (model[dayDif][dayDif][1] != "1")
                                {
                                    newPet.CheckOutImg = model[dayDif][dayDif][1];
                                }
                            }

                            petModels.Add(newPet);
                        });

                        PackageResponseModel packageResponseModel = _mapper.Map<PackageResponseModel>(item);

                        GetCareCenterPetListModel model = new();

                        model.Pet = petModels;
                        model.Model = packageResponseModel;

                        data.List.Add(model);

                        paging.MaxPage = list.Item2;

                        data.Paging = paging;

                        response.Status = StatusCode.OK;
                        response.Payload.Message = "Lấy dữ liệu thành công";
                        response.Payload.Data = data;
                    }
                } 
                else if (user.Role == UserRole.STAFF)
                {
                    Tuple<List<Package>, int> list = await _packageRepository.GetByStaffId(userId, paging);

                    List<PetModel> petModels = [];

                    foreach (var item in list.Item1)
                    {
                        item.OrderDetails.ForEach(x =>
                        {
                            PetModel newPet = _mapper.Map<PetModel>(x.Pet);

                            List<Dictionary<int, List<string>>> model = [];

                            model = JsonConvert.DeserializeObject<List<Dictionary<int, List<string>>>>(x.AttendanceList ?? "") ?? [];

                            int dayDif = (DateTimeOffset.UtcNow.AddHours(7) - x.FromDate).Days;

                            if (model[dayDif][dayDif][0] == "0")
                            {
                                newPet.IsCheckIn = false;
                            }
                            else
                            {
                                newPet.IsCheckIn = true;
                                if (model[dayDif][dayDif][0] != "1")
                                {
                                    newPet.CheckInImg = model[dayDif][dayDif][0];
                                }
                            }

                            if (model[dayDif][dayDif][1] == "0")
                            {
                                newPet.IsCheckOut = false;
                            }
                            else
                            {
                                newPet.IsCheckOut = true;
                                if (model[dayDif][dayDif][1] != "1")
                                {
                                    newPet.CheckOutImg = model[dayDif][dayDif][1];
                                }
                            }

                            petModels.Add(newPet);
                        });

                        PackageResponseModel packageResponseModel = _mapper.Map<PackageResponseModel>(item);

                        GetCareCenterPetListModel model = new();

                        model.Pet = petModels;
                        model.Model = packageResponseModel;

                        data.List.Add(model);

                        paging.MaxPage = list.Item2;

                        data.Paging = paging;

                        response.Status = StatusCode.OK;
                        response.Payload.Message = "Lấy dữ liệu thành công";
                        response.Payload.Data = data;
                    }
                }
                else
                {
                    response.Status = StatusCode.BadRequest;
                    response.Payload.Message = "Bạn không có quyền truy cập dịch vụ này";
                }
            }
            else
            {
                response.Status = StatusCode.BadRequest;
                response.Payload.Message = "Không thể tìm thấy người dùng";
            }

            return response;
        }

        public async Task<ResponseObject<ListPetModel>> GetActiveByUserIdAndPetTypeId(Guid userId, Guid packageId, ListRequest request)
        {
            ResponseObject<ListPetModel> response = new();
            ListPetModel data = new();

            User? user = await _userRepository.GetByIdAsync(userId);
            Package? package = await _packageRepository.GetByIdAsync(packageId);

            if (user == null)
            {
                response.Status = StatusCode.BadRequest;
                response.Payload.Message = "Không thể tìm thấy người dùng";
            }
            else if (package == null)
            {
                response.Status = StatusCode.BadRequest;
                response.Payload.Message = "Không thể tìm thấy gói dịch vụ";
            } 
            else if (package.PetTypeId == null)
            {
                response.Status = StatusCode.BadRequest;
                response.Payload.Message = "Không thể tìm thấy gói dịch vụ";
            } 
            else 
            {
                List<PetModel> petList = [];

                Paging paging = new()
                {
                    Page = request.Page,
                    Size = request.Size,
                    MaxPage = 1
                };

                if (user.Role == UserRole.CUSTOMER)
                {
                    List<Pet>? list = await _petRepository.GetActiveByUserIdAndPetTypeId(userId, package.PetTypeId.Value, paging);

                    if (list != null && list.Count != 0)
                    {
                        foreach (Pet item in list)
                        {
                            PetModel model = new()
                            {
                                Id = item.Id,
                                FullName = item.FullName,
                                Birthday = item.Birthday,
                                Weight = item.Weight,
                                Breed = item.Breed,
                                Sterilise = item.Sterilise,
                                Gender = item.Gender,
                                ProfileImage = item.ProfileImage,
                                Description = item.Description,
                                Status = item.Status
                            };
                            petList.Add(model);
                        }
                        data.List = petList;
                        data.Paging = paging;

                        response.Status = StatusCode.OK;
                        response.Payload.Message = "Lấy dữ liệu thành công";
                        response.Payload.Data = data;
                    }
                    else
                    {
                        response.Status = StatusCode.OK;
                        response.Payload.Message = "Bạn chưa có thú cưng nào";
                    }
                }
                else
                {
                    response.Status = StatusCode.BadRequest;
                    response.Payload.Message = "Chỉ khách hàng mới có thú cưng";
                }
            }

            return response;
        }

        public async Task<ResponseObject<CheckInAndOutResponse>> CheckIn(Guid userId, Guid petId, bool isCheckIn, FileDetails filesDetail)
        {
            ResponseObject<CheckInAndOutResponse> response = new();
            CheckInAndOutResponse data = new();
            data.IsSucceed = false;

            User? staff = await _userRepository.GetByIdAsync(userId);
            Pet? pet = await _petRepository.GetByIdAsync(petId);

            if (staff != null && staff.Role == UserRole.STAFF) 
            {
                if (pet != null) 
                {
                    OrderDetail? orderDetail = await _orderDetailRepository.GetByIdAsyncCustom(petId);

                    if (orderDetail != null)
                    {
                        if (orderDetail.Package != null)
                        {
                            if (orderDetail.Package.CareCenter != null)
                            {
                                if (orderDetail.Package.CareCenter.Staffs != null && orderDetail.Package.CareCenter.Staffs.Where(x => x.UserId == userId).FirstOrDefault() != null)  {
                                    if (orderDetail.AttendanceList != null)
                                    {
                                        List<Dictionary<int, List<string>>> model = [];

                                        model = JsonConvert.DeserializeObject<List<Dictionary<int, List<string>>>>(orderDetail.AttendanceList) ?? [];

                                        int dayDif = (DateTimeOffset.UtcNow.AddHours(7) - orderDetail.FromDate).Days;

                                        if (dayDif <= model.Count)
                                        {
                                            string addText = "1";

                                            if (!isCheckIn)
                                            {
                                                addText = "0";
                                            }
                                            else if (filesDetail.IsContain)
                                            {
                                                List<FileDetails> images = [filesDetail];

                                                List<string> fileName = await uploadImage.UploadImage(images);

                                                addText = String.Join(",", fileName);
                                            }


                                            if (!isCheckIn && model[dayDif][dayDif][1] != "0")
                                            {
                                                response.Status = StatusCode.BadRequest;
                                                response.Payload.Message = "Không thể thực hiện khi thú cưng đã trả về cho chủ sở hũu";
                                                response.Payload.Data = data;
                                            }
                                            else
                                            {
                                                model[dayDif][dayDif][0] = addText;
                                                data.IsSucceed = true;
                                                orderDetail.AttendanceList = JsonConvert.SerializeObject(model);
                                                //orderDetail.UpdatedAt = DateTimeOffset.UtcNow.AddHours(7);
                                                //orderDetail.UpdatedBy = staff.Username;

                                                await _orderDetailRepository.EditAsync(orderDetail);

                                                response.Status = StatusCode.OK;

                                                if (isCheckIn)
                                                {
                                                    response.Payload.Message = "Xác nhận nhận thú thành công";
                                                }
                                                else 
                                                {
                                                    response.Payload.Message = "Gỡ xác nhận nhận thú thành công";
                                                }
                                            
                                                response.Payload.Data = data;
                                            }
                                        }
                                        else
                                        {
                                            response.Status = StatusCode.BadRequest;
                                            response.Payload.Message = "Không thể điểm danh do đã quá ngày sử dụng dịch vụ";
                                        }
                                    }
                                    else
                                    {
                                        response.Status = StatusCode.BadRequest;
                                        response.Payload.Message = "Không tìm thấy danh sách điểm danh";
                                    }
                                } 
                                else
                                {
                                    response.Status = StatusCode.BadRequest;
                                    response.Payload.Message = "Không tìm thấy thông tin nhân viên";
                                }
                            }
                            else
                            {
                                response.Status = StatusCode.BadRequest;
                                response.Payload.Message = "Không tìm thấy thông tin trung tâm chăm sóc thú cưng";
                            }
                        }
                        else
                        {
                            response.Status = StatusCode.BadRequest;
                            response.Payload.Message = "Không tìm thấy thông tin gói hàng đã mua";
                        }
                    }
                    else
                    {
                        response.Status = StatusCode.BadRequest;
                        response.Payload.Message = "Không tìm thấy đơn hàng";
                    }
                }
                else
                {
                    response.Status = StatusCode.BadRequest;
                    response.Payload.Message = "Không tìm thấy thú cưng";
                }
            }
            else
            {
                response.Status = StatusCode.BadRequest;
                response.Payload.Message = "Không tìm thấy nhân viên";
            }

            return response;
        }

        public async Task<ResponseObject<CheckInAndOutResponse>> CheckOut(Guid userId, Guid petId, bool isCheckOut, FileDetails filesDetail)
        {
            ResponseObject<CheckInAndOutResponse> response = new();
            CheckInAndOutResponse data = new();
            data.IsSucceed = false;

            User? staff = await _userRepository.GetByIdAsync(userId);
            Pet? pet = await _petRepository.GetByIdAsync(petId);

            if (staff != null && staff.Role == UserRole.STAFF)
            {
                if (pet != null)
                {
                    OrderDetail? orderDetail = await _orderDetailRepository.GetByIdAsyncCustom(petId);

                    if (orderDetail != null)
                    {
                        if (orderDetail.Package != null)
                        {
                            if (orderDetail.Package.CareCenter != null)
                            {
                                if (orderDetail.Package.CareCenter.Staffs != null && orderDetail.Package.CareCenter.Staffs.Where(x => x.UserId == userId).FirstOrDefault() != null)
                                {
                                    if (orderDetail.AttendanceList != null)
                                    {
                                        List<Dictionary<int, List<string>>> model = [];

                                        model = JsonConvert.DeserializeObject<List<Dictionary<int, List<string>>>>(orderDetail.AttendanceList) ?? [];

                                        int dayDif = (DateTimeOffset.UtcNow.AddHours(7) - orderDetail.FromDate).Days;

                                        if (dayDif <= model.Count)
                                        {
                                            string addText = "1";

                                            if (!isCheckOut)
                                            {
                                                addText = "0";
                                            }
                                            else if (filesDetail.IsContain)
                                            {
                                                List<FileDetails> images = [filesDetail];

                                                List<string> fileName = await uploadImage.UploadImage(images);

                                                addText = String.Join(",", fileName);
                                            }

                                            if (model[dayDif][dayDif][0] == "0")
                                            {
                                                response.Status = StatusCode.BadRequest;
                                                response.Payload.Message = "Không thể thực hiện khi thú cưng chưa có mặt ở trung tâm";
                                                response.Payload.Data = data;
                                            }
                                            else
                                            {
                                                model[dayDif][dayDif][1] = addText;
                                                data.IsSucceed = true;
                                                orderDetail.AttendanceList = JsonConvert.SerializeObject(model);
                                                //orderDetail.UpdatedAt = DateTimeOffset.UtcNow.AddHours(7);
                                                //orderDetail.UpdatedBy = staff.Username;

                                                await _orderDetailRepository.EditAsync(orderDetail);

                                                response.Status = StatusCode.OK;

                                                if (isCheckOut)
                                                {
                                                    response.Payload.Message = "Xác nhận trả thú thành công";
                                                }
                                                else
                                                {
                                                    response.Payload.Message = "Gỡ xác nhận trả thú thành công";
                                                }
                                                response.Payload.Data = data;
                                            }
                                        }
                                        else
                                        {
                                            response.Status = StatusCode.BadRequest;
                                            response.Payload.Message = "Không thể điểm danh do đã quá ngày sử dụng dịch vụ";
                                        }
                                    }
                                    else
                                    {
                                        response.Status = StatusCode.BadRequest;
                                        response.Payload.Message = "Không tìm thấy danh sách điểm danh";
                                    }
                                }
                                else
                                {
                                    response.Status = StatusCode.BadRequest;
                                    response.Payload.Message = "Không tìm thấy thông tin nhân viên";
                                }
                            }
                            else
                            {
                                response.Status = StatusCode.BadRequest;
                                response.Payload.Message = "Không tìm thấy thông tin trung tâm chăm sóc thú cưng";
                            }
                        }
                        else
                        {
                            response.Status = StatusCode.BadRequest;
                            response.Payload.Message = "Không tìm thấy thông tin gói hàng đã mua";
                        }
                    }
                    else
                    {
                        response.Status = StatusCode.BadRequest;
                        response.Payload.Message = "Không tìm thấy đơn hàng";
                    }
                }
                else
                {
                    response.Status = StatusCode.BadRequest;
                    response.Payload.Message = "Không tìm thấy thú cưng";
                }
            }
            else
            {
                response.Status = StatusCode.BadRequest;
                response.Payload.Message = "Không tìm thấy nhân viên";
            }

            return response;
        }
    }
}
