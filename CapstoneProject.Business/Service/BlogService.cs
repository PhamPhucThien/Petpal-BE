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
using CapstoneProject.DTO.Request.Blog;
using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.Blog;
using CapstoneProject.Repository.Interface;

namespace CapstoneProject.Business.Service
{
    public class BlogService : IBlogService
    {
        private IBlogRepository _blogRepository;
        private IUserRepository _userRepository;
        private IMapper _mapper;

        public BlogService(IBlogRepository blogRepository, IUserRepository userRepository, IMapper mapper)
        {
            _blogRepository = blogRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        
        public async Task<BaseListResponse<BlogResponse>> GetList(ListRequest request)
        {
            Paging paging = new()
            {
                Page = request.Page,
                Size = request.Size,
                MaxPage = 1
            };
            var listBlog = await _blogRepository.GetWithPaging(paging);
            var listBlogResponse = _mapper.Map<List<BlogResponse>>(listBlog);
            paging.Total = listBlogResponse.Count;
            BaseListResponse<BlogResponse> response = new()
            {
                List = listBlogResponse,
                Paging = paging,
            };
            return response;
        }

        public async Task<BlogResponse> GetById(string blogId)
        {
            var blog = await _blogRepository.GetByIdAsync(Guid.Parse(blogId));
            if (blog == null)
            {
                throw new Exception("Not found Pet with this id");
               
            }
            var blogResponse = _mapper.Map<BlogResponse>(blog);
            return blogResponse;
        }

        public async Task<BlogResponse> Create(BlogCreateRequest request)
        {
            var userCheck =  await _userRepository.GetByIdAsync(Guid.Parse(request.UserID));
            if (userCheck == null)
            {
                throw new Exception("User id is invalid.");
            }
            
            var blogCreate = _mapper.Map<Blog>(request);
            blogCreate.CreatedAt = DateTimeOffset.Now;
            blogCreate.CreatedBy = request.CreatedBy;
            var result = await _blogRepository.AddAsync(blogCreate);
            var blog = await _blogRepository.GetByIdAsync(result.Id);
            return _mapper.Map<BlogResponse>(blog);
        }

        public async Task<BlogResponse> Update(BlogUpdateRequest request)
        {
            var blogCheck = await _blogRepository.GetByIdAsync(Guid.Parse(request.Id));
            if (blogCheck == null)
            {
                throw new Exception("ID is invalid.");
            }

            var blogUpdate = _mapper.Map<Blog>(request);
            blogUpdate.UserId = blogCheck.UserId;
            blogUpdate.CreatedAt = blogCheck.CreatedAt;
            blogUpdate.CreatedBy = blogCheck.CreatedBy;
            blogUpdate.UpdatedAt = DateTimeOffset.Now;
            blogUpdate.UpdatedBy = request.UpdatedBy;
            var result = await _blogRepository.EditAsync(blogUpdate);
            blogUpdate.User = blogCheck.User;
            return result ? _mapper.Map<BlogResponse>(blogUpdate) : null;
        }
    }
}
