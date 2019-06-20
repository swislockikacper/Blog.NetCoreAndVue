namespace BlogApi.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string value) 
            => value == null || value.Length == 0 ? true : false;
    }
}
