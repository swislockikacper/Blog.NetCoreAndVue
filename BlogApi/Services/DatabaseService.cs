using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BlogApi.Constants;
using BlogApi.DTO;
using BlogApi.Extensions;
using BlogApi.Interfaces;
using Dapper;
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

        public async Task<Post> CreatePost(Post post)
        {
            ValidatePost(post);

            using(var dbConnection = new SqlConnection(configuration[Database.ConnectionStringPath]))
            {
                var insert = $"INSERT INTO {Database.Post} ([Title], [Body]) VALUES (@Title, @Body)";

                await dbConnection.ExecuteAsync(insert, new
                {
                    Title = post.Title,
                    Body = post.Body
                });
            }

            return post;
        }

        public async Task DeletePost(int id)
        {
            ValidateId(id);

            using (var dbConnection = new SqlConnection(configuration[Database.ConnectionStringPath]))
            {
                var delete = $"DELETE FROM {Database.Post} WHERE [Id] = @Id";

                await dbConnection.ExecuteAsync(delete, new
                {
                    Id = id
                });
            }
        }

        public async Task<Post> EditPost(Post post)
        {
            ValidatePost(post);
            ValidateId(post.Id);

            using (var dbConnection = new SqlConnection(configuration[Database.ConnectionStringPath]))
            {
                var set = $"UPDATE {Database.Post} SET [Title] = @Title, [Body] = @Body WHERE [Id] = {post.Id}";

                await dbConnection.ExecuteAsync(set, new
                {
                    Title = post.Title,
                    Body = post.Body
                });
            }

            return post;
        }

        public async Task<Post> PostById(int id)
        {
            ValidateId(id);

            using (var dbConnection = new SqlConnection(configuration[Database.ConnectionStringPath]))
            {
                var query = $"SELECT * FROM {Database.Post} WHERE [Id] = {id}";

                var post = await dbConnection.QueryAsync<Post>(query);

                return post.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<Post>> Posts()
        {
            using (var dbConnection = new SqlConnection(configuration[Database.ConnectionStringPath]))
            {
                var query = $"SELECT * FROM {Database.Post}";

                var posts = await dbConnection.QueryAsync<Post>(query);

                return posts.ToList();
            }
        }

        private void ValidatePost(Post post)
        {
            if (post.Title.IsNullOrEmpty() || post.Body.IsNullOrEmpty())
                throw new ArgumentException("Values of post cannot be empty.");
        }

        private void ValidateId(int id)
        {
            if(!id.IsCorrectId())
                throw new ArgumentException("Id must be bigger than 0.");
        }
    }
}