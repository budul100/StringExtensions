using NUnit.Framework;
using StringExtensions;

namespace StringExtensionsTests
{
    public class MultipleTests
    {
        #region Public Methods

        [Test]
        public void CommonString()
        {
            var list = new string[] { "ToCommondays", "MonCommonday_", "", string.Empty, default, "TuesCommonda", "WednesCommon_day" };

            var result = list.GetCommon();

            Assert.IsTrue(result == "Common");
        }

        #endregion Public Methods
    }
}