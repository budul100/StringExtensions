using NUnit.Framework;
using StringExtensions;
using System.Linq;

namespace StringExtensionsTests
{
    public class SplitTests
    {
        #region Public Methods

        [Test]
        public void SplitEmptyWithCharSeparatorToInt()
        {
            var result = (default(string)).Split<int>(',').ToArray();

            Assert.IsTrue(result.Count() == 0);
        }

        [Test]
        public void SplitEmptyWithCharSeparatorToString()
        {
            var result = (default(string)).Split<string>(',').ToArray();

            Assert.IsTrue(result.Count() == 0);
        }

        [Test]
        public void SplitEmptyWithStringSeparatorToInt()
        {
            var result = (default(string)).Split<int>(",;").ToArray();

            Assert.IsTrue(result.Count() == 0);
        }

        [Test]
        public void SplitEmptyWithStringSeparatorToString()
        {
            var result = (default(string)).Split<string>(",;").ToArray();

            Assert.IsTrue(result.Count() == 0);
        }

        [Test]
        public void SplitWithCharSeparatorToInt()
        {
            var result = ("1,2,3").Split<int>(',').ToArray();

            Assert.IsTrue(result.Count() == 3);
        }

        [Test]
        public void SplitWithCharSeparatorToString()
        {
            var result = ("1,2,3").Split<string>(',').ToArray();

            Assert.IsTrue(result.Count() == 3);
        }

        [Test]
        public void SplitWithStringSeparatorToInt()
        {
            var result = ("1,2;3").Split<int>(",;").ToArray();

            Assert.IsTrue(result.Count() == 3);
        }

        [Test]
        public void SplitWithStringSeparatorToString()
        {
            var result = ("1,2;3").Split<string>(",;").ToArray();

            Assert.IsTrue(result.Count() == 3);
        }

        #endregion Public Methods
    }
}
