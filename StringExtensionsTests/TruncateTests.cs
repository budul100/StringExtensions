using NUnit.Framework;
using StringExtensions;

namespace StringExtensionsTests
{
    public class TruncateTests
    {
        #region Public Methods

        [Test]
        public void TruncateWithEnoughLength()
        {
            var text = "Ulm Hbf";
            var result = text.Truncate(20);

            Assert.IsTrue(result.Length == text.Length);
        }

        [Test]
        public void TruncateWithExtensionAndEnoughLength()
        {
            var text = "Ulm Hbf";
            var result = text.Truncate(
                maxLength: 5,
                extension: "...");

            Assert.IsTrue(result == "Ul...");
        }

        [Test]
        public void TruncateWithExtensionAndNotEnoughLength()
        {
            var text = "Ulm Hbf";
            var result = text.Truncate(
                maxLength: 2,
                extension: "...");

            Assert.IsTrue(result == "...");
        }

        [Test]
        public void TruncateWithoutShrink()
        {
            var result = ("Ulm Hbf").Truncate(5);

            Assert.IsTrue(result.Contains(" ", comparisonType: System.StringComparison.InvariantCulture));
            Assert.IsTrue(result.Length == 5);
        }

        [Test]
        public void TruncateWithShrink()
        {
            var result = ("Ulm Hbf").Truncate(
                maxLength: 5,
                shrinkIfNecessary: true);

            Assert.IsFalse(result.Contains(" ", comparisonType: System.StringComparison.InvariantCulture));
        }

        #endregion Public Methods
    }
}