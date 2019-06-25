using BlogApi.DTOs;

namespace BlogApi.Interfaces
{
    public interface IValidationService
    {
        void ValidatePostContentIsNotEmpty(Post post);
        void ValidateIdIsCorrect(int id);
    }
}
