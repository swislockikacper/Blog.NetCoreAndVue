using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BlogApi.Interfaces
{
    public interface IStorageService
    {
        Task<string> UploadFile(int blogId, IFormFile file);
    }
}
