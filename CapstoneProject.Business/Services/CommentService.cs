using AutoMapper;
using CapstoneProject.Business.Interfaces;
using CapstoneProject.Database.Model;
using CapstoneProject.DTO.Request;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.Comment;
using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.Comment;
using CapstoneProject.Repository.Interface;

namespace CapstoneProject.Business.Services
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
            Tuple<List<Comment>, int> listComment = await _commentRepository.GetWithPaging(paging);
            List<CommentResponse> listCommentResponse = _mapper.Map<List<CommentResponse>>(listComment.Item1);
            paging.MaxPage = listComment.Item2;
            BaseListResponse<CommentResponse> response = new()
            {
                List = listCommentResponse,
                Paging = paging,
            };
            return response;
        }

        public async Task<CommentResponse> GetById(string commentId)
        {
            Comment? comment = await _commentRepository.GetByIdAsync(Guid.Parse(commentId));
            if (comment == null)
            {
                throw new Exception("Not found Comment with this id");

            }
            CommentResponse commentResponse = _mapper.Map<CommentResponse>(comment);
            return commentResponse;
        }

        public async Task<CommentResponse> Create(CommentCreateRequest request)
        {
            User? userCheck = await _userRepository.GetByIdAsync(Guid.Parse(request.UserId));
            if (userCheck == null)
            {
                throw new Exception("User id is invalid.");
            }

            if (request.RelatedId != null)
            {
                Comment? commentCheck = await _commentRepository.GetByIdAsync(Guid.Parse(request.RelatedId));
                if (commentCheck == null)
                {
                    throw new Exception("Relared Id is invalid.");
                }
            }

            Comment commentCreate = _mapper.Map<Comment>(request);
            commentCreate.CreatedAt = DateTimeOffset.Now;
            Comment? result = await _commentRepository.AddAsync(commentCreate);
            Comment? comment = await _commentRepository.GetByIdAsync(result.Id);
            return _mapper.Map<CommentResponse>(comment);
        }

        public async Task<CommentResponse?> Update(CommentUpdateRequest request)
        {
            Comment? commentCheck = await _commentRepository.GetByIdAsync(Guid.Parse(request.Id));
            if (commentCheck == null)
            {
                throw new Exception("ID is invalid.");
            }

            User? userCheck = await _userRepository.GetByIdAsync(Guid.Parse(request.UserId));
            if (userCheck == null)
            {
                throw new Exception("User id is invalid.");
            }

            if (request.RelatedId != null)
            {
                Comment? commentParentCheck = await _commentRepository.GetByIdAsync(Guid.Parse(request.RelatedId));
                if (commentParentCheck == null)
                {
                    throw new Exception("Related Id is invalid.");
                }
            }

            Comment commentUpdate = _mapper.Map<Comment>(request);
            commentUpdate.UserId = commentCheck.Id;
            commentUpdate.CreatedAt = commentCheck.CreatedAt;
            commentUpdate.CreatedBy = commentCheck.CreatedBy;
            commentUpdate.UpdatedAt = DateTimeOffset.Now;
            bool result = await _commentRepository.EditAsync(commentUpdate);
            Comment? commentUpdated = await _commentRepository.GetByIdAsync(commentUpdate.Id);
            return result ? _mapper.Map<CommentResponse>(commentUpdated) : null;
        }
    }
}
