using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SoftCircuits.StringExtensions
{
    public static partial class StringExtensions
    {
        /// <summary>
        /// Returns a copy of this string with the case changed according to <paramref name="caseType"/>.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <param name="caseType"></param>
        /// <returns>The modified string.</returns>
        public static string SetCase(this string s, CaseType caseType)
        {
            return caseType switch
            {
                CaseType.Lower => s.ToLower(),
                CaseType.Upper => s.ToUpper(),
                CaseType.CapitalizeFirstCharacter => SetCapitalizeFirstCharacter(s),
                CaseType.Sentence => SetSentenceCase(s),
                CaseType.Title => SetTitleCase(s),
                _ => s,
            };
        }

        /// <summary>
        /// Converts the first character in <paramref name="s"/> to upper case.
        /// </summary>
        private static string SetCapitalizeFirstCharacter(string s)
        {
            StringBuilder builder = new(s);

            if (builder.Length > 0)
                builder[0] = char.ToUpper(builder[0]);

            return builder.ToString();
        }

        /// <summary>
        /// Converts a string to sentence case.
        /// </summary>
        private static string SetSentenceCase(string s)
        {
            StringBuilder builder = new(s);

            bool inSentence = false;
            int wordStart = -1;
            int i;

            for (i = 0; i < builder.Length; i++)
            {
                if (IsWordCharacter(s, i))
                {
                    if (wordStart == -1)
                        wordStart = i;
                }
                else if (wordStart != -1)
                {
                    SetWordSentenceCase(builder, wordStart, i, ref inSentence);
                    wordStart = -1;
                }
            }

            if (wordStart != -1)
                SetWordSentenceCase(builder, wordStart, i, ref inSentence);

            return builder.ToString();
        }

        private static void SetWordSentenceCase(StringBuilder builder, int wordStart, int wordEnd, ref bool inSentence)
        {
            Debug.Assert(wordStart != -1);
            Debug.Assert(wordStart <= wordEnd);
            Debug.Assert(wordEnd <= builder.Length);

            // Set word to lower case if not acronym
            if (IncludesLowerCase(builder, wordStart, wordEnd))
                EachChar(builder, wordStart, wordEnd, c => char.ToLower(c));

            if (!inSentence)
            {
                builder[wordStart] = char.ToUpper(builder[wordStart]);
                inSentence = true;
            }

            if (inSentence && wordEnd < builder.Length && IsEndOfSentenceCharacter(builder, wordEnd))
                inSentence = false;
        }

        // TODO: ???

        public static HashSet<string> UncapitalizedTitleWords { get; set; } = new(StringComparer.OrdinalIgnoreCase)
        {
            "a",
            "about",
            "after",
            "an",
            "and",
            "are",
            "around",
            "as",
            "at",
            "be",
            "before",
            "but",
            "by",
            "else",
            "for",
            "from",
            "how",
            "if",
            "in",
            "is",
            "into",
            "nor",
            "of",
            "on",
            "or",
            "over",
            "than",
            "that",
            "the",
            "then",
            "this",
            "through",
            "to",
            "under",
            "when",
            "where",
            "why",
            "with"
        };

        /// <summary>
        /// Converts a string to title case.
        /// </summary>
        private static string SetTitleCase(string s)
        {
            StringBuilder builder = new(s);

            bool inSentence = false;
            int wordStart = -1;
            int i;

            for (i = 0; i < builder.Length; i++)
            {
                if (IsWordCharacter(s, i))
                {
                    if (wordStart == -1)
                        wordStart = i;
                }
                else if (wordStart != -1)
                {
                    SetWordTitleCase(builder, wordStart, i, ref inSentence);
                    wordStart = -1;
                }
            }

            if (wordStart != -1)
                SetWordTitleCase(builder, wordStart, i, ref inSentence);

            return builder.ToString();
        }

        private static void SetWordTitleCase(StringBuilder builder, int wordStart, int wordEnd, ref bool inSentence)
        {
            Debug.Assert(wordStart != -1);
            Debug.Assert(wordStart <= wordEnd);
            Debug.Assert(wordEnd <= builder.Length);

            // Set word to lower case if not acronym
            if (IncludesLowerCase(builder, wordStart, wordEnd))
                EachChar(builder, wordStart, wordEnd, c => char.ToLower(c));

            if (!inSentence || !UncapitalizedTitleWords.Contains(builder.ToString(wordStart, wordEnd - wordStart)))
            {
                builder[wordStart] = char.ToUpper(builder[wordStart]);
                inSentence = true;
            }

            if (inSentence && wordEnd < builder.Length && IsEndOfSentenceCharacter(builder, wordEnd))
                inSentence = false;
        }
    }
}
