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
using CapstoneProject.DTO.Request.Comment;
using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.Comment;
using CapstoneProject.Repository.Interface;

namespace CapstoneProject.Business.Service
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepository, IMapper mapper, IUserRepository userRepository)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<BaseListResponse<CommentResponse>> GetList(ListRequest request)
        {
            Paging paging = new()
            {
                Page = request.Page,
                Size = request.Size,
                MaxPage = 1
            };
            var listComment = await _commentRepository.GetWithPaging(paging);
            var listCommentResponse = _mapper.Map<List<CommentResponse>>(listComment);
            paging.Total = listCommentResponse.Count;
            BaseListResponse<CommentResponse> response = new()
            {
                List = listCommentResponse,
                Paging = paging,
            };
            return response;
        }

        public async Task<CommentResponse> GetById(string commentId)
        {
            var comment = await _commentRepository.GetByIdAsync(Guid.Parse(commentId));
            if (comment == null)
            {
                throw new Exception("Not found Comment with this id");
               
            }
            var commentResponse = _mapper.Map<CommentResponse>(comment);
            return commentResponse;
        }

        public async Task<CommentResponse> Create(CommentCreateRequest request)
        {
            var userCheck =  await _userRepository.GetByIdAsync(Guid.Parse(request.UserId));
            if (userCheck == null)
            {
                throw new Exception("User id is invalid.");
            }

            if (request.RelatedId != null)
            {
                var commentCheck =  await _commentRepository.GetByIdAsync(Guid.Parse(request.RelatedId));
                if (commentCheck == null)
                {
                    throw new Exception("Relared Id is invalid.");
                }
            }
            
            var commentCreate = _mapper.Map<Comment>(request);
            commentCreate.CreatedAt = DateTimeOffset.Now;
            var result = await _commentRepository.AddAsync(commentCreate);
            var comment = await _commentRepository.GetByIdAsync(result.Id);
            return _mapper.Map<CommentResponse>(comment);
        }

        public async Task<CommentResponse> Update(CommentUpdateRequest request)
        {
            var commentCheck = await _commentRepository.GetByIdAsync(Guid.Parse(request.Id));
            if (commentCheck == null)
            {
                throw new Exception("ID is invalid.");
            }
            
            var userCheck =  await _userRepository.GetByIdAsync(Guid.Parse(request.UserId));
            if (userCheck == null)
            {
                throw new Exception("User id is invalid.");
            }
            
            if (request.RelatedId != null)
            {
                var commentParentCheck =  await _commentRepository.GetByIdAsync(Guid.Parse(request.RelatedId));
                if (commentParentCheck == null)
                {
                    throw new Exception("Related Id is invalid.");
                }
            }

            var commentUpdate = _mapper.Map<Comment>(request);
            commentUpdate.UserId = commentCheck.Id;
            commentUpdate.CreatedAt = commentCheck.CreatedAt;
            commentUpdate.CreatedBy = commentCheck.CreatedBy;
            commentUpdate.UpdatedAt = DateTimeOffset.Now;
            var result = await _commentRepository.EditAsync(commentUpdate);
            var commentUpdated = await _commentRepository.GetByIdAsync(commentUpdate.Id);
            return result ? _mapper.Map<CommentResponse>(commentUpdated) : null;
        }
    }
}
