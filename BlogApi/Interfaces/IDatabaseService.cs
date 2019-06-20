using BlogApi.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogApi.Interfaces
{
    public interface IDatabaseService
    {
        Task<IEnumerable<Post>> Posts();
        Task<Post> PostById(int id);
        Task<Post> CreatePost(Post post);
        Task<Post> EditPost(Post post);
        Task DeletePost(int id);
    }
}
