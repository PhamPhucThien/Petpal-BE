using AutoMapper;
using CapstoneProject.Business.Interfaces;
using CapstoneProject.Database.Model;
using CapstoneProject.DTO.Request;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.Blog;
using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.Blog;
using CapstoneProject.Repository.Interface;

namespace CapstoneProject.Business.Services
{
    public class BlogService(IBlogRepository blogRepository, IUserRepository userRepository, IMapper mapper) : IBlogService
    {
        private readonly IBlogRepository _blogRepository = blogRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseListResponse<BlogResponse>> GetList(ListRequest request)
        {
            Paging paging = new()
            {
                Page = request.Page,
                Size = request.Size,
                Search = request.Search ?? string.Empty,
                MaxPage = 1
            };
            Tuple<List<Blog>, int> listBlog = await _blogRepository.GetWithPaging(paging);
            List<BlogResponse> listBlogResponse = _mapper.Map<List<BlogResponse>>(listBlog.Item1);
            paging.MaxPage = listBlog.Item2;
            BaseListResponse<BlogResponse> response = new()
            {
                List = listBlogResponse,
                Paging = paging,
            };
            return response;
        }

        public async Task<BlogResponse> GetById(string blogId)
        {
            Blog? blog = await _blogRepository.GetByIdAsync(Guid.Parse(blogId));
            if (blog == null)
            {
                throw new Exception("Not found Blog with this id");

            }
            BlogResponse blogResponse = _mapper.Map<BlogResponse>(blog);
            return blogResponse;
        }

        public async Task<BlogResponse> Create(BlogCreateRequest request)
        {
            User? userCheck = await _userRepository.GetByIdAsync(Guid.Parse(request.UserID));
            if (userCheck == null)
            {
                throw new Exception("User id is invalid.");
            }

            Blog blogCreate = _mapper.Map<Blog>(request);
            blogCreate.CreatedAt = DateTimeOffset.Now;
            blogCreate.CreatedBy = request.CreatedBy;
            Blog? result = await _blogRepository.AddAsync(blogCreate);
            Blog? blog = await _blogRepository.GetByIdAsync(result.Id);
            return _mapper.Map<BlogResponse>(blog);
        }

        public async Task<BlogResponse?> Update(BlogUpdateRequest request)
        {
            Blog? blogCheck = await _blogRepository.GetByIdAsync(Guid.Parse(request.Id));
            if (blogCheck == null)
            {
                throw new Exception("ID is invalid.");
            }

            Blog blogUpdate = _mapper.Map<Blog>(request);
            blogUpdate.UserId = blogCheck.UserId;
            blogUpdate.CreatedAt = blogCheck.CreatedAt;
            blogUpdate.CreatedBy = blogCheck.CreatedBy;
            blogUpdate.UpdatedAt = DateTimeOffset.Now;
            blogUpdate.UpdatedBy = request.UpdatedBy;
            bool result = await _blogRepository.EditAsync(blogUpdate);
            blogUpdate.User = blogCheck.User;
            return result ? _mapper.Map<BlogResponse>(blogUpdate) : null;
        }
    }
}
