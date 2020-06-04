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

        private const string ShrinkRemove = @"(?<!\b[aeiuoäöü]*)[aeiouäöü]|\s|\<\>";
        private const string ShrinkSplitAt = @"[_\W]";

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

        public static string Shorten(this string value, int maxLength)
        {
            if (maxLength < 1)
            {
                throw new ArgumentException(
                    message: "The maximum length must be greater than 0.",
                    paramName: nameof(maxLength));
            }

            var result = GetShrinked(
                value: value,
                maxLength: maxLength,
                removeWhitespaces: false);

            return result;
        }

        public static string Shrink(this string value, int maxLength = 0)
        {
            var result = GetShrinked(
                value: value,
                maxLength: maxLength,
                removeWhitespaces: true);

            return result;
        }

        public static IEnumerable<T> Split<T>(this string value, string delimiters, bool excludeEmpties = false)
        {
            if (!value.IsEmpty())
            {
                // Do not use IsEmpty for the delimiters since it can be whitespace, e.g. a line break
                if (delimiters == default)
                {
                    if (typeof(T) == typeof(string))
                    {
                        yield return value.Convert<T>();
                    }
                }
                else
                {
                    var separators = delimiters.ToCharArray();

                    var splits = value
                        .Split(separators)
                        .Where(v => !(excludeEmpties && !string.IsNullOrWhiteSpace(v))).ToArray();

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

        private static string GetShrinked(string value, int maxLength, bool removeWhitespaces)
        {
            var result = value?.Trim();

            if (!result.IsEmpty()
                && (removeWhitespaces || maxLength == 0 || value.Length > maxLength))
            {
                var parts = Regex.Split(
                    input: result,
                    pattern: ShrinkSplitAt,
                    options: RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline)
                    .Where(p => !p.IsEmpty()).ToArray();

                var fullLength = parts
                    .Sum(p => p.Length);

                var builder = new StringBuilder();

                foreach (var part in parts)
                {
                    var corrected = Regex.Replace(
                        input: part,
                        pattern: ShrinkRemove,
                        replacement: string.Empty,
                        options: RegexOptions.IgnoreCase | RegexOptions.Multiline);

                    if (maxLength == 0)
                    {
                        builder.Append(corrected);
                    }
                    else
                    {
                        var partShare = (double)part.Length / fullLength;

                        var partLength = part == parts.Last()
                            ? maxLength - builder.Length
                            : (int)Math.Ceiling(maxLength * partShare);
                        var currentLength = partLength < corrected.Length
                            ? partLength
                            : corrected.Length;

                        var shortened = corrected.Substring(
                            startIndex: 0,
                            length: currentLength);

                        builder.Append(shortened);

                        if (builder.Length >= maxLength)
                        {
                            break;
                        }
                    }
                }

                result = maxLength > 0 && builder.Length > maxLength
                    ? builder.ToString().Substring(0, maxLength)
                    : builder.ToString();
            }

            return result;
        }

        #endregion Private Methods
    }
}