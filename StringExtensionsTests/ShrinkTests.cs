using NUnit.Framework;
using StringExtensions;

namespace StringExtensionsTests
{
    public class ShrinkTests
    {
        #region Public Methods

        [Test]
        public void LevelLengthes()
        {
            var result = ("Banovce nad Bebravou").Shrink(8);

            Assert.AreEqual(
                result,
                "BnvcnBbr");
        }

        [Test]
        public void ShrinkWhitespace()
        {
            var result = ("Ulm Hbf").Shrink(10);

            Assert.IsFalse(result.Contains(
                value: " ",
                comparisonType: System.StringComparison.InvariantCulture));
        }

        [Test]
        public void ToCamelCases()
        {
            var result = ("Banovce nad Bebravou").Shrink(
                maxLength: 8,
                toCamelCases: true);

            Assert.AreEqual(
                result,
                "BnvcNBbr");
        }

        [Test]
        public void VowelAtBegin()
        {
            var result = ("Ianka pri Nitre").Shrink(8);

            Assert.AreEqual(
                result,
                "IankpNtr");
        }

        [Test]
        public void VowelAtInner()
        {
            var result = ("Medzibrodie nad Oravou").Shrink(8);

            Assert.AreEqual(
                result,
                "MdzbrnOr");
        }

        #endregion Public Methods
    }
}