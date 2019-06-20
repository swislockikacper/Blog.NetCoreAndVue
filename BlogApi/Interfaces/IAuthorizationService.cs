using BlogApi.DTOs;

namespace BlogApi.Interfaces
{
    public interface IAuthorizationService
    {
        string GenerateJSONWebToken(User userInfo);
        User AuthenticateUser(User login);
        User RegisterUser(User user);
    }
}
