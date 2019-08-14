using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Extensions
{
    public static class StringExtensions
    {
        #region Private Fields

        private const string ShrinkRemove = @"(?<!\b[AEIOUÄÖÜ]*)[AEIOUÄÖÜ]|\s|\<\>";
        private const string ShrinkSplitAt = @"[_\W]";

        #endregion Private Fields

        #region Public Methods

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

        public static string Join<T>(this IEnumerable<T> values, string delimiter = ",", bool distinct = false)
        {
            var result = new StringBuilder();

            if (values != null)
            {
                var resulting = distinct
                    ? values.Distinct().ToArray()
                    : values.ToArray();

                foreach (var value in resulting)
                {
                    var current = value?.ToString().Trim();

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

        public static string Merge(string delimiter, params string[] values)
        {
            return values.Merge(delimiter);
        }

        public static string Merge<T, TProp>(this IEnumerable<T> items, Func<T, TProp> property, string delimiter = ",")
        {
            return items
                .Select(i => property?.Invoke(i)?.ToString() ?? i?.ToString())
                .Where(i => !i.IsEmpty())
                .Merge(delimiter);
        }

        public static string Merge<T>(this IEnumerable<T> values, string delimiter = ",")
        {
            return values.Join(
                delimiter: delimiter,
                distinct: true);
        }

        public static string Repeat(this string value, int count)
        {
            var result = string.Empty;

            if (count > 0)
            {
                result = new StringBuilder(value.Length * count)
                    .Insert(0, value, count)
                    .ToString();
            }

            return result;
        }

        public static string Shrink(this string value, int length)
        {
            var result = value;

            if (!value.IsEmpty() && length > 0 && value.Length > length)
            {
                var parts = Regex.Split(
                    input: value.ToUpper(),
                    pattern: ShrinkSplitAt,
                    options: RegexOptions.IgnoreCase
                        | RegexOptions.IgnorePatternWhitespace
                        | RegexOptions.Multiline)
                    .Where(p => !p.IsEmpty()).ToArray();

                var fullLength = parts
                    .Sum(p => p.Length);

                var builder = new StringBuilder();

                foreach (string p in parts)
                {
                    var part = Regex.Replace(
                        input: p,
                        pattern: ShrinkRemove,
                        replacement: string.Empty,
                        options: RegexOptions.IgnoreCase | RegexOptions.Multiline);

                    var partShare =
                        (double)part.Length / fullLength;
                    var partLength =
                        (int)Math.Ceiling((length - builder.Length) * (p == parts.Last() ? 1 : partShare));

                    var current = partLength < part.Length
                        ? part.Substring(0, partLength)
                        : part;
                    builder.Append(current);

                    if (builder.Length >= length)
                    {
                        break;
                    }
                }

                result = builder.Length > length
                    ? builder.ToString().Substring(0, length)
                    : builder.ToString();
            }

            return result;
        }

        #endregion Public Methods
    }
}