using BlogApi.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogApi.Interfaces
{
    public interface IDatabaseService
    {
        Task<IEnumerable<Post>> Posts();
        Task<Post> PostById(int id);
        Task CreatePost(Post post);
        Task EditPost(Post post);
        Task DeletePost(int id);
    }
}
