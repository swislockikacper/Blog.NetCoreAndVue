using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BlogApi.Constants;
using BlogApi.DTOs;
using BlogApi.Extensions;
using BlogApi.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace BlogApi.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly IConfiguration configuration;
       // private readonly IStorageService storageService;
        private readonly IValidationService validationService;

        public DatabaseService(IConfiguration configuration,/* IStorageService storageService,*/ IValidationService validationService)
        {
            this.configuration = configuration;
            //this.storageService = storageService;
            this.validationService = validationService;
        }

        public async Task<Post> CreatePost(Post post)
        {
            validationService.ValidatePostContentIsNotEmpty(post);

            using(var dbConnection = new SqlConnection(configuration[Database.ConnectionStringPath]))
            {
                var insert = $"INSERT INTO {Database.Post} ([Title], [UserId], [Created]) VALUES (@Title, @UserId, @Created)";

                var postParameters = new DynamicParameters();

                postParameters.Add("@Title", post.Title);
                postParameters.Add("@UserId", post.UserId);
                postParameters.Add("@Created", DateTime.UtcNow.ToTimestamp());

                await dbConnection.ExecuteAsync(insert, postParameters);

                var query = $"SELECT TOP(1) [Id] FROM {Database.Post} ORDER BY [Id] DESC";
                var postId = await dbConnection.QueryAsync<int>(query);

                insert = $"INSERT INTO {Database.PostElement} ([Type], [Number], [Content], [PostId]) VALUES (@Type, @Number, @Content, @PostId)";

                post.Id = postId.FirstOrDefault();

                foreach (var item in post.Elements)
                {
                    item.PostId = post.Id;

                    var postElementParameters = new DynamicParameters();

                    postElementParameters.Add("@Type", item.Type);
                    postElementParameters.Add("@Number", item.Number);
                    postElementParameters.Add("@Content", item.Content);
                    postElementParameters.Add("@PostId", item.PostId);

                    await dbConnection.ExecuteAsync(insert, postElementParameters);
                }
            }

            return post;
        }

        public async Task DeletePost(int id)
        {
            validationService.ValidateIdIsCorrect(id);

            using (var dbConnection = new SqlConnection(configuration[Database.ConnectionStringPath]))
            {
                var parameters = new DynamicParameters();

                parameters.Add("@Id", id);

                await dbConnection.ExecuteAsync(Database.DeletePostProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Post> EditPost(Post post)
        {
            validationService.ValidatePostContentIsNotEmpty(post);
            validationService.ValidateIdIsCorrect(post.Id);

            using (var dbConnection = new SqlConnection(configuration[Database.ConnectionStringPath]))
            {
                var postParameters = new DynamicParameters();

                postParameters.Add("@Id", post.Id);
                postParameters.Add("@Title", post.Title);

                var update = $"UPDATE {Database.Post} SET [Title] = @Title WHERE [Id] = @Id";

                await dbConnection.ExecuteAsync(update, postParameters);

                var postElementsDeleteParameters = new DynamicParameters();

                postElementsDeleteParameters.Add("@Id", post.Id);

                update = $"DELETE FROM {Database.PostElement} WHERE [PostId] = @Id";

                await dbConnection.ExecuteAsync(update, postElementsDeleteParameters);

                foreach (var item in post.Elements)
                {
                    item.PostId = post.Id;

                    var postElementParameters = new DynamicParameters();

                    postElementParameters.Add("@Type", item.Type);
                    postElementParameters.Add("@Number", item.Number);
                    postElementParameters.Add("@Content", item.Content);
                    postElementParameters.Add("@PostId", item.PostId);

                    update = $"INSERT INTO {Database.PostElement} ([Type], [Number], [Content], [PostId]) VALUES (@Type, @Number, @Content, @PostId)";

                    await dbConnection.ExecuteAsync(update,postElementParameters);
                }
            }

            return post;
        }

        public async Task<Post> PostById(int id)
        {
            validationService.ValidateIdIsCorrect(id);

            using (var dbConnection = new SqlConnection(configuration[Database.ConnectionStringPath]))
            {
                var parameters = new DynamicParameters();

                parameters.Add("@Id", id);

                var queries = $"SELECT * FROM {Database.Post} WHERE [Id] = @Id" +
                    $"; " +
                    $"SELECT * FROM {Database.PostElement} WHERE [PostId] = @Id";

                var results = await dbConnection.QueryMultipleAsync(queries, parameters);

                var post = results.Read<Post>().FirstOrDefault();
                post.Elements = results.Read<PostElement>().ToList();

                return post;
            }
        }

        public async Task<IEnumerable<Post>> Posts()
        {
            using (var dbConnection = new SqlConnection(configuration[Database.ConnectionStringPath]))
            {
                var queries = $"SELECT * FROM {Database.Post}" +
                    $"; " +
                    $"SELECT * FROM {Database.PostElement}";

                var results = await dbConnection.QueryMultipleAsync(queries);

                var posts = results.Read<Post>().ToList();
                var postsElements = results.Read<PostElement>().ToList();

                foreach (var post in posts)
                {
                    post.Elements = postsElements.Where(pe => pe.PostId == post.Id).ToList();
                }

                return posts;
            }
        }
    }
}