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

        public static bool IsAllDigits
            (this string value)
        {
            var result = value?.All(char.IsDigit) ?? true;

            return result;
        }

        public static bool IsEmpty
            (this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool IsEqualOrEmpty
            (this string current, string other)
        {
            var result = current.IsEmpty()
                || other.IsEmpty()
                || current == other;

            return result;
        }

        public static string Join<T>
            (this IEnumerable<T> values, string delimiter = ",")
        {
            var result = new StringBuilder();

            if (values != null)
            {
                var distincted = values
                    .Distinct().ToArray();

                foreach (var d in distincted)
                {
                    var current = d?.ToString().Trim();

                    if (!current.IsEmpty())
                    {
                        if (result.Length > 0 && !delimiter.IsEmpty())
                        {
                            result.Append(delimiter);
                        }

                        result.Append(current);
                    }
                }
            }

            return result.ToString();
        }

        public static string Join
            (string delimiter, params string[] values)
        {
            return values.Join(delimiter);
        }

        public static string Join<T, TProp>
            (this IEnumerable<T> items, Func<T, TProp> property,
            string delimiter = ",")
        {
            var values = items
                .Select(i => property?.Invoke(i)?.ToString()
                    ?? i?.ToString())
                .Distinct()
                .Where(i => !i.IsEmpty())
                .ToArray();

            return values.Join(delimiter);
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

        public static string Shrink
            (this string value, int length)
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
                    .Where(p => !p.IsEmpty());

                var count = parts
                    .Sum(p => p.Length);

                var builder = new StringBuilder();

                foreach (string p in parts)
                {
                    var part = Regex.Replace(
                        input: p,
                        pattern: ShrinkRemove,
                        replacement: string.Empty,
                        options: RegexOptions.IgnoreCase | RegexOptions.Multiline);

                    var partShare = (double)part.Length / count;
                    var partLength = (int)Math.Ceiling(
                            (length - builder.Length) * (p == parts.Last() ? 1 : partShare));

                    var current =
                        partLength < part.Length ?
                        part.Substring(0, partLength) :
                        part;
                    builder.Append(current);

                    if (builder.Length >= length)
                    {
                        break;
                    }
                }

                result =
                    builder.Length > length ?
                    builder.ToString().Substring(0, length) :
                    builder.ToString();
            }

            return result;
        }

        public static IEnumerable<T> Split<T>
            (this string value, string delimiter, bool excludeEmpties = false)
        {
            return value.Split<T>(
                delimiter: delimiter[0],
                excludeEmpties: excludeEmpties).ToArray();
        }

        public static IEnumerable<T> Split<T>
            (this string value, char delimiter, bool excludeEmpties = false)
        {
            if (!value.IsEmpty())
            {
                var splitted = value
                    .Split(delimiter)
                    .Where(v => !(excludeEmpties && v.IsEmpty())).ToArray();

                foreach (var s in splitted)
                {
                    yield return s.Trim().Convert<string, T>();
                }
            }
        }

        #endregion Public Methods
    }
}