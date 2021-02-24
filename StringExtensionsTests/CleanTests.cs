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
        public void ShrinkWithWhitespace()
        {
            var result = ("Ulm Hbf").Shrink(10);

            Assert.IsFalse(result.Contains(" ", comparisonType: System.StringComparison.InvariantCulture));
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