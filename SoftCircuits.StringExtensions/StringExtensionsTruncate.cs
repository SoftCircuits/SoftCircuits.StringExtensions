using System;

namespace SoftCircuits.StringExtensions
{
    public static partial class StringExtensions
    {
        /// <summary>
        /// Returns a copy of this string truncated to the specified length.
        /// </summary>
        /// <param name="maxLength">Maximum string length.</param>
        /// <param name="smartTrim">If true, trailing partial words and whitespace are removed, unless
        /// there is not room for at least one whole word.</param>
        /// <param name="appendEllipsis">If true, <c>&quot;...&quot;</c> is appended to the truncated
        /// string. The string is further truncated to make room for the ellipsis.
        /// </param>
        public static string Truncate(this string s, int maxLength, bool smartTrim = true, bool appendEllipsis = true)
        {
            const string ellipsis = "...";

            if (maxLength < 0)
                throw new ArgumentException($"{nameof(maxLength)} cannot be less than zero.");

            if (s.Length > maxLength)
            {
                if (appendEllipsis)
                {
                    if (maxLength > ellipsis.Length)
                        maxLength -= ellipsis.Length;
                    else
                        appendEllipsis = false;
                }

                int length = maxLength;

                if (smartTrim)
                {
                    while (length > 0 && IsWordCharacter(s, length))
                        length--;
                    while (length > 0 && char.IsWhiteSpace(s[length - 1]))
                        length--;
                    if (length == 0)
                        length = maxLength;
                }

                s = s.Substring(0, length);

                if (appendEllipsis)
                    s += ellipsis;
            }

            return s;
        }
    }
}
