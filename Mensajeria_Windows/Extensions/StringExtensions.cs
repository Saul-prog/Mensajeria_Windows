namespace Mensajeria_Windows.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Takes a string if is null returns null, then trims it and if is empry returns null
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string TrimAndAsNullIfEmpty(this string value)
        {
            var result = string.IsNullOrWhiteSpace(value) ? null : value.Trim();

            return string.IsNullOrEmpty(result) ? null : result;
        }

        /// <summary>
        /// Indicates whether the specified string is null or an empty string ("").
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Indicates whether a specified string is null, empty, or consists only of white-space characters.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
    }
}
