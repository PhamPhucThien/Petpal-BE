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

namespace CapstoneProject.Business.Service
{
    public class PetService(IPetRepository petRepository, IMapper mapper, IUserRepository userRepository, IPetTypeRepository petTypeRepository) : IPetService
    {
        private readonly IPetRepository _petRepository = petRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPetTypeRepository _petTypeRepository = petTypeRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseListResponse<PetResponse>> GetList(ListRequest request)
        {
            Paging paging = new()
            {
                Page = request.Page,
                Size = request.Size,
                MaxPage = 1
            };
            var listPet = await _petRepository.GetWithPaging(paging);
            var listPetResponse = _mapper.Map<List<PetResponse>>(listPet);
            paging.Total = listPetResponse.Count;
            BaseListResponse<PetResponse> response = new()
            {
                List = listPetResponse,
                Paging = paging,
            };
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

        public async Task<PetResponse> CreatePet(PetCreateRequest request)
        {
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
            var petCreate = _mapper.Map<Pet>(request);
            petCreate.CreatedAt = DateTimeOffset.Now;
            petCreate.CreatedBy = request.CreatedBy;
            var result = await _petRepository.AddAsync(petCreate);
            var pet = await _petRepository.GetByIdAsync(result.Id);
            return _mapper.Map<PetResponse>(pet);
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
