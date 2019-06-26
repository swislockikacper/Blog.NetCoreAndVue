using BlogApi.Enums;
using Microsoft.AspNetCore.Http;

namespace BlogApi.DTOs
{
    public class PostElement
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public short Number { get; set; }
        public PostItemType Type { get; set; }
        public string Content { get; set; }
        public IFormFile File { get; set; }
    }
}
