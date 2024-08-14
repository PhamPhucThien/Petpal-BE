using AutoMapper;
using CapstoneProject.Business.Interfaces;
using CapstoneProject.Database.Model;
using CapstoneProject.DTO;
using CapstoneProject.DTO.Request;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.PetType;
using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.PetType;
using CapstoneProject.Repository.Interface;
using System.Management;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CapstoneProject.Business.Services
{
    public class PetTypeService : IPetTypeService
    {
        private readonly IPetTypeRepository _petTypeRepository;
        private readonly IMapper _mapper;
        public StatusCode StatusCode = new();

        public PetTypeService(IPetTypeRepository petTypeRepository, IMapper mapper)
        {
            _petTypeRepository = petTypeRepository;
            _mapper = mapper;
        }

        public async Task<ResponseObject<List<PetTypeDetailResponse>>> GetList(ListRequest request)
        {
            ResponseObject<List<PetTypeDetailResponse>> response = new();

            Paging paging = new()
            {
                Page = request.Page,
                Size = request.Size,
                MaxPage = 1
            };

            Tuple<List<PetType>, int> listPetType = await _petTypeRepository.GetWithPaging(paging);
            List<PetTypeDetailResponse> listPetTypeResponse = _mapper.Map<List<PetTypeDetailResponse>>(listPetType.Item1);
            paging.MaxPage = listPetType.Item2;

            response.Status = StatusCode.OK;
            response.Payload.Message = "Lấy dữ liệu thành công";
            response.Payload.Data = listPetTypeResponse;
        
            return response;
        }

        public async Task<ResponseObject<PetTypeDetailResponse>> GetById(string petId)
        {
            ResponseObject<PetTypeDetailResponse> response = new();

            PetType? petType = await _petTypeRepository.GetByIdAsync(Guid.Parse(petId));

            if (petType == null)
            {
                response.Status = StatusCode.NotFound;
                response.Payload.Message = "Id không tồn tại";
                response.Payload.Data = null;

            }

            PetTypeDetailResponse petTypeResponse = _mapper.Map<PetTypeDetailResponse>(petType);

            response.Status = StatusCode.OK;
            response.Payload.Message = "Lấy dữ liệu thành công";
            response.Payload.Data = petTypeResponse;

            return response;
        }

        public async Task<ResponseObject<PetTypeDetailResponse>> Create(PetTypeCreateRequest request)
        {
            ResponseObject<PetTypeDetailResponse> response = new();

            PetType petTypeCreate = _mapper.Map<PetType>(request);
            petTypeCreate.CreatedAt = DateTimeOffset.Now;
            petTypeCreate.CreatedBy = request.CreatedBy;
            PetType? result = await _petTypeRepository.AddAsync(petTypeCreate);

            if (result != null)
            {
                PetTypeDetailResponse data = _mapper.Map<PetTypeDetailResponse>(result);
                response.Status = StatusCode.OK;
                response.Payload.Message = "Tạo dữ liệu thành công";
                response.Payload.Data = data;
            }
            else
            {
                response.Status = StatusCode.NotFound;
                response.Payload.Message = "Tạo dữ liệu thất bại";
                response.Payload.Data = null;
            }

            return response;
        }

        public async Task<ResponseObject<PetTypeDetailResponse>> Update(PetTypeUpdateRequest request)
        {
            ResponseObject<PetTypeDetailResponse> response = new();

            PetType? petTypeCheck = await _petTypeRepository.GetByIdAsync(Guid.Parse(request.Id));

            if (petTypeCheck == null)
            {
                response.Status = StatusCode.NotFound;
                response.Payload.Message = "Id không tồn tại";
                response.Payload.Data = null;
            }

            PetType petTypeCreate = _mapper.Map<PetType>(request);
            petTypeCreate.CreatedAt = DateTimeOffset.Now;
            petTypeCreate.CreatedBy = request.UpdatedBy;
            bool? result = await _petTypeRepository.EditAsync(petTypeCreate);

            if (result != null && result == true)
            {
                PetTypeDetailResponse data = _mapper.Map<PetTypeDetailResponse>(petTypeCreate);

                response.Status = StatusCode.OK;
                response.Payload.Message = "Cập nhật dữ liệu thành công";
                response.Payload.Data = data;
            }
            else
            {
                response.Status = StatusCode.NotFound;
                response.Payload.Message = "Cập nhật dữ liệu thất bại";
                response.Payload.Data = null;
            }

            return response;
        }
    }
}
