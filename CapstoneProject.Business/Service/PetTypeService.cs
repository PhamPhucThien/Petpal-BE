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
using CapstoneProject.DTO.Request.PetType;
using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.Pet;
using CapstoneProject.DTO.Response.PetType;
using CapstoneProject.Repository.Interface;

namespace CapstoneProject.Business.Service
{
    public class PetTypeService : IPetTypeService
    {
        private IPetTypeRepository _petTypeRepository;
        private IMapper _mapper;

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
            var listPetType = await _petTypeRepository.GetWithPaging(paging);
            var listPetTypeResponse = _mapper.Map<List<PetTypeDetailResponse>>(listPetType);
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
            var petType = await _petTypeRepository.GetByIdAsync(Guid.Parse(petId));
            if (petType == null)
            {
                throw new Exception("Not found Pet Type with this id");
               
            }
            var petTypeResponse = _mapper.Map<PetTypeDetailResponse>(petType);
            return petTypeResponse;
        }

        public async Task<PetTypeDetailResponse> Create(PetTypeCreateRequest request)
        {
            var petTypeCreate = _mapper.Map<PetType>(request);
            petTypeCreate.CreatedAt = DateTimeOffset.Now;
            petTypeCreate.CreatedBy = request.CreatedBy;
            var result = await _petTypeRepository.AddAsync(petTypeCreate);
            var petType = await _petTypeRepository.GetByIdAsync(result.Id);
            return _mapper.Map<PetTypeDetailResponse>(petType);
        }

        public async Task<PetTypeDetailResponse> Update(PetTypeUpdateRequest request)
        {
            var petTypeCheck = await _petTypeRepository.GetByIdAsync(Guid.Parse(request.Id));
            if (petTypeCheck == null)
            {
                throw new Exception("ID is invalid.");
            }
            var petTypeUpdate = _mapper.Map<PetType>(request);
            petTypeUpdate.CreatedAt = petTypeCheck.CreatedAt;
            petTypeUpdate.CreatedBy = petTypeCheck.CreatedBy;
            petTypeUpdate.UpdatedAt = DateTimeOffset.Now;
            petTypeUpdate.UpdatedBy = request.UpdatedBy;
            var result = await _petTypeRepository.EditAsync(petTypeUpdate);
            return result ? _mapper.Map<PetTypeDetailResponse>(petTypeUpdate) : null;
        }
    }
}
