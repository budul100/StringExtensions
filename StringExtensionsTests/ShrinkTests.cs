using NUnit.Framework;
using StringExtensions;

namespace StringExtensionsTests
{
    public class ShrinkTests
    {
        #region Public Methods

        [Test]
        public void ShortenWithEnoughLength()
        {
            var text = "Ulm Hbf";
            var result = text.Shorten(20);

            Assert.IsTrue(result.Length == text.Length);
        }

        [Test]
        public void ShortenWithoutShrink()
        {
            var result = ("Ulm Hbf").Shorten(5);

            Assert.IsTrue(result.Contains(" ", comparisonType: System.StringComparison.InvariantCulture));
            Assert.IsTrue(result.Length == 5);
        }

        [Test]
        public void ShortenWithShrink()
        {
            var result = ("Ulm Hbf").Shorten(
                maxLength: 5,
                shrinkIfNecessary: true);

            Assert.IsFalse(result.Contains(" ", comparisonType: System.StringComparison.InvariantCulture));
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