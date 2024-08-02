﻿using CapstoneProject.Business.Interface;
using CapstoneProject.Database.Model;
using CapstoneProject.Database.Model.Meta;
using CapstoneProject.DTO;
using CapstoneProject.DTO.Request;
using CapstoneProject.DTO.Request.CareCenters;
using CapstoneProject.DTO.Request.User;
using CapstoneProject.DTO.Response.Account;
using CapstoneProject.DTO.Response.CareCenters;
using CapstoneProject.Repository.Interface;
using System.Transactions;

namespace CapstoneProject.Business.Service
{
    public class CareCenterService(ICareCenterRepository careCenterRepository, IUserRepository userRepository, IAuthRepository authRepository) : ICareCenterService
    {
        private readonly ICareCenterRepository _careCenterRepository = careCenterRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IAuthRepository _authRepository = authRepository;
        public UploadImageService uploadImage = new();
        public StatusCode StatusCode { get; set; } = new();

        public async Task<ResponseObject<CreateCareCenterAndManagerResponse>> CreateCareCenterAndManager(Guid userId, CreateCareCenterRequest request, FileDetails front_image, FileDetails back_image)
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

                    List<FileDetails> files = [front_image, back_image];
                    List<string> images = await uploadImage.UploadImage(files);

                    manager.IdentityFrontImage = images[0] ?? "";
                    manager.IdentityBackImage = images[1] ?? "";

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

        public async Task<ResponseObject<GetCareCenterListResponse>> GetList(GetCareCenterListRequest request)
        {
            ResponseObject<GetCareCenterListResponse> response = new();

            Paging paging = new()
            {
                Page = request.Page,
                Size = request.Size,
                MaxPage = 1
            };

            List<CareCenter> list = await _careCenterRepository.GetWithPaging(paging);

            GetCareCenterListResponse listModel = new();

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
                listModel.List.Add(model);
            }

            listModel.Paging = paging;
            listModel.Paging.Total = paging.Total;

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
    }
}
