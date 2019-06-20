using BlogApi.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BlogApi.Services
{
    public class StorageService : IStorageService
    {
        public Task UploadFile(int blogId, IFormFile file)
        {
            throw new System.NotImplementedException();
        }
    }
}
