using BlogApi.DTOs;
using BlogApi.Extensions;
using BlogApi.Interfaces;
using System;

namespace BlogApi.Services
{
    public class ValidationService : IValidationService
    {
        public void ValidatePostContentIsNotEmpty(Post post)
        {
            if (post.Title.IsNullOrEmpty() || post.Elements == null || post.UserId.IsNullOrEmpty())
                throw new ArgumentException("Values of post cannot be empty.");
        }

        public void ValidateIdIsCorrect(int id)
        {
            if (!id.IsCorrectId())
                throw new ArgumentException("Id must be bigger than 0.");
        }
    }
}
