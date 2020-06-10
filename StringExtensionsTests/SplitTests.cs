using NUnit.Framework;
using StringExtensions;
using System.Linq;

namespace StringExtensionsTests
{
    public class SplitTests
    {
        #region Public Methods

        [Test]
        public void SplitEmptyWithCharDelimiterToInt()
        {
            var result = (default(string)).Split<int>(',').ToArray();

            Assert.IsTrue(result.Count() == 0);
        }

        [Test]
        public void SplitEmptyWithCharDelimiterToString()
        {
            var result = (default(string)).Split<string>(',').ToArray();

            Assert.IsTrue(result.Count() == 0);
        }

        [Test]
        public void SplitEmptyWithStringDelimiterToInt()
        {
            var result = (default(string)).Split<int>(",;").ToArray();

            Assert.IsTrue(result.Count() == 0);
        }

        [Test]
        public void SplitEmptyWithStringDelimiterToString()
        {
            var result = (default(string)).Split<string>(",;").ToArray();

            Assert.IsTrue(result.Count() == 0);
        }

        [Test]
        public void SplitLines()
        {
            var test = @"C:\Users\m
                C:\Users\l";

            var result = test.SplitLines().ToArray();

            Assert.IsTrue(result.Count() == 2);
        }

        [Test]
        public void SplitWithCharDelimiterToInt()
        {
            var result = ("1,2,3").Split<int>(',').ToArray();

            Assert.IsTrue(result.Count() == 3);
        }

        [Test]
        public void SplitWithCharDelimiterToString()
        {
            var result = ("1,2,3").Split<string>(',').ToArray();

            Assert.IsTrue(result.Count() == 3);
        }

        [Test]
        public void SplitWithEmptyCharDelimiterToInt()
        {
            var result = ("1,2;3").Split<int>(default(char)).ToArray();

            Assert.IsTrue(result.Count() == 0);
        }

        [Test]
        public void SplitWithEmptyCharDelimiterToString()
        {
            var result = ("1,2;3").Split<string>(default(char)).ToArray();

            Assert.IsTrue(result.Count() == 1);
        }

        [Test]
        public void SplitWithEmptyStringDelimiterToInt()
        {
            var result = ("1,2;3").Split<int>(default(string)).ToArray();

            Assert.IsTrue(result.Count() == 0);
        }

        [Test]
        public void SplitWithEmptyStringDelimiterToString()
        {
            var result = ("1,2;3").Split<string>(default(string)).ToArray();

            Assert.IsTrue(result.Count() == 1);
        }

        [Test]
        public void SplitWithStringDelimiterToInt()
        {
            var result = ("1,2;3").Split<int>("[,;]").ToArray();

            Assert.IsTrue(result.Count() == 3);
        }

        [Test]
        public void SplitWithStringDelimiterToString()
        {
            var result = ("1,2;3").Split<string>("[,;]").ToArray();

            Assert.IsTrue(result.Count() == 3);
        }

        #endregion Public Methods
    }
}