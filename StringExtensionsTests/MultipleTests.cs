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
        public void GetCommonShortString()
        {
            var list = new string[]
            {
                "A1",
                "A2"
            };

            var result = list.GetCommon();

            Assert.IsTrue(result == "A");
        }

        [Test]
        public void GetCommonStringWithoutSucces()
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
        public void GetCommonStringWithSucces()
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

        #endregion Public Methods
    }
}