using System;
using System.Collections.Generic;

namespace SoftCircuits.StringExtensions
{
    public static partial class StringExtensions
    {
        /// <summary>
        /// Splits a string into a list of string tokens.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <param name="predicate">Delegate to return true for characters that delimit tokens.</param>
        public static List<string> Tokenize(this string s, Func<char, bool> predicate)
        {
            List<string> tokens = new();
            string? token;
            int pos = 0;

            while ((token = s.GetNextToken(predicate, ref pos)) != null)
                tokens.Add(token);

            return tokens;
        }

        /// <summary>
        /// Splits a string into a list of string tokens.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <param name="delimiterChars">String that contains the characters that delimit tokens.</param>
        /// <param name="ignoreCase">If true, upper and lower case characters are considered equal.</param>
        public static List<string> Tokenize(this string s, string delimiterChars, bool ignoreCase = false)
        {
            HashSet<char> hashSet = new(delimiterChars, CharComparer.GetEqualityComparer(ignoreCase));
            List<string> tokens = new();
            string? token;
            int pos = 0;

            while ((token = s.GetNextToken(hashSet.Contains, ref pos)) != null)
                tokens.Add(token);

            return tokens;
        }

        /// <summary>
        /// Returns the next token from this string.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <param name="predicate">Delegate to return true for delimiting characters.</param>
        /// <param name="pos">Current position within the string, updated The starting position. Is updated</param>
        /// <returns>The next token or null if there are no more tokens.</returns>
        public static string? GetNextToken(this string s, Func<char, bool> predicate, ref int pos)
        {
            // Skip delimiters
            while (pos < s.Length && predicate(s[pos]))
                pos++;

            // Parse token
            int start = pos;
            while (pos < s.Length && !predicate(s[pos]))
                pos++;

            // Extract token
            if (pos > start)
                return s[start..pos];

            return null;
        }

        /// <summary>
        /// Returns the next token from this string.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <param name="delimiterChars">String that contains delimiting characters.</param>
        /// <param name="pos"></param>
        /// <param name="ignoreCase"></param>
        /// <returns>The next token or null if there are no more tokens.</returns>
        public static string? GetNextToken(this string s, string delimiterChars, ref int pos, bool ignoreCase = false)
        {
            HashSet<char> hashSet = new(delimiterChars, CharComparer.GetEqualityComparer(ignoreCase));
            return s.GetNextToken(hashSet.Contains, ref pos);
        }
    }
}
