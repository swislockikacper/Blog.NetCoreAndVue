using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApi.DTO;
using BlogApi.Interfaces;

namespace BlogApi.Services
{
    public class DatabaseService : IDatabaseService
    {
        public Task CreatePost(Post post)
        {
            throw new System.NotImplementedException();
        }

        public Task DeletePost(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task EditPost(Post post)
        {
            throw new System.NotImplementedException();
        }

        public Task<Post> PostById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Post>> Posts()
        {
            throw new System.NotImplementedException();
        }
    }
}
