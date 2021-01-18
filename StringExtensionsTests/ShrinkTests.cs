using NUnit.Framework;
using StringExtensions;

namespace StringExtensionsTests
{
    public class ShrinkTests
    {
        #region Public Methods

        [Test]
        public void ShrinkWithWhitespace()
        {
            var result = ("Ulm Hbf").Shrink(10);

            Assert.IsFalse(result.Contains(" ", comparisonType: System.StringComparison.InvariantCulture));
        }

        #endregion Public Methods
    }
}