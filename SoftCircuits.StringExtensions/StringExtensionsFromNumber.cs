using System;
using System.Diagnostics;
using System.Text;

namespace SoftCircuits.StringExtensions
{
    public static partial class StringExtensions
    {
        private static readonly string NegativePrefix = "negative ";

        private static readonly string[] Ones =
        {
            "zero",
            "one",
            "two",
            "three",
            "four",
            "five",
            "six",
            "seven",
            "eight",
            "nine"
        };

        private static readonly string[] Teens =
        {
            "ten",
            "eleven",
            "twelve",
            "thirteen",
            "fourteen",
            "fifteen",
            "sixteen",
            "seventeen",
            "eighteen",
            "nineteen"
        };

        private static readonly string[] Tens =
        {
            "",
            "ten",
            "twenty",
            "thirty",
            "forty",
            "fifty",
            "sixty",
            "seventy",
            "eighty",
            "ninety"
        };

        // US Nnumbering
        private static readonly string[] Thousands =
        {
            "",
            "thousand",
            "million",
            "billion",
            "trillion",
            "quadrillion",
            "quintillion",
            "sextillion",
            "septillion",
            "octillion",
        };

        /// <summary>
        /// Converts the given number to a string.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <param name="decimalFormat">Specifies how to handle the decimal portion.</param>
        public static string FromNumber(float value, DecimalFormat decimalFormat) => FromNumber((decimal)value, decimalFormat);

        /// <summary>
        /// Converts the given number to a string.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <param name="decimalFormat">Specifies how to handle the decimal portion.</param>
        public static string FromNumber(double value, DecimalFormat decimalFormat) => FromNumber((decimal)value, decimalFormat);

        /// <summary>
        /// Converts the given number to a string.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <param name="decimalFormat">Specifies how to handle the decimal portion.</param>
        public static string FromNumber(decimal value, DecimalFormat decimalFormat)
        {
            bool isNegative = false;
            if (value < 0m)
            {
                value = Math.Abs(value);
                isNegative = true;
            }

            string integerPart = value.ToString();
            int i = integerPart.IndexOf('.');
            if (i >= 0)
                integerPart = integerPart[0..i];
            decimal @decimal = value - Math.Truncate(value);

            StringBuilder builder = new();
            FormatNumber(builder, integerPart);
            if (isNegative)
                builder.Insert(0, NegativePrefix);

            // Handle fractional portion
            switch (decimalFormat)
            {
                case DecimalFormat.Currency:
                    builder.AppendFormat(" and {0:00}/100", @decimal * 100);
                    break;
                case DecimalFormat.Fraction:
                    if (@decimal != 0)
                    {
                        builder.Append(" and ");
                        builder.Append(DecimalToFraction(@decimal));
                    }
                    break;
                default:
                    break;
            }

            return builder.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string DecimalToFraction(decimal value)
        {
            if (value == 0m)
                return string.Empty;

            // Consider precision value to convert fractional part to integral equivalent
            long pVal = 1000000000;

            // Calculate GCD of integral equivalent of fractional part and precision value
            long gcd = GetGreatestCommonDivisor((long)Math.Round(value * pVal), pVal);

            // Calculate numerator and denominator
            long numerator = (long)Math.Round(value * pVal) / gcd;
            long denominator = pVal / gcd;

            return $"{numerator}/{denominator}";
        }


        /// <summary>
        /// 
        /// </summary>
        private static long GetGreatestCommonDivisor(long a, long b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a | b;
        }

        /// <summary>
        /// Converts the given number to a string.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        public static string FromNumber(int value) => FromNumber((long)value);

        /// <summary>
        /// Converts the given number to a string.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        public static string FromNumber(long value)
        {
            bool isNegative = false;
            if (value < 0)
            {
                value = Math.Abs(value);
                isNegative = true;
            }

            StringBuilder builder = new();
            FormatNumber(builder, value.ToString());
            if (isNegative)
                builder.Insert(0, NegativePrefix);
            return builder.ToString();
        }

        /// <summary>
        /// Converts the given number to a string.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <param name="isNegative">Variable set to true if the value is negative.</param>
        public static string FromNumber(int value, out bool isNegative) => FromNumber((long)value, out isNegative);

        /// <summary>
        /// Converts the given number to a string.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <param name="isNegative">Variable set to true if the value is negative.</param>
        public static string FromNumber(long value, out bool isNegative)
        {
            if (value < 0)
            {
                value = Math.Abs(value);
                isNegative = true;
            }
            else isNegative = false;

            StringBuilder builder = new();
            FormatNumber(builder, value.ToString());
            return builder.ToString();
        }

        /// <summary>
        /// Converts a integer value to a string.
        /// </summary>
        private static void FormatNumber(StringBuilder builder, string digits)
        {
            string s;
            bool allZeros = true;

            for (int i = digits.Length - 1; i >= 0; i--)
            {
                int ndigit = digits[i] - '0';
                int column = digits.Length - (i + 1);

                // Determine if ones, tens, or hundreds column
                switch (column % 3)
                {
                    case 0:        // Ones position
                        bool showThousands = true;
                        if (i == 0)
                        {
                            // First digit in number (last in loop)
                            s = string.Format("{0} ", Ones[ndigit]);
                        }
                        else if (digits[i - 1] == '1')
                        {
                            // This digit is part of "teen" value
                            s = string.Format("{0} ", Teens[ndigit]);
                            // Skip tens position
                            i--;
                        }
                        else if (ndigit != 0)
                        {
                            // Any non-zero digit
                            s = string.Format("{0} ", Ones[ndigit]);
                        }
                        else
                        {
                            // This digit is zero. If digit in tens and hundreds
                            // column are also zero, don't show "thousands"
                            s = string.Empty;
                            // Test for non-zero digit in this grouping
                            if (digits[i - 1] != '0' || (i > 1 && digits[i - 2] != '0'))
                                showThousands = true;
                            else
                                showThousands = false;
                        }

                        // Show "thousands" if non-zero in grouping
                        if (showThousands)
                        {
                            if (column > 0)
                            {
                                s = string.Format("{0}{1}{2}",
                                    s,
                                    Thousands[column / 3],
                                    allZeros ? " " : ", ");
                            }
                            // Indicate non-zero digit encountered
                            allZeros = false;
                        }
                        builder.Insert(0, s);
                        break;

                    case 1:        // Tens column
                        if (ndigit > 0)
                        {
                            s = string.Format("{0}{1}",
                                Tens[ndigit],
                                (digits[i + 1] != '0') ? "-" : " ");
                            builder.Insert(0, s);
                        }
                        break;

                    case 2:        // Hundreds column
                        if (ndigit > 0)
                        {
                            s = string.Format("{0} hundred ", Ones[ndigit]);
                            builder.Insert(0, s);
                        }
                        break;
                }
            }

            // Trim trailing space
            Debug.Assert(builder.Length > 0 && builder[^1] == ' ');
            builder.Length--;
        }
    }
}
