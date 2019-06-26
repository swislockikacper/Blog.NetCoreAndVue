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
        private readonly IStorageService storageService;
        private readonly IValidationService validationService;

        public DatabaseService(IConfiguration configuration, IStorageService storageService, IValidationService validationService)
        {
            this.configuration = configuration;
            this.storageService = storageService;
            this.validationService = validationService;
        }

        //todo optimalization and delete sql injection
        public async Task<Post> CreatePost(Post post)
        {
            validationService.ValidatePostContentIsNotEmpty(post);

            using(var dbConnection = new SqlConnection(configuration[Database.ConnectionStringPath]))
            {
                var insert = $"INSERT INTO {Database.Post} ([Title], [UserId], [Created]) VALUES (@Title, @UserId, @Created)";

                await dbConnection.ExecuteAsync(insert, new
                {
                    Title = post.Title,
                    UserId = post.UserId,
                    Created = DateTime.UtcNow.ToTimestamp()
                });

                var query = $"SELECT TOP(1) [Id] FROM {Database.Post} ORDER BY [Id] DESC";
                var postId = await dbConnection.QueryAsync<int>(query);

                insert = $"INSERT INTO {Database.PostElement} ([Type], [Number], [Content], [PostId]) VALUES (@Type, @Number, @Content, @PostId)";

                foreach (var item in post.Elements)
                {
                    await dbConnection.ExecuteAsync(insert, new
                    {
                        Type = item.Type,
                        Number = item.Number,
                        Content = item.Content,
                        PostId = postId
                    });
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

                await dbConnection.ExecuteAsync("[dbo].[DeletePost]", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        //todo optimalization and delete sql injection
        public async Task<Post> EditPost(Post post)
        {
            validationService.ValidatePostContentIsNotEmpty(post);
            validationService.ValidateIdIsCorrect(post.Id);

            using (var dbConnection = new SqlConnection(configuration[Database.ConnectionStringPath]))
            {
                var set = $"UPDATE {Database.Post} SET [Title] = @Title WHERE [Id] = {post.Id}";

                await dbConnection.ExecuteAsync(set, new
                {
                    Title = post.Title
                });

                foreach (var item in post.Elements)
                {
                    set = $"UPDATE {Database.PostElement} SET [Type] = @Type, [Number] = @Number, [Content] = @Content  WHERE [Id] = {item.Id}";

                    await dbConnection.ExecuteAsync(set, new
                    {
                        Type = item.Type,
                        Number = item.Number,
                        Content = item.Content
                    });
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
                    $"SELECT * FROM {Database.PostElement} WHERE ]PostId] = @Id";

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

                var posts = results.Read<Post>();
                var postsElements = results.Read<PostElement>();

                foreach (var post in posts)
                {
                    post.Elements = postsElements.Where(pe => pe.PostId == post.Id).ToList();
                }

                return posts;
            }
        }
    }
}