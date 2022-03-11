using NUnit.Framework;
using StringExtensions;

namespace StringExtensionsTests
{
    public class CheckTests
    {
        #region Public Methods

        [Test]
        public void IsAllDigits()
        {
            Assert.IsFalse("a1".IsAllDigits());
            Assert.IsFalse("aaa".IsAllDigits());
            Assert.IsFalse(string.Empty.IsAllDigits());
            Assert.IsTrue("111".IsAllDigits());
        }

        [Test]
        public void IsConsecutiveDigits()
        {
            Assert.IsFalse("1a1".IsConsecutiveDigits());
            Assert.IsFalse("aaa".IsConsecutiveDigits());
            Assert.IsFalse(string.Empty.IsConsecutiveDigits());
            Assert.IsTrue("111".IsConsecutiveDigits());
            Assert.IsTrue("1a".IsConsecutiveDigits());
            Assert.IsTrue("a1".IsConsecutiveDigits());
        }

        #endregion Public Methods
    }
}