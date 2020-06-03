using NUnit.Framework;
using StringExtensions;

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

        #endregion Public Methods
    }
}