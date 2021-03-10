using StringExtensions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace StringExtensions.Helpers
{
    internal static class Shrinker
    {
        #region Private Fields

        private const string ShrinkRemove = @"(?<!\b[aeiuoáăâäàāąåãæéĕěêëėèēęíĭîïìīįĩóŏôöòōøõőůúŭûüűùūųũ]*)[aeiouáăâäàāąåãæéĕěêëėèēęíĭîïìīįĩóŏôöòōøõőůúŭûüűùūųũ]|\s|\<\>";
        private const string ShrinkSplitAt = @"[_\W]";

        #endregion Private Fields

        #region Public Methods

        public static string GetShrinked(this string value, int maxLength, bool leaveWhitespaces = false,
            bool toCamelCases = false)
        {
            var parts = GetParts(
                value: value,
                maxLength: maxLength,
                leaveWhitespaces: leaveWhitespaces).ToArray();
            SetShrinkeds(
                parts: parts,
                maxLength: maxLength,
                toCamelCases: toCamelCases);

            var result = GetOutput(parts);
            return result;
        }

        #endregion Public Methods

        #region Private Methods

        private static string GetOutput(IEnumerable<Part> parts)
        {
            var result = new StringBuilder();

            var orderedParts = parts
                .OrderBy(p => p.Index).ToArray();

            foreach (var orderedPart in orderedParts)
            {
                result.Append(orderedPart.Shrinked);
            }

            return result.ToString();
        }

        private static IEnumerable<Part> GetParts(string value, int maxLength, bool leaveWhitespaces)
        {
            var input = value?.Trim();

            if (!input.IsEmpty()
                && (!leaveWhitespaces || maxLength == 0 || input.Length > maxLength))
            {
                var parts = Regex.Split(
                    input: input,
                    pattern: ShrinkSplitAt,
                    options: RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline)
                    .Select(p => p.Trim())
                    .Where(p => !p.IsEmpty()).ToArray();

                var index = 0;

                foreach (var part in parts)
                {
                    var result = new Part
                    {
                        Given = part,
                        Index = index++,
                    };

                    yield return result;
                }
            }
        }

        private static void SetShrinkeds(IEnumerable<Part> parts, int maxLength, bool toCamelCases)
        {
            var givenLength = (double)parts.Sum(p => p.Given.Length);
            var shrinkedLength = maxLength;

            var orderedParts = parts
                .OrderBy(p => p.Index == 0)
                .ThenBy(p => p.Given.Length).ToArray();

            foreach (var orderedPart in orderedParts)
            {
                orderedPart.Shrinked = Regex.Replace(
                    input: orderedPart.Given,
                    pattern: ShrinkRemove,
                    replacement: string.Empty,
                    options: RegexOptions.IgnoreCase | RegexOptions.Multiline);

                var parthLength = orderedPart != orderedParts.Last()
                    ? Convert.ToInt32(Math.Floor(orderedPart.Given.Length / givenLength * shrinkedLength))
                    : shrinkedLength;

                if (orderedPart.Shrinked.Length > parthLength)
                {
                    orderedPart.Shrinked = orderedPart.Shrinked.Substring(
                        startIndex: 0,
                        length: parthLength);
                }

                if (toCamelCases
                    && orderedPart.Index > 0
                    && orderedPart.Shrinked.Length > 0)
                {
                    var first = orderedPart.Shrinked.Substring(
                        startIndex: 0,
                        length: 1).ToUpper();

                    var others = orderedPart.Shrinked.Length > 1
                        ? orderedPart.Shrinked.Substring(1).ToLower()
                        : string.Empty;

                    orderedPart.Shrinked = first + others;
                }

                givenLength -= orderedPart.Given.Length;
                shrinkedLength -= orderedPart.Shrinked.Length;
            }
        }

        #endregion Private Methods
    }
}