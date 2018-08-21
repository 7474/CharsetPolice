using CharsetPolice.Police;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace CharsetPoliceTest
{
    [TestClass]
    public class CharsetPoliceManTest
    {
        [TestMethod]
        public void TestSearchCharset()
        {
            var policeMan = new CharsetPoliceMan();
            var body = "<html><head><meta charset=\"utf-8\"></head>";

            var expected = 18;
            var actual = policeMan.SearchCharset(Encoding.UTF8.GetBytes(body));

            Assert.AreEqual(expected, actual, "charset‚ªŠÜ‚Ü‚ê‚Ä‚¢‚éB");
        }
        [TestMethod]
        public void TestSearchCharsetUppercase()
        {
            var policeMan = new CharsetPoliceMan();
            var body = "<HTML><HEAD><META CHARSET=\"UTF-8\"></HEAD>";

            // “––Ê‘å•¶š‚Í–³‹
            //var expected = 18;
            var expected = -1;
            var actual = policeMan.SearchCharset(Encoding.UTF8.GetBytes(body));

            Assert.AreEqual(expected, actual, "CHARSET‚ªŠÜ‚Ü‚ê‚Ä‚¢‚é‚ª‘ÎÛŠOB");
        }
    }
}
