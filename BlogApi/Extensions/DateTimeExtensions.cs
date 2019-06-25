using System;

namespace BlogApi.Extensions
{
    public static class DateTimeExtensions
    {
        public static long ToTimestamp(this DateTime date)
            => ((DateTimeOffset)date).ToUnixTimeSeconds();
    }
}
