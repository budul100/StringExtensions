using NUnit.Framework;
using StringExtensions;
using System.Linq;

namespace StringExtensionsTests
{
    public class Tests
    {
        #region Public Methods

        [Test]
        public void ShortenWithWhitespace()
        {
            var result = ("Ulm Hbf").Shorten(10);

            Assert.IsTrue(result.Contains(" ", comparisonType: System.StringComparison.InvariantCulture));
        }

        [Test]
        public void ShrinkWithWhitespace()
        {
            var result = ("Ulm Hbf").Shrink(10);

            Assert.IsFalse(result.Contains(" ", comparisonType: System.StringComparison.InvariantCulture));
        }

        [Test]
        public void SplitEmptyWithCharSeparator()
        {
            var result = (default(string)).Split<int>(',').ToArray();

            Assert.IsTrue(result.Count() == 0);
        }

        [Test]
        public void SplitEmptyWithStringSeparator()
        {
            var result = (default(string)).Split<int>(",;").ToArray();

            Assert.IsTrue(result.Count() == 0);
        }

        [Test]
        public void SplitWithCharSeparator()
        {
            var result = ("1,2,3").Split<int>(',').ToArray();

            Assert.IsTrue(result.Count() == 3);
        }

        [Test]
        public void SplitWithStringSeparator()
        {
            var result = ("1,2;3").Split<int>(",;").ToArray();

            Assert.IsTrue(result.Count() == 3);
        }

        #endregion Public Methods
    }
}