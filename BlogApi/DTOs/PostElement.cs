using BlogApi.Enums;

namespace BlogApi.DTOs
{
    public class PostElement
    {
        public short Number { get; set; }
        public PostItemType Type { get; set; }
        public short Content { get; set; }
    }
}
