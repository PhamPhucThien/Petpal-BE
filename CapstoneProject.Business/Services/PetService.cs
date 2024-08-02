using AutoMapper;
using CapstoneProject.Business.Interfaces;
using CapstoneProject.Database.Model;
using CapstoneProject.Database.Model.Meta;
using CapstoneProject.DTO;
using CapstoneProject.DTO.Request;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.Pet;
using CapstoneProject.DTO.Response.Pet;
using CapstoneProject.Repository.Interface;

namespace CapstoneProject.Business.Services
{
    public class PetService(IPetRepository petRepository, IMapper mapper, IUserRepository userRepository, IPetTypeRepository petTypeRepository) : IPetService
    {
        private readonly IPetRepository _petRepository = petRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPetTypeRepository _petTypeRepository = petTypeRepository;
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

            if (user == null)
            {
                response.Status = StatusCode.BadRequest;
                response.Payload.Message = "Không thể tìm thấy người dùng";
            }
            else
            {
                if (user.Role == UserRole.CUSTOMER)
                {
                    Pet newPet = new()
                    {
                        Id = Guid.NewGuid(),
                        UserId = userId,
                        FullName = request.Fullname,
                        Description = request.Description ?? "Không có",
                        Birthday = request.Birthday,
                        Weight = request.Weight,
                        Gender = request.Gender,
                        Breed = request.Breed,
                        Sterilise = request.Sterilise,
                        CreatedAt = DateTime.UtcNow,
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
    }
}
