using NUnit.Framework;
using StringExtensions;

namespace StringExtensionsTests
{
    public class MultipleTests
    {
        #region Public Methods

        [Test]
        public void AddOnce()
        {
            var result = default(string).AddOnce("test");

            Assert.IsTrue(result == "test");
        }

        [Test]
        public void GetCommonWithFullDifference()
        {
            var list = new string[]
            {
                "AHausen",
                "BHausen",
                "",
            };

            var result = list.GetCommon();

            Assert.IsTrue(string.IsNullOrWhiteSpace(result));
        }

        [Test]
        public void GetCommonWithInnerDifference()
        {
            var list = new string[]
            {
                "AHAUSEN",
                "BHAUSEN",
            };

            var result = list.GetCommon();

            Assert.IsTrue(string.IsNullOrWhiteSpace(result));
        }

        [Test]
        public void GetCommonWithNumberAtEnd()
        {
            var list = new string[]
            {
                "AB1",
                "AB2"
            };

            var result = list.GetCommon();

            Assert.IsTrue(result == "AB");
        }

        [Test]
        public void GetCommonWithNumberAtFront()
        {
            var list = new string[]
            {
                "1AB",
                "2AB"
            };

            var result = list.GetCommon();

            Assert.IsTrue(string.IsNullOrWhiteSpace(result));
        }

        [Test]
        public void GetCommonWithPartDifference()
        {
            var list = new string[]
            {
                "To Commondays",
                "Mon Commonday_",
                "",
                string.Empty,
                default,
                "Tues Commonda",
                "Wednes Common_day"
            };

            var result = list.GetCommon();

            Assert.IsTrue(result == "Common");
        }

        [Test]
        public void GetCommonWithSame()
        {
            var list = new string[]
            {
                "AB",
                "AB"
            };

            var result = list.GetCommon();

            Assert.IsTrue(result == "AB");
        }

        #endregion Public Methods
    }
}