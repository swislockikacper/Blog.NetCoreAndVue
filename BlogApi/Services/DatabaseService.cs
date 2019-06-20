using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApi.DTO;
using BlogApi.Interfaces;
using Microsoft.Extensions.Configuration;

namespace BlogApi.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly IConfiguration configuration;
        private readonly IStorageService storageService;

        public DatabaseService(IConfiguration configuration, IStorageService storageService)
        {
            this.configuration = configuration;
            this.storageService = storageService;
        }

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
