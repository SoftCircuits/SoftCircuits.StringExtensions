using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SoftCircuits.StringExtensions
{
    public static partial class StringExtensions
    {

        #region Null and empty strings

        /// <summary>
        /// Returns this string, or an empty string if this string is null.
        /// </summary>
        public static string EmptyIfNull(this string? s) => s ?? string.Empty;

        /// <summary>
        /// Returns this string, or null if this string is empty.
        /// </summary>
        public static string? NullIfEmpty(this string? s) => string.IsNullOrEmpty(s) ? null : s;

        /// <summary>
        /// Returns this string, or an empty string if this string is null or contains only whitespace.
        /// </summary>
        public static string EmptyIfNullOrWhiteSpace(this string? s) => string.IsNullOrWhiteSpace(s) ? string.Empty : s;

        /// <summary>
        /// Returns this string, or null if this string is null or contains only whitespace.
        /// </summary>
        public static string? NullIfEmptyOrWhiteSpace(this string? s) => string.IsNullOrWhiteSpace(s) ? null : s;

        #endregion

        #region Whitespace

        /// <summary>
        /// Returns a copy of this tring with all whitespace sequences replaced with a single space
        /// character and all leading and trailing whitespace removed.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <returns>The modified string.</returns>
        public static string NormalizeWhiteSpace(this string s)
        {
            bool wasSpace = false;

            StringBuilder builder = new(s.Length);

            foreach (char c in s)
            {
                if (char.IsWhiteSpace(c))
                {
                    if (builder.Length > 0)
                        wasSpace = true;
                }
                else
                {
                    if (wasSpace)
                        builder.Append(' ');
                    builder.Append(c);
                    wasSpace = false;
                }
            }

            return builder.ToString();
        }

        #endregion

        #region Count words

        /// <summary>
        /// Counts the number of words in this string. Words are separated by one or more whitespace
        /// character.
        /// </summary>
        /// <returns></returns>
        public static int CountWords(this string s)
        {
            bool wasSpace = true;
            int words = 0;

            foreach (char c in s)
            {
                if (char.IsWhiteSpace(c))
                {
                    wasSpace = true;
                }
                else
                {
                    if (wasSpace)
                        words++;
                    wasSpace = false;
                }
            }

            return words;
        }

        #endregion

        #region Reverse

        /// <summary>
        /// Returns a copy of this string with the characters in reverse order.
        /// </summary>
        /// <param name="s">This string.</param>
        public static string Reverse(this string s)
        {
            StringBuilder builder = new(s.Length);

            for (int i = s.Length - 1; i >= 0; i--)
                builder.Append(s[i]);

            return builder.ToString();
        }

        #endregion

        #region Distinct

        /// <summary>
        /// Returns a string that includes exactly one occurrence of each character from this string.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <param name="ignoreCase">If true, upper and lower case characters are considered equal.</param>
        public static string Distinct(this string s, bool ignoreCase = false)
        {
            StringBuilder builder = new(s.Length);
            HashSet<char> hashSet = new(s.Length, CharComparer.GetEqualityComparer(ignoreCase));

            foreach (char c in s)
            {
                if (hashSet.Add(c))
                    builder.Append(c);
            }

            return builder.ToString();
        }

        #endregion

        #region Union

        /// <summary>
        /// Returns a string that contains a single occurrence of each character that appears either in this string or
        /// <paramref name="unionChars"/>.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <param name="unionChars">Collection of characters to union with this string.</param>
        /// <param name="ignoreCase">If true, lower and upper case characters are considered equal.</param>
        public static string Union(this string s, IEnumerable<char> unionChars, bool ignoreCase = false)
        {
            int unionCharsCount = unionChars.Count();
            StringBuilder builder = new(s.Length + unionCharsCount);
            HashSet<char> hashSet = new(s.Length + unionCharsCount, CharComparer.GetEqualityComparer(ignoreCase));

            foreach (char c in s)
            {
                if (hashSet.Add(c))
                    builder.Append(c);
            }

            foreach (char c in unionChars)
            {
                if (hashSet.Add(c))
                    builder.Append(c);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Returns a string that contains a single occurrence of each character that appears either in this string or
        /// <paramref name="unionChars"/>.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <param name="unionChars">String of characters to union with this string.</param>
        /// <param name="ignoreCase">If true, lower and upper case characters are considered equal.</param>
        public static string Union(this string s, string unionChars, bool ignoreCase = false) => Union(s, (IEnumerable<char>)unionChars, ignoreCase);

        #endregion

        #region Intersect

        /// <summary>
        /// Returns a string that contains a single occurrence of each character that appears in both this string and
        /// <paramref name="intersectChars"/>.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <param name="intersectChars">Collection of characters to intersect with this string.</param>
        /// <param name="ignoreCase">If true, lower and upper case characters are considered equal.</param>
        public static string Intersect(this string s, IEnumerable<char> intersectChars, bool ignoreCase = false)
        {
            StringBuilder builder = new(s.Length);
            HashSet<char> hashSet = new(s.Length, CharComparer.GetEqualityComparer(ignoreCase));

            foreach (char c in intersectChars)
            {
                hashSet.Add(c);
            }

            foreach (char c in s)
            {
                if (hashSet.Remove(c))
                    builder.Append(c);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Returns a string that contains a single occurrence of each character that appears in both this string and
        /// <paramref name="intersectChars"/>.
        /// </summary>
        /// <param name="intersectChars">String of characters to union with this string.</param>
        /// <param name="ignoreCase">If true, lower and upper case characters are considered equal.</param>
        public static string Intersect(this string s, string intersectChars, bool ignoreCase = false) => Intersect(s, (IEnumerable<char>)intersectChars, ignoreCase);

        #endregion

        #region Except

        /// <summary>
        /// Returns a string with a single occurrence of each character from the original string except those characters
        /// found in <paramref name="exceptChars"/>.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <param name="exceptChars">Collection of characters to exclude from the result.</param>
        /// <param name="ignoreCase">If true, lower and upper case characters are considered equal.</param>
        public static string Except(this string s, IEnumerable<char> exceptChars, bool ignoreCase = false)
        {
            StringBuilder builder = new(s.Length);
            HashSet<char> hashSet = new(exceptChars, CharComparer.GetEqualityComparer(ignoreCase));

            foreach (char c in s)
            {
                if (!hashSet.Contains(c))
                    builder.Append(c);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Returns a string with a single occurrence of each character from the original string except those characters
        /// found in <paramref name="exceptChars"/>.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <param name="exceptChars">String of characters to exclude from the result.</param>
        /// <param name="ignoreCase">If true, lower and upper case characters are considered equal.</param>
        public static string Except(this string s, string exceptChars, bool ignoreCase = false) => Except(s, (IEnumerable<char>)exceptChars, ignoreCase);

        #endregion

        #region Sort

        /// <summary>
        /// Returns a copy of this string with the characters sorted.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <param name="ignoreCase">If true, lower and upper case characters are considered equal.</param>
        public static string Sort(this string s, bool ignoreCase = false)
        {
            StringBuilder builder = new(s.Length);
            IComparer<char> comparer = CharComparer.GetComparer(ignoreCase);

            foreach (char c in s.OrderBy(c => c, comparer))
            {
                builder.Append(c);
            }

            return builder.ToString();
        }

        #endregion

        /// <summary>
        /// Returns true if this string contains any of the characters in <paramref name="findChars"/>.
        /// </summary>
        /// <param name="findChars">String of characters to find.</param>
        public static bool ContainsAny(this string s, string findChars, bool ignoreCase = false)
        {
            if (string.IsNullOrEmpty(findChars))
                return false;

            HashSet<char> hashSet = new(findChars, CharComparer.GetEqualityComparer(ignoreCase));

            foreach (char c in s)
            {
                if (hashSet.Contains(c))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Returns a string with spaces inserted between words indicated by camel case.
        /// </summary>
        /// <param name="s"></param>
        /// <returns>The converted string.</returns>
        public static string InsertCamelCaseSpaces(this string s)
        {
            StringBuilder builder = new(s.Length * 2);

            bool lastIsUpper = false;
            bool lastIsWhitespace = false;

            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                bool isUpper = char.IsUpper(c);
                bool nextIsLower = i + 1 < s.Length && char.IsLower(s[i + 1]);

                if (isUpper && builder.Length > 0 && (!lastIsUpper || nextIsLower) && !lastIsWhitespace)
                    builder.Append(' ');

                builder.Append(c);
                lastIsUpper = isUpper;
                lastIsWhitespace = char.IsWhiteSpace(c);
            }

            return builder.ToString();
        }






        /// <summary>
        /// Creates a string... TODO:
        /// </summary>
        /// <param name="s"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        //public static string Filter(this string s, Func<char, bool> predicate)
        //{
        //    StringBuilder builder = new(s.Length);

        //    foreach (char c in s)
        //    {
        //        if (predicate(c))
        //            builder.Append(c);
        //    }

        //    return builder.ToString();
        //}





        #region Helper methods

        private static bool IsWordCharacter(string s, int pos)
        {
            Debug.Assert(pos < s.Length);
            char c = s[pos];
            return char.IsLetterOrDigit(c) ||
                c == '\'' ||
                (c == '.' && pos < s.Length - 1 && char.IsDigit(s[pos + 1]));
        }

        private static bool IsWordCharacter(StringBuilder builder, int pos)
        {
            Debug.Assert(pos < builder.Length);
            char c = builder[pos];
            return char.IsLetterOrDigit(c) ||
                c == '\'' ||
                (c == '.' && pos < builder.Length - 1 && char.IsDigit(builder[pos + 1]));
        }

        private static bool IsEndOfSentenceCharacter(string s, int pos)
        {
            Debug.Assert(pos < s.Length);
            char c = s[pos];
            return c == '!' ||
                c == '?' ||
                c == ':' ||
                (c == '.' && !(pos < (s.Length - 1) && char.IsDigit(s[pos + 1])));
        }

        private static bool IsEndOfSentenceCharacter(StringBuilder builder, int pos)
        {
            Debug.Assert(pos < builder.Length);
            char c = builder[pos];
            return c == '!' ||
                c == '?' ||
                c == ':' ||
                (c == '.' && !(pos < (builder.Length - 1) && char.IsDigit(builder[pos + 1])));
        }

        private static bool IncludesLowerCase(string s, int start, int end)
        {
            Debug.Assert(start <= end);
            Debug.Assert(end <= s.Length);

            for (int i = start; i < end; i++)
            {
                if (char.IsLower(s[i]))
                    return true;
            }

            return false;
        }

        private static bool IncludesLowerCase(StringBuilder builder, int start, int end)
        {
            Debug.Assert(start <= end);
            Debug.Assert(end <= builder.Length);

            for (int i = start; i < end; i++)
            {
                if (char.IsLower(builder[i]))
                    return true;
            }

            return false;
        }

        private static void EachChar(StringBuilder builder, int start, int end, Func<char, char> action)
        {
            Debug.Assert(start <= end);
            Debug.Assert(end <= builder.Length);

            for (int i = start; i < end; i++)
            {
                builder[i] = action(builder[i]);
            }
        }

        #endregion

    }
}
