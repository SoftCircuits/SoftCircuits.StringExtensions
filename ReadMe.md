# String Extensions

[![NuGet version (SoftCircuits.StringExtensions)](https://img.shields.io/nuget/v/SoftCircuits.StringExtensions.svg?style=flat-square)](https://www.nuget.org/packages/SoftCircuits.StringExtensions/)

```
Install-Package SoftCircuits.StringExtensions
```

## IMPORTANT

**This repository has been deprecated. Please use SoftCircuits.Wordify instead.**

## Overview

Class library provides extension methods that augment the string class. Extension methods add methods to the existing string class and include methods to normalize whitespace, filter and sort characters, parse tokens, truncate to a given length, many options to set case, and static methods that convert numbers to strings, and much more.

### Reference

    public static string EmptyIfNull(this string? s);

 Returns this string, or an empty string if this string is null. 

    public static string? NullIfEmpty(this string? s);

 Returns this string, or null if this string is empty. 

    public static string EmptyIfNullOrWhiteSpace(this string? s);

 Returns this string, or an empty string if this string is null or contains only whitespace. 

    public static string? NullIfEmptyOrWhiteSpace(this string? s);

 Returns this string, or null if this string is null or contains only whitespace. 

    public static string NormalizeWhiteSpace(this string s);

 Returns a copy of this tring with all whitespace sequences replaced with a single space character and all leading and trailing whitespace removed. 

`s`

This string.

Returns

The modified string.

    public static int CountWords(this string s);

 Counts the number of words in this string. Words are separated by one or more whitespace character. 

    public static string Reverse(this string s);

 Returns a copy of this string with the characters in reverse order. 

`s`

This string.

    public static string Distinct(this string s, bool ignoreCase = false);

 Returns a string that includes exactly one occurrence of each character from this string. 

`s`

This string.

`ignoreCase`

If true, upper and lower case characters are considered equal.

    public static string Union(this string s, IEnumerable<char> unionChars, bool ignoreCase = false);

 Returns a string that contains a single occurrence of each character that appears either in this string or `unionChars`. 

`s`

This string.

`unionChars`

Collection of characters to union with this string.

`ignoreCase`

If true, lower and upper case characters are considered equal.

    public static string Union(this string s, string unionChars, bool ignoreCase = false);

 Returns a string that contains a single occurrence of each character that appears either in this string or `unionChars`. 

`s`

This string.

`unionChars`

String of characters to union with this string.

`ignoreCase`

If true, lower and upper case characters are considered equal.

    public static string Intersect(this string s, IEnumerable<char> intersectChars, bool ignoreCase = false);

 Returns a string that contains a single occurrence of each character that appears in both this string and `intersectChars`. 

`s`

This string.

`intersectChars`

Collection of characters to intersect with this string.

`ignoreCase`

If true, lower and upper case characters are considered equal.

    public static string Intersect(this string s, string intersectChars, bool ignoreCase = false);

 Returns a string that contains a single occurrence of each character that appears in both this string and `intersectChars`. 

`intersectChars`

String of characters to union with this string.

`ignoreCase`

If true, lower and upper case characters are considered equal.

    public static string Except(this string s, IEnumerable<char> exceptChars, bool ignoreCase = false);

 Returns a string with a single occurrence of each character from the original string except those characters found in `exceptChars`. 

`s`

This string.

`exceptChars`

Collection of characters to exclude from the result.

`ignoreCase`

If true, lower and upper case characters are considered equal.

    public static string Except(this string s, string exceptChars, bool ignoreCase = false);

 Returns a string with a single occurrence of each character from the original string except those characters found in `exceptChars`. 

`s`

This string.

`exceptChars`

String of characters to exclude from the result.

`ignoreCase`

If true, lower and upper case characters are considered equal.

    public static string Sort(this string s, bool ignoreCase = false);

 Returns a copy of this string with the characters sorted. 

`s`

This string.

`ignoreCase`

If true, lower and upper case characters are considered equal.

    public static bool ContainsAny(this string s, string findChars, bool ignoreCase = false);

 Returns true if this string contains any of the characters in `findChars`. 

`findChars`

String of characters to find.

    public static string InsertCamelCaseSpaces(this string s);

 Returns a string with spaces inserted between words indicated by camel case. 

`s`



Returns

The converted string.

    public static bool IncludesLowerCase(StringBuilder builder, int start, int end);

 Adds two integers and returns the result. 

`left`

 The left operand of the addition. 

`right`

 The right operand of the addition. 

Returns

 The sum of two integers. 

Exceptions

`System.OverflowException`



See Also

`ExampleClass.Label`



    public static string FromNumber(float value, DecimalFormat decimalFormat);

 Converts the given number to a string. 

`value`

Value to convert.

`decimalFormat`

Specifies how to handle the decimal portion.

    public static string FromNumber(double value, DecimalFormat decimalFormat);

 Converts the given number to a string. 

`value`

Value to convert.

`decimalFormat`

Specifies how to handle the decimal portion.

    public static string FromNumber(decimal value, DecimalFormat decimalFormat);

 Converts the given number to a string. 

`value`

Value to convert.

`decimalFormat`

Specifies how to handle the decimal portion.

    public static string FromNumber(int value);

 Converts the given number to a string. 

`value`

Value to convert.

    public static string FromNumber(long value);

 Converts the given number to a string. 

`value`

Value to convert.

    public static string FromNumber(int value, out bool isNegative);

 Converts the given number to a string. 

`value`

Value to convert.

`isNegative`

Variable set to true if the value is negative.

    public static string FromNumber(long value, out bool isNegative);

 Converts the given number to a string. 

`value`

Value to convert.

`isNegative`

Variable set to true if the value is negative.

    public static string SetCase(this string s, CaseType caseType);

 Returns a copy of this string with the case changed according to `caseType`. 

`s`

This string.

`caseType`



Returns

The modified string.

    public static List<string> Tokenize(this string s, Func<char, bool> predicate);

 Splits a string into a list of string tokens. 

`s`

This string.

`predicate`

Delegate to return true for characters that delimit tokens.

    public static List<string> Tokenize(this string s, string delimiterChars, bool ignoreCase = false);

 Splits a string into a list of string tokens. 

`s`

This string.

`delimiterChars`

String that contains the characters that delimit tokens.

`ignoreCase`

If true, upper and lower case characters are considered equal.

    public static string? GetNextToken(this string s, Func<char, bool> predicate, ref int pos);

 Returns the next token from this string. 

`s`

This string.

`predicate`

Delegate to return true for delimiting characters.

`pos`

Current position within the string, updated The starting position. Is updated

Returns

The next token or null if there are no more tokens.

    public static string? GetNextToken(this string s, string delimiterChars, ref int pos, bool ignoreCase = false);

 Returns the next token from this string. 

`s`

This string.

`delimiterChars`

String that contains delimiting characters.

`pos`



`ignoreCase`



Returns

The next token or null if there are no more tokens.

    public static string Truncate(this string s, int maxLength, bool smartTrim = true, bool appendEllipsis = true);

 Returns a copy of this string truncated to the specified length. 

`maxLength`

Maximum string length.

`smartTrim`

If true, trailing partial words and whitespace are removed, unless there is not room for at least one whole word.

`appendEllipsis`

If true, `"..."` is appended to the truncated string. The string is further truncated to make room for the ellipsis. 

