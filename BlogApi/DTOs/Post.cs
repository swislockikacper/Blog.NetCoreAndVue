using System.Collections.Generic;

namespace BlogApi.DTOs
{
    public class Post
    {
        public int Id { get; set; }
        public long Created { get; set; }
        public string Title { get; set; }
        public string UserId { get; set; }
        public IEnumerable<PostElement> Elements { get; set; }
    }
}
