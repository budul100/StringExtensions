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
        public void SplitLinesWithExclude()
        {
            var test = @"C:\Users\m
                C:\Users\l";

            var result = test.SplitLines(true).ToArray();

            Assert.IsTrue(result.Count() == 2);
        }

        [Test]
        public void SplitLinesWithoutExclude()
        {
            var test = @"C:\Users\m
                C:\Users\l";

            var result = test.SplitLines(false).ToArray();

            Assert.IsTrue(result.Count() == 2);
        }

        [Test]
        public void SplitMatchingLength()
        {
            var given = "0000";

            var result = given.Split(2).ToArray();

            Assert.IsTrue(result.Count() == 2);
        }

        [Test]
        public void SplitNonMatchingLength()
        {
            var given = "00000";

            var result = given.Split(2).ToArray();

            Assert.IsTrue(result.Count() == 3);
        }

        [Test]
        public void SplitStringToMultiNullableLong()
        {
            var result = ("4,5").Split<long?>(",").ToArray();

            Assert.IsTrue(result.First() == 4);
            Assert.IsTrue(result.Last() == 5);
        }

        [Test]
        public void SplitStringToNullableLongWithEmptyDelimiter()
        {
            var result = ("3").Split<long?>(default(string), true).ToArray();

            Assert.IsTrue(result.Count() == 0);
        }

        [Test]
        public void SplitStringToSingleNullableLong()
        {
            var result = ("3").Split<long?>(",").ToArray();

            Assert.IsTrue(result.Single() == 3);
        }

        [Test]
        public void SplitStringWithPipe()
        {
            var result1 = ("45").Split<string>(@"\|").ToArray();
            var result2 = (@"4|5").Split<string>(@"\|").ToArray();

            Assert.IsTrue(result1.Single() == "45");
            Assert.IsTrue(result2.First() == "4");
            Assert.IsTrue(result2.Last() == "5");
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

            var result2 = ("1,2;3").Split<int>("").ToArray();

            Assert.IsTrue(result2.Count() == 0);
        }

        [Test]
        public void SplitWithEmptyStringDelimiterToString()
        {
            var result = ("1,2;3").Split<string>(default(string)).ToArray();

            Assert.IsTrue(result.Count() == 1);

            var result2 = ("1,2;3").Split<string>("").ToArray();

            Assert.IsTrue(result2.Count() == 1);
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