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
using CapstoneProject.DTO.Request.Pet;
using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.Pet;
using CapstoneProject.DTO.Response.User;
using CapstoneProject.Repository.Interface;
using CapstoneProject.DTO;
using CapstoneProject.Database.Model.Meta;
using CapstoneProject.DTO.Response.Orders;
using CapstoneProject.Repository.Repository;

namespace CapstoneProject.Business.Service
{
    public class PetService(IPetRepository petRepository, IMapper mapper, IUserRepository userRepository, IPetTypeRepository petTypeRepository) : IPetService
    {
        private readonly IPetRepository _petRepository = petRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPetTypeRepository _petTypeRepository = petTypeRepository;
        private readonly IMapper _mapper = mapper;
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
                List<Pet>? list = [];
                List<PetModel> petList = [];

                Paging paging = new()
                {
                    Page = request.Page,
                    Size = request.Size,
                    MaxPage = 1
                };

                if (user.Role == UserRole.CUSTOMER)
                {
                    list = await _petRepository.GetByUserId(userId, paging);

                    if (list != null && list.Count != 0)
                    {
                        foreach (var item in list)
                        {
                            PetModel model = new()
                            {
                                Id = item.Id,
                                FullName = item.FullName,
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
                    } else
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
            var pet = await _petRepository.GetByIdAsync(Guid.Parse(petId));
            if (pet == null)
            {
                throw new Exception("Not found Pet with this id");
               
            }
            var petResponse = _mapper.Map<PetResponse>(pet);
            return petResponse;
        }

        public async Task<ResponseObject<CreatePetResponse>> CreatePet(Guid userId, PetCreateRequest request)
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
                        Description = request.Description,
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = user.Username
                    };

                    Pet? checkPet = await _petRepository.AddAsync(newPet);

                    if (checkPet != null)
                    {
                        response.Status = StatusCode.OK;
                        response.Payload.Message = "Tạo thú cưng thành công";

                        data.IsSucceed = true;
                        response.Payload.Data = data;
                    } else
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

        public async Task<PetResponse> UpdatePet(PetUpdateRequest request)
        {
            var petCheck = await _petRepository.GetByIdAsync(Guid.Parse(request.Id));
            if (petCheck == null)
            {
                throw new Exception("ID is invalid.");
            }
            
            var userCheck =  await _userRepository.GetByIdAsync(Guid.Parse(request.UserId));
            if (userCheck == null)
            {
                throw new Exception("User id is invalid.");
            }

            var petTypeCheck = await _petTypeRepository.GetByIdAsync(Guid.Parse(request.PetTypeId));
            if (petTypeCheck == null)
            {
                throw new Exception("Pet type id is invalid.");
            }

            var petUpdate = _mapper.Map<Pet>(request);
            petUpdate.CreatedAt = petCheck.CreatedAt;
            petUpdate.CreatedBy = petCheck.CreatedBy;
            petUpdate.UpdatedAt = DateTimeOffset.Now;
            petUpdate.UpdatedBy = request.UpdatedBy;
            var result = await _petRepository.EditAsync(petUpdate);
            petUpdate.User = petCheck.User;
            petUpdate.PetType = petCheck.PetType;
            return result ? _mapper.Map<PetResponse>(petUpdate) : null;
        }
    }
}
