using AutoMapper;
using CapstoneProject.Business.Interface;
using CapstoneProject.Database.Model;
using CapstoneProject.DTO.Request;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.PetType;
using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.PetType;
using CapstoneProject.Repository.Interface;

namespace CapstoneProject.Business.Service
{
    public class PetTypeService : IPetTypeService
    {
        private readonly IPetTypeRepository _petTypeRepository;
        private readonly IMapper _mapper;

        public PetTypeService(IPetTypeRepository petTypeRepository, IMapper mapper)
        {
            _petTypeRepository = petTypeRepository;
            _mapper = mapper;
        }

        public async Task<BaseListResponse<PetTypeDetailResponse>> GetList(ListRequest request)
        {
            Paging paging = new()
            {
                Page = request.Page,
                Size = request.Size,
                MaxPage = 1
            };
            List<PetType> listPetType = await _petTypeRepository.GetWithPaging(paging);
            List<PetTypeDetailResponse> listPetTypeResponse = _mapper.Map<List<PetTypeDetailResponse>>(listPetType);
            paging.Total = listPetTypeResponse.Count;
            BaseListResponse<PetTypeDetailResponse> response = new()
            {
                List = listPetTypeResponse,
                Paging = paging,
            };
            return response;
        }

        public async Task<PetTypeDetailResponse> GetById(string petId)
        {
            PetType? petType = await _petTypeRepository.GetByIdAsync(Guid.Parse(petId));
            if (petType == null)
            {
                throw new Exception("Not found Pet Type with this id");

            }
            PetTypeDetailResponse petTypeResponse = _mapper.Map<PetTypeDetailResponse>(petType);
            return petTypeResponse;
        }

        public async Task<PetTypeDetailResponse> Create(PetTypeCreateRequest request)
        {
            PetType petTypeCreate = _mapper.Map<PetType>(request);
            petTypeCreate.CreatedAt = DateTimeOffset.Now;
            petTypeCreate.CreatedBy = request.CreatedBy;
            PetType? result = await _petTypeRepository.AddAsync(petTypeCreate);
            PetType? petType = await _petTypeRepository.GetByIdAsync(result.Id);
            return _mapper.Map<PetTypeDetailResponse>(petType);
        }

        public async Task<PetTypeDetailResponse?> Update(PetTypeUpdateRequest request)
        {
            PetType? petTypeCheck = await _petTypeRepository.GetByIdAsync(Guid.Parse(request.Id));
            if (petTypeCheck == null)
            {
                throw new Exception("ID is invalid.");
            }
            PetType petTypeUpdate = _mapper.Map<PetType>(request);
            petTypeUpdate.CreatedAt = petTypeCheck.CreatedAt;
            petTypeUpdate.CreatedBy = petTypeCheck.CreatedBy;
            petTypeUpdate.UpdatedAt = DateTimeOffset.Now;
            petTypeUpdate.UpdatedBy = request.UpdatedBy;
            bool result = await _petTypeRepository.EditAsync(petTypeUpdate);
            return result ? _mapper.Map<PetTypeDetailResponse>(petTypeUpdate) : null;
        }
    }
}
