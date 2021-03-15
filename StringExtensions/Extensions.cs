using StringExtensions.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace StringExtensions
{
    public static class Extensions
    {
        #region Private Fields

        private const string NewLineSeparators = @"[\r\n]+";

        #endregion Private Fields

        #region Public Methods

        public static string Add(this string given, string value, string delimiter = ",")
        {
            var result = new StringBuilder(given);

            var current = value?.Trim();

            if (!current.IsEmpty())
            {
                // Do not use IsEmpty for the delimiter since it can be whitespace, e.g. a line break
                if (result.Length > 0
                    && delimiter != null)
                {
                    result.Append(delimiter);
                }

                result.Append(current);
            }

            return result.ToString();
        }

        public static string AddOnce(this string given, string value, string delimiter = ",")
        {
            var result = given;

            var current = value?.Trim();

            if (!current.IsEmpty())
            {
                if (given.IsEmpty())
                {
                    result = current;
                }
                else if (!given.Contains(current))
                {
                    result = given.Add(
                        value: value,
                        delimiter: delimiter);
                }
            }

            return result;
        }

        public static bool IsAllDigits(this string value)
        {
            var result = value?.All(char.IsDigit) ?? true;

            return result;
        }

        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool IsEqualOrEmpty(this string current, string other)
        {
            var result = current.IsEmpty()
                || other.IsEmpty()
                || current == other;

            return result;
        }

        public static string Join(string delimiter = ",", params string[] values)
        {
            var result = values.Join(delimiter);

            return result;
        }

        public static string Join(this IEnumerable<string> values, string delimiter = ",")
        {
            var result = new StringBuilder();

            if (values?.Any() ?? false)
            {
                foreach (var value in values)
                {
                    var current = value?.Trim();

                    if (!current.IsEmpty())
                    {
                        // Delimiter can be whitespace, e.g. a line break
                        if (result.Length > 0 && delimiter != null)
                        {
                            result.Append(delimiter);
                        }

                        result.Append(current);
                    }
                }
            }

            return result.ToString();
        }

        public static string RemoveAccents(this string value)
        {
            var result = value;

            if (!value.IsEmpty())
            {
                var conversion = new StringBuilder();

                var characters = value.Normalize(NormalizationForm.FormD).ToCharArray();

                foreach (char character in characters)
                {
                    if (CharUnicodeInfo.GetUnicodeCategory(character) != UnicodeCategory.NonSpacingMark)
                    {
                        conversion.Append(character);
                    }
                }

                result = conversion.ToString();
            }

            return result;
        }

        public static string Repeat(this string value, int count)
        {
            var result = new StringBuilder();

            if (count > 0)
            {
                result.Insert(
                    index: 0,
                    value: value,
                    count: count);
            }

            return result.ToString();
        }

        public static string Shrink(this string value, int maxLength = 0, bool toCamelCases = false)
        {
            var result = value.GetShrinked(
                maxLength: maxLength,
                toCamelCases: toCamelCases);

            return result;
        }

        public static IEnumerable<T> Split<T>(this string value, string delimiters, bool excludeEmpties = false)
        {
            if (!value.IsEmpty())
            {
                // Do not use IsEmpty for the delimiters since it can be whitespace, e.g. a line break
                if (string.IsNullOrEmpty(delimiters))
                {
                    if (typeof(T) == typeof(string))
                    {
                        yield return value.Convert<T>();
                    }
                }
                else
                {
                    var splits = Regex.Split(
                        input: value,
                        pattern: delimiters)
                        .Where(v => !excludeEmpties || !v.IsEmpty()).ToArray();

                    foreach (var split in splits)
                    {
                        var result = split.Trim().Convert<T>();

                        yield return result;
                    }
                }
            }
        }

        public static IEnumerable<T> Split<T>(this string value, char delimiter, bool excludeEmpties = false)
        {
            var delimiters = delimiter != default(char)
                ? delimiter.ToString()
                : default;

            var result = value.Split<T>(
                delimiters: delimiters,
                excludeEmpties: excludeEmpties).ToArray();

            return result;
        }

        public static IEnumerable<String> Split(this string value, int length)
        {
            if (length <= 0)
            {
                throw new ArgumentException(
                      message: "The length has to be positive.",
                      paramName: nameof(length));
            }

            if (!value.IsEmpty())
            {
                for (var index = 0; index < value.Length; index += length)
                {
                    var currentLength = Math.Min(
                        length,
                        value.Length - index);

                    yield return value.Substring(
                        startIndex: index,
                        length: currentLength);
                }
            }
        }

        public static IEnumerable<string> SplitLines(this string value, bool excludeEmpties = false)
        {
            var result = value.Split<string>(
                delimiters: NewLineSeparators,
                excludeEmpties: excludeEmpties).ToArray();

            return result;
        }

        public static string ToCamelCases(this string value)
        {
            var result = value;

            value = value?.Trim();

            if (!value.IsEmpty())
            {
                var first = value.Substring(
                    startIndex: 0,
                    length: 1).ToUpper();

                var others = value.Length > 1
                    ? value.Substring(1).ToLower()
                    : string.Empty;

                result = first + others;
            }

            return result;
        }

        public static string ToStandardChars(this string value)
        {
            var result = value;

            if (!value.IsEmpty())
            {
                result = result.Replace('Á', 'A');
                result = result.Replace('Ă', 'A');
                result = result.Replace('Â', 'A');
                result = result.Replace('Ä', 'A');
                result = result.Replace('À', 'A');
                result = result.Replace('Ā', 'A');
                result = result.Replace('Ą', 'A');
                result = result.Replace('Å', 'A');
                result = result.Replace('Ã', 'A');
                result = result.Replace('Æ', 'A');
                result = result.Replace('Ć', 'C');
                result = result.Replace('Č', 'C');
                result = result.Replace('Ç', 'C');
                result = result.Replace('Ĉ', 'C');
                result = result.Replace('Ċ', 'C');
                result = result.Replace('Ď', 'D');
                result = result.Replace('Đ', 'D');
                result = result.Replace('É', 'E');
                result = result.Replace('Ĕ', 'E');
                result = result.Replace('Ě', 'E');
                result = result.Replace('Ê', 'E');
                result = result.Replace('Ë', 'E');
                result = result.Replace('Ė', 'E');
                result = result.Replace('È', 'E');
                result = result.Replace('Ē', 'E');
                result = result.Replace('Ę', 'E');
                result = result.Replace('Ŋ', 'N');
                result = result.Replace('Ð', 'E');
                result = result.Replace('Ğ', 'G');
                result = result.Replace('Ģ', 'G');
                result = result.Replace('Ĝ', 'G');
                result = result.Replace('Ġ', 'G');
                result = result.Replace('Ĥ', 'H');
                result = result.Replace('Ħ', 'H');
                result = result.Replace('Í', 'I');
                result = result.Replace('Ĭ', 'I');
                result = result.Replace('Î', 'I');
                result = result.Replace('Ï', 'I');
                result = result.Replace('İ', 'I');
                result = result.Replace('Ì', 'I');
                result = result.Replace('Ī', 'I');
                result = result.Replace('Į', 'I');
                result = result.Replace('Ĩ', 'I');
                result = result.Replace('Ĵ', 'J');
                result = result.Replace('Ķ', 'K');
                result = result.Replace('Ĺ', 'L');
                result = result.Replace('Ľ', 'L');
                result = result.Replace('Ļ', 'L');
                result = result.Replace('Ŀ', 'L');
                result = result.Replace('Ł', 'L');
                result = result.Replace('Ĳ', 'I');
                result = result.Replace('Œ', 'O');
                result = result.Replace('Ń', 'N');
                result = result.Replace('Ň', 'N');
                result = result.Replace('Ņ', 'N');
                result = result.Replace('Ñ', 'N');
                result = result.Replace('Ó', 'O');
                result = result.Replace('Ŏ', 'O');
                result = result.Replace('Ô', 'O');
                result = result.Replace('Ö', 'O');
                result = result.Replace('Ò', 'O');
                result = result.Replace('Ō', 'O');
                result = result.Replace('Ø', 'O');
                result = result.Replace('Õ', 'O');
                result = result.Replace('Ő', 'O');
                result = result.Replace('Ŕ', 'R');
                result = result.Replace('Ř', 'R');
                result = result.Replace('Ŗ', 'R');
                result = result.Replace('Ś', 'S');
                result = result.Replace('Š', 'S');
                result = result.Replace('Ş', 'S');
                result = result.Replace('Ŝ', 'S');
                result = result.Replace('Ť', 'T');
                result = result.Replace('Ţ', 'T');
                result = result.Replace('Ŧ', 'T');
                result = result.Replace('Þ', 'P');
                result = result.Replace('Ů', 'U');
                result = result.Replace('Ú', 'U');
                result = result.Replace('Ŭ', 'U');
                result = result.Replace('Û', 'U');
                result = result.Replace('Ü', 'U');
                result = result.Replace('Ű', 'U');
                result = result.Replace('Ù', 'U');
                result = result.Replace('Ū', 'U');
                result = result.Replace('Ų', 'U');
                result = result.Replace('Ũ', 'U');
                result = result.Replace('Ŵ', 'W');
                result = result.Replace('Ý', 'Y');
                result = result.Replace('Ŷ', 'Y');
                result = result.Replace('Ÿ', 'Y');
                result = result.Replace('Ź', 'Z');
                result = result.Replace('Ž', 'Z');
                result = result.Replace('Ż', 'Z');
                result = result.Replace('á', 'a');
                result = result.Replace('ă', 'a');
                result = result.Replace('â', 'a');
                result = result.Replace('ä', 'a');
                result = result.Replace('à', 'a');
                result = result.Replace('ā', 'a');
                result = result.Replace('ą', 'a');
                result = result.Replace('å', 'a');
                result = result.Replace('ã', 'a');
                result = result.Replace('æ', 'a');
                result = result.Replace('ć', 'c');
                result = result.Replace('č', 'c');
                result = result.Replace('ç', 'c');
                result = result.Replace('ĉ', 'c');
                result = result.Replace('ċ', 'c');
                result = result.Replace('ď', 'd');
                result = result.Replace('đ', 'd');
                result = result.Replace('ı', 'i');
                result = result.Replace('é', 'e');
                result = result.Replace('ĕ', 'e');
                result = result.Replace('ě', 'e');
                result = result.Replace('ê', 'e');
                result = result.Replace('ë', 'e');
                result = result.Replace('ė', 'e');
                result = result.Replace('è', 'e');
                result = result.Replace('ē', 'e');
                result = result.Replace('ę', 'e');
                result = result.Replace('ŋ', 'n');
                result = result.Replace('ð', 'e');
                result = result.Replace('ğ', 'g');
                result = result.Replace('ģ', 'g');
                result = result.Replace('ĝ', 'g');
                result = result.Replace('ġ', 'g');
                result = result.Replace('ĥ', 'h');
                result = result.Replace('ħ', 'h');
                result = result.Replace('í', 'i');
                result = result.Replace('ĭ', 'i');
                result = result.Replace('î', 'i');
                result = result.Replace('ï', 'i');
                result = result.Replace('ì', 'i');
                result = result.Replace('ī', 'i');
                result = result.Replace('į', 'i');
                result = result.Replace('ĩ', 'i');
                result = result.Replace('ĵ', 'j');
                result = result.Replace('ķ', 'k');
                result = result.Replace('ĸ', 'k');
                result = result.Replace('ĺ', 'l');
                result = result.Replace('ľ', 'l');
                result = result.Replace('ļ', 'l');
                result = result.Replace('ŀ', 'l');
                result = result.Replace('ł', 'l');
                result = result.Replace('ĳ', 'i');
                result = result.Replace('œ', 'o');
                result = result.Replace('ſ', 's');
                result = result.Replace('ń', 'n');
                result = result.Replace('ň', 'n');
                result = result.Replace('ņ', 'n');
                result = result.Replace('ŉ', 'n');
                result = result.Replace('ñ', 'n');
                result = result.Replace('ó', 'o');
                result = result.Replace('ŏ', 'o');
                result = result.Replace('ô', 'o');
                result = result.Replace('ö', 'o');
                result = result.Replace('ò', 'o');
                result = result.Replace('ō', 'o');
                result = result.Replace('ø', 'o');
                result = result.Replace('õ', 'o');
                result = result.Replace('ő', 'o');
                result = result.Replace('ŕ', 'r');
                result = result.Replace('ř', 'r');
                result = result.Replace('ŗ', 'r');
                result = result.Replace('ś', 's');
                result = result.Replace('š', 's');
                result = result.Replace('ş', 's');
                result = result.Replace('ŝ', 's');
                result = result.Replace('ß', 's');
                result = result.Replace('ť', 't');
                result = result.Replace('ţ', 't');
                result = result.Replace('ŧ', 't');
                result = result.Replace('þ', 'p');
                result = result.Replace('ů', 'u');
                result = result.Replace('ú', 'u');
                result = result.Replace('ŭ', 'u');
                result = result.Replace('û', 'u');
                result = result.Replace('ü', 'u');
                result = result.Replace('ű', 'u');
                result = result.Replace('ù', 'u');
                result = result.Replace('ū', 'u');
                result = result.Replace('ų', 'u');
                result = result.Replace('ũ', 'u');
                result = result.Replace('ŵ', 'w');
                result = result.Replace('ý', 'y');
                result = result.Replace('ŷ', 'y');
                result = result.Replace('ÿ', 'y');
                result = result.Replace('ź', 'z');
                result = result.Replace('ž', 'z');
                result = result.Replace('ż', 'z');
            }

            return result;
        }

        public static string Truncate(this string value, int maxLength, string extension)
        {
            if (maxLength < 1)
            {
                throw new ArgumentException(
                    message: "The maximum length must be greater than 0.",
                    paramName: nameof(maxLength));
            }

            if (extension.IsEmpty())
            {
                throw new ArgumentException(
                    message: "The truncate extension must be given.",
                    paramName: nameof(extension));
            }

            var result = value;

            maxLength = maxLength - extension.Length > 0
                ? maxLength - extension.Length
                : 0;

            if ((value?.Length ?? 0) > maxLength)
            {
                result = value.Substring(
                    startIndex: 0,
                    length: maxLength);

                result += extension;
            }

            return result;
        }

        public static string Truncate(this string value, int maxLength, bool shrinkIfNecessary = false)
        {
            if (maxLength < 1)
            {
                throw new ArgumentException(
                    message: "The maximum length must be greater than 0.",
                    paramName: nameof(maxLength));
            }

            var result = value;

            if ((value?.Length ?? 0) > maxLength)
            {
                if (shrinkIfNecessary)
                {
                    result = value.GetShrinked(
                        maxLength: maxLength,
                        leaveWhitespaces: true);
                }
                else
                {
                    result = value.Substring(
                        startIndex: 0,
                        length: maxLength);
                }
            }

            return result;
        }

        #endregion Public Methods

        #region Private Methods

        private static T Convert<T>(this string input)
        {
            object result = default(T);

            if (typeof(T) == typeof(string))
            {
                result = input;
            }
            else
            {
                var type = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);

                if (!input.IsEmpty())
                {
                    result = System.Convert.ChangeType(
                        value: input,
                        conversionType: type,
                        provider: CultureInfo.CurrentCulture);
                }
            }

            return (T)result;
        }

        #endregion Private Methods
    }
}