namespace BlogApi.Extensions
{
    public static class IntExtensions
    {
        public static bool IsCorrectId(this int value)
            => value > 0 ? true : false;
    }
}
