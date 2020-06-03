using NUnit.Framework;
using StringExtensions;

namespace StringExtensionsTests
{
    public class Tests
    {
        #region Public Methods

        [Test]
        public void Shrink()
        {
            var result = ("Ulm Hbf").Shrink();

            Assert.IsFalse(result.Contains(" ", comparisonType: System.StringComparison.InvariantCulture));
        }

        #endregion Public Methods
    }
}