using AutoMapper;
using Data.Entities;
using Repositories.UnitOfWork;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;

namespace Services.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public PostService(IUnitOfWork unitOfWork, 
                           UserManager<User> userManager,
                         
                           IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
           
            _mapper = mapper;
        }

        public async Task<int?> CreatePostAsync(Post post)
        {
            
            
            await _unitOfWork.Repository<Post>().CreateAsync(post);


            return await _unitOfWork.CommitAsync();
        }

        public async Task<bool> DeletePostAsync(int id)
        {
            var repo = _unitOfWork.Repository<Post>();
            var post = await repo.GetByIdAsync(id);
            if (post == null)
                return false;

            var result = await repo.RemoveAsync(post);
            await _unitOfWork.CommitAsync();
            return result;
        }

        public async Task<List<Post>> GetAllPostsAsync()
        {
            var repo = _unitOfWork.Repository<Post>();
            return await repo.AsQueryable()
                            .Where(p => p.IsPublished)
                            .ToListAsync();
        }

        public async Task<Post?> GetPostByIdAsync(int id)
        {
            return await _unitOfWork.Repository<Post>().GetByIdAsync(id);
        }

        public async Task<List<Post>> GetPostsByUserIdAsync(int userId)
        {
            var repo = _unitOfWork.Repository<Post>();
            string authorId = userId.ToString();
            return await repo.AsQueryable()
                             .Where(p => p.AuthorId == authorId)
                             .ToListAsync();
        }

        public async Task<int?> UpdatePostAsync(Post post)
        {
           await _unitOfWork.Repository<Post>().UpdateAsync(post);
            return await _unitOfWork.CommitAsync();
            
        }
    }
}
