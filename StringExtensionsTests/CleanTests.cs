using NUnit.Framework;
using StringExtensions;

namespace StringExtensionsTests
{
    public class CleanTests
    {
        #region Public Methods

        [Test]
        public void RemoveAcents()
        {
            var result = ("ÁĂÂÄÀĀĄÅÃÆĆČÇĈĊĎĐÉĔĚÊËĖÈĒĘŊÐĞĢĜĠĤĦÍĬÎÏİÌĪĮĨĴĶĹĽĻĿŁĲŒŃŇŅÑÓŎÔÖÒŌØÕŐŔŘŖŚŠŞŜŤŢŦÞŮÚŬÛÜŰÙŪŲŨŴÝŶŸŹŽŻáăâäàāąåãæćčçĉċďđıéĕěêëėèēęŋðğģĝġĥħíĭîïìīįĩĵķĸĺľļŀłĳœſńňņŉñóŏôöòōøõőŕřŗśšşŝßťţŧþůúŭûüűùūųũŵýŷÿźžż").RemoveAccents();

            Assert.AreEqual(
                result,
                "AAAAAAAAAÆCCCCCDĐEEEEEEEEEŊÐGGGGHĦIIIIIIIIIJKLLLĿŁĲŒNNNNOOOOOOØOORRRSSSSTTŦÞUUUUUUUUUUWYYYZZZaaaaaaaaaæcccccdđıeeeeeeeeeŋðgggghħiiiiiiiijkĸlllŀłĳœſnnnŉnooooooøoorrrssssßttŧþuuuuuuuuuuwyyyzzz");
        }

        [Test]
        public void ToCamelCases()
        {
            var result1 = ("ABCDEF").ToCamelCases();

            Assert.AreEqual(
                result1,
                "Abcdef");

            var result2 = ("abcdef").ToCamelCases();

            Assert.AreEqual(
                result2,
                "Abcdef");

            var result3 = ("Abcdef").ToCamelCases();

            Assert.AreEqual(
                result3,
                "Abcdef");
        }

        [Test]
        public void ToStandardChars()
        {
            var result = ("ÁĂÂÄÀĀĄÅÃÆĆČÇĈĊĎĐÉĔĚÊËĖÈĒĘŊÐĞĢĜĠĤĦÍĬÎÏİÌĪĮĨĴĶĹĽĻĿŁĲŒŃŇŅÑÓŎÔÖÒŌØÕŐŔŘŖŚŠŞŜŤŢŦÞŮÚŬÛÜŰÙŪŲŨŴÝŶŸŹŽŻáăâäàāąåãæćčçĉċďđıéĕěêëėèēęŋðğģĝġĥħíĭîïìīįĩĵķĸĺľļŀłĳœſńňņŉñóŏôöòōøõőŕřŗśšşŝßťţŧþůúŭûüűùūųũŵýŷÿźžż").ToStandardChars();

            Assert.AreEqual(
                result,
                "AAAAAAAAAACCCCCDDEEEEEEEEENEGGGGHHIIIIIIIIIJKLLLLLIONNNNOOOOOOOOORRRSSSSTTTPUUUUUUUUUUWYYYZZZaaaaaaaaaacccccddieeeeeeeeenegggghhiiiiiiiijkkllllliosnnnnnooooooooorrrssssstttpuuuuuuuuuuwyyyzzz");
        }

        #endregion Public Methods
    }
}