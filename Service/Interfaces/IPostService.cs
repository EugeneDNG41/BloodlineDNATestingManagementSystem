using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IPostService
    {
        Task<List<Post>> GetAllPostsAsync();
        Task<Post?> GetPostByIdAsync(int id);
        Task<int?> CreatePostAsync(Post post);
        Task<int?> UpdatePostAsync(Post post);
        Task<bool> DeletePostAsync(int id);
        Task<List<Post>> GetPostsByUserIdAsync(int userId);

       
    }
}
